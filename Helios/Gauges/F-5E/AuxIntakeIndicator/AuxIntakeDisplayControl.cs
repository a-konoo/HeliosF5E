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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Channels;
    using System.Windows.Threading;

    public delegate void AuxInrakeDisplayWork(int value, DateTimeOffset datetime);

    public class AuxIntakeDisplayControl
    {
        private DispatcherTimer _waitTimer;
        private static Guid blockId;
        private static GaugeNeedle curGauge;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static int intakeValue;
        private static bool retryFlag = false;
        public DateTimeOffset StartTime { set; get; }
        public Func<int, DateTimeOffset, ValueTuple<int, int>> DequeueFunc { set; get; }

        public event FinishFlapDisplayWork FinishAuxIntakeDisplayEvent = delegate (int value, DateTimeOffset datetime) { };
        private readonly Dictionary<int, GaugeNeedle> gaugeHash;

        public AuxIntakeDisplayControl(Dictionary<int, GaugeNeedle> _gaugeHash, int _intakeValue)
        {
            intakeValue = _intakeValue;
            gaugeHash = _gaugeHash;
        }

        public void TaskRun(Guid id, F5E_AUXINTAKE_DISPLAY_SEQUENCE status, int intakeValue)
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
                
                switch ((F5E_AUXINTAKE_DISPLAY_SEQUENCE)status)
                {
                    case F5E_AUXINTAKE_DISPLAY_SEQUENCE.START:
                        curGauge = GetCurrentCard();
                        retryFlag = false;
                        if (curGauge == null)
                        {
                            // CLOSE
                            curGauge = gaugeHash[1];
                        }

                        F5E_AUXINTAKE_CARD_POSITION cardPos = GetCurrentPosition(curGauge);
                        ChangeStatusCards(curGauge);
                        status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.CHECK;
                        if (cardPos == (F5E_AUXINTAKE_CARD_POSITION.UP))
                        {
                            status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.HIDE;
                        }
                        StartTime = DateTime.Now;
                        break;

                    case F5E_AUXINTAKE_DISPLAY_SEQUENCE.HIDE:

                        isBorder = MovingDown(curGauge);
                        if (isBorder)
                        {
                            status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.CHECK;
                        }
                        StartTime = DateTime.Now;
                        break;
                    case F5E_AUXINTAKE_DISPLAY_SEQUENCE.CHECK:
                        (judge, intakeValue) = DequeueFunc(intakeValue, StartTime);

                        status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.END;
                        if (intakeValue != 0)
                        {
                            curGauge = gaugeHash[intakeValue];
                            ChangeStatusCards(curGauge);
                            status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.SHOW;
                        }else
                        {
                            status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.END;
                        }
                        ChangeAllCardPos(F5E_AUXINTAKE_CARD_POSITION.UP);
                        break;
                    case F5E_AUXINTAKE_DISPLAY_SEQUENCE.SHOW:
                        curGauge = SelectCard(intakeValue);
                        ChangeStatusCards(curGauge);
                        isBorder = MovingUp(curGauge);
                        if (isBorder)
                        {

                            status = F5E_AUXINTAKE_DISPLAY_SEQUENCE.END;
                        }
                        break;
                    case F5E_AUXINTAKE_DISPLAY_SEQUENCE.END:
                        break;

                }
                if (status == F5E_AUXINTAKE_DISPLAY_SEQUENCE.END)
                {
                    F5E_AUXINTAKE_CARD_POSITION endPos = GetCurrentPosition(curGauge);
                    ChangeAllCardPos(endPos);
                    FinishTask(intakeValue);
                    return;
                }
                SetTaskRunTimer(id, status, intakeValue);
            }
            catch (Exception ex)
            {
                Logger.Warn($"Unable start to move flap animation" + ex);
            }
            return;
        }

        public void SetTaskRunTimer(Guid id, F5E_AUXINTAKE_DISPLAY_SEQUENCE status, int intakeValue)
        {
            _waitTimer = new DispatcherTimer();

            _waitTimer.Tick += (sender, e2) =>
            {
                TaskRun(id, status, intakeValue);
            };
            _waitTimer.Interval = TimeSpan.FromMilliseconds(100L);
            _waitTimer.Tag = intakeValue;
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

        public F5E_AUXINTAKE_CARD_POSITION GetCurrentPosition(GaugeNeedle gauge)
        {
            var result = new[] { F5E_AUXINTAKE_CARD_POSITION.UP, F5E_AUXINTAKE_CARD_POSITION.DOWN }.Where(x => (int)x == gauge.VerticalOffset);
            return result.Count() > 0 ? result.First() : F5E_AUXINTAKE_CARD_POSITION.MID;
        }


        private GaugeNeedle SelectCard(int index)
        {
            return gaugeHash[index];
        }

        private bool MovingDown(GaugeNeedle gauge)
        {
            if (gauge.VerticalOffset > (int)F5E_AUXINTAKE_CARD_POSITION.DOWN)
            {
                gauge.VerticalOffset = (int)F5E_AUXINTAKE_CARD_POSITION.DOWN;
                return true;
            }
            gauge.VerticalOffset += 10;
            return false;
        }
        private bool MovingUp(GaugeNeedle gauge)
        {

            if (gauge.VerticalOffset <= (int)F5E_AUXINTAKE_CARD_POSITION.UP)
            {
                gauge.VerticalOffset = (int)F5E_AUXINTAKE_CARD_POSITION.UP;
                return true;
            }
            gauge.VerticalOffset -= 10;
            return true;
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
        public void ChangeAllCardPos(F5E_AUXINTAKE_CARD_POSITION pos)
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

        public void FinishTask(int intakeValue)
        {
            _waitTimer?.Stop();
            _waitTimer = null;
            blockId = Guid.Empty;
            FinishAuxIntakeDisplayEvent(intakeValue, DateTimeOffset.Now);
        }
    }
}