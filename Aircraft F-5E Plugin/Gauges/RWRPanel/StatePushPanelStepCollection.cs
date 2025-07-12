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

namespace GadrocsWorkshop.Helios.Controls.F5E
{
    using GadrocsWorkshop.Helios.Collections;
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.ComponentModel;

    public class StatePushPanelStepCollection : NoResetObservablecollection<StatePushPanelPosition>
    {
        private PropertyChangedEventHandler _changeHandler;

        public event EventHandler<StatePushPanelChangeArgs> PositionChanged;

        protected override void InsertItem(int index, StatePushPanelPosition item)
        {
            _changeHandler = new PropertyChangedEventHandler(Item_PropertyChanged);
            item.PropertyChanged += _changeHandler;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].PropertyChanged -= _changeHandler;
            base.RemoveItem(index);
        }

        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyNotificationEventArgs args = e as PropertyNotificationEventArgs;
            EventHandler<StatePushPanelChangeArgs> handler = PositionChanged;
            if (args != null)
            {
                if (handler != null)
                {
                    handler.Invoke(this, new StatePushPanelChangeArgs(
                        sender as StatePushPanelPosition,
                        args.PropertyName, args.OldValue, args.NewValue));
                }
            }
        }
    }
}
