//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using NLog;

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.Interfaces.DCS.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Threading;

    public delegate void FinishFlapDisplayWork(int value, DateTimeOffset datetime);

    public class FlapDisplayControl
    {
        private DispatcherTimer _waitTimer;

        private static Guid blockId;
        private static GaugeNeedle curGauge;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static int flapValue;
        private static List<int> flapValues = new List<int>();
        public DateTimeOffset StartTime { set; get; }
        public Func<int, DateTimeOffset, ValueTuple<int, int>> DequeueFunc { set; get; }

        public event FinishFlapDisplayWork FinishFlapDisplayEvent = delegate (int value, DateTimeOffset datetime) { };

        private readonly Dictionary<int, GaugeNeedle> gaugeHash;

        public FlapDisplayControl(Dictionary<int, GaugeNeedle> _gaugeHash, int _flapValue)
        {
            flapValue = _flapValue;
            gaugeHash = _gaugeHash;
        }

        public void TaskRun(Guid id, F5E_FLAP_DISPLAY_SEQUENCE status, int flapValue)
        {

            if (blockId != id && blockId != Guid.Empty) { return; }
            _waitTimer?.Stop();
            if (_waitTimer == null)
            {
                _waitTimer = new DispatcherTimer();
            }
            blockId = id;
            try
            {
                bool isBorder = false;
                int judge = 0;
                switch ((F5E_FLAP_DISPLAY_SEQUENCE)status)
                {
                    case F5E_FLAP_DISPLAY_SEQUENCE.START:
                        curGauge = GetCurrentCard();
                        if (curGauge == null)
                        {
                            // AUTO
                            curGauge = gaugeHash[6];
                        }
                        F5E_FLAP_CARD_POSITION cardPos = GetCurrentPosition(curGauge);
                        ChangeStatusCards(curGauge);
                        status = F5E_FLAP_DISPLAY_SEQUENCE.CHECK;
                        if (cardPos == (F5E_FLAP_CARD_POSITION.UP))
                        {
                            status = F5E_FLAP_DISPLAY_SEQUENCE.HIDE;
                        }
                        StartTime = DateTime.Now;
                        flapValues.Clear();
                        break;

                    case F5E_FLAP_DISPLAY_SEQUENCE.HIDE:

                        isBorder = MovingDown(curGauge);
                        if (isBorder)
                        {
                            status = F5E_FLAP_DISPLAY_SEQUENCE.CHECK;
                        }
                        StartTime = DateTime.Now;
                        flapValues.Clear();
                        break;
                    case F5E_FLAP_DISPLAY_SEQUENCE.CHECK:
                        (judge, flapValue) = DequeueFunc(flapValue, StartTime);
                        Console.WriteLine(judge + "/" + flapValue+ "/" + String.Join(",",flapValues?.ToArray()));
                        if (flapValues.Count() ==0 || flapValues?.Last() != flapValue)
                        {
                            flapValues.Add(flapValue);
                        }
                        
                        if (judge == 1)
                        {
                            if (flapValue == 0 || flapValues.Max() == 0)
                            {
                                int timeSpanMill = ((13 - flapValue)) * 100;
                                StartTime = DateTime.Now + TimeSpan.FromMilliseconds(timeSpanMill);
                                status = F5E_FLAP_DISPLAY_SEQUENCE.CHECK;
                            } else
                            {
                                flapValue = flapValues.Max();
                                flapValues.Clear();
                                curGauge = gaugeHash[flapValue];
                                ChangeStatusCards(curGauge);
                                status = F5E_FLAP_DISPLAY_SEQUENCE.SHOW;
                                F5E_FLAP_CARD_POSITION resetPos = GetCurrentPosition(curGauge);
                            }
                            ChangeAllCardPos(F5E_FLAP_CARD_POSITION.DOWN);

                        }
                        else
                        {
                            if(gaugeHash.ContainsKey(flapValue))
                            {
                                curGauge = gaugeHash[flapValue];
                                flapValues.Clear();
                                ChangeStatusCards(curGauge);
                                ChangeAllCardPos(F5E_FLAP_CARD_POSITION.DOWN);
                            }
                            status = F5E_FLAP_DISPLAY_SEQUENCE.SHOW;
                        }
                        break;
                    case F5E_FLAP_DISPLAY_SEQUENCE.SHOW:
                       
                        isBorder = MovingUp(curGauge);
                        if (isBorder)
                        {
                            status = F5E_FLAP_DISPLAY_SEQUENCE.CHECK;
                            if (judge == 0 || judge == 1 || judge == 3)
                            {
                                status = F5E_FLAP_DISPLAY_SEQUENCE.PREEND;
                            }
                        }
                        break;
                    case F5E_FLAP_DISPLAY_SEQUENCE.PREEND:
                        F5E_FLAP_CARD_POSITION endPos = GetCurrentPosition(curGauge);
                        ChangeAllCardPos(endPos);
                        status = F5E_FLAP_DISPLAY_SEQUENCE.END;
                        break;

                }
                if (status == F5E_FLAP_DISPLAY_SEQUENCE.END)
                {
                    FinishTask(flapValue);
                    return;
                }
                SetTaskRunTimer(id, status, flapValue);
            }
            catch (Exception ex)
            {
                Logger.Warn($"Unable start to move flap animation" + ex);
            }
            return;
        }

        public void SetTaskRunTimer(Guid id, F5E_FLAP_DISPLAY_SEQUENCE status, int flapValue)
        {
            _waitTimer = new DispatcherTimer();

            _waitTimer.Tick += (sender, e2) =>
            {
                TaskRun(id, status, flapValue);
            };
            _waitTimer.Interval = TimeSpan.FromMilliseconds(100L);
            _waitTimer.Tag = flapValue;
            _waitTimer.Start();
        }

        public GaugeNeedle GetCurrentCard()
        {
            var currentCard = gaugeHash.Where(x => x.Value.IsHidden != true);
            return currentCard.First().Value;
        }



        public GaugeNeedle[] GetAllCard()
        {
            var allGauges = gaugeHash.Select(x => x.Value).ToArray();
            return allGauges;
        }

        public int ConvertCardToStatus(GaugeNeedle gauge)
        {
            return gaugeHash.Where(x => x.Value == gauge)?.First().Key ?? 0;
        }

        public F5E_FLAP_CARD_POSITION GetCurrentPosition(GaugeNeedle gauge)
        {
            var result = new[] { F5E_FLAP_CARD_POSITION.UP, F5E_FLAP_CARD_POSITION.DOWN }.Where(x => (int)x == gauge.VerticalOffset);
            return result.Count() > 0 ? result.First() : F5E_FLAP_CARD_POSITION.MID;
        }


        private GaugeNeedle SelectCard(int index)
        {
            return gaugeHash[index];
        }

        private bool MovingDown(GaugeNeedle gauge)
        {
            if (gauge.VerticalOffset > (int)F5E_FLAP_CARD_POSITION.DOWN)
            {
                gauge.VerticalOffset = (int)F5E_FLAP_CARD_POSITION.DOWN;
                return true;
            }

            gauge.VerticalOffset += 6;
            return false;
        }
        private bool MovingUp(GaugeNeedle gauge)
        {

            if (gauge.VerticalOffset <= (int)F5E_FLAP_CARD_POSITION.UP)
            {
                gauge.VerticalOffset = (int)F5E_FLAP_CARD_POSITION.UP;
                return true;
            }

            gauge.VerticalOffset -= 6;
            return false;
        }

        public void ChangeStatusCards(GaugeNeedle curr)
        {
            if (curr == null) { return; }
            Action<GaugeNeedle> func = (a) =>
            {
                var others = GetAllCard().Where(x => !x.Equals(a));
                foreach (GaugeNeedle c in others)
                {
                    c.IsHidden = true;
                }
                curr.IsHidden = false;
            };
            func(curr);
        }
        public void ChangeAllCardPos(F5E_FLAP_CARD_POSITION pos)
        {
            foreach (var card in GetAllCard())
            {
                card.VerticalOffset = (int)pos;
            }
        }

        public void SetHideStatus(GaugeNeedle curr, bool isHide)
        {
            if (curr == null) { return; }
            curr.IsHidden = isHide;
        }

        public void FinishTask(int flapValue)
        {
            _waitTimer?.Stop();
            _waitTimer = null;
            blockId = Guid.Empty;
            FinishFlapDisplayEvent(flapValue, DateTimeOffset.Now);
        }
    }
}