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


namespace GadrocsWorkshop.Helios.Controls
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Windows.Controls;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// UI logic for FlipThrottleBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.Base.FlipThrottle", "Behavior")]
    public partial class FlipThrottleBehaviorEditor : HeliosPropertyEditor
    {
        public FlipThrottleBehaviorEditor()
        {
            InitializeComponent();
        }


        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            FlipThrottle throttle = Control as FlipThrottle;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && throttle != null)
            {
                int index = throttle.Positions.IndexOf((FlipThrottlePosition)senderControl.Tag);
                throttle.CurrentPosition = index + 1;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ControlProperty)
            {
                FlipThrottle button = Control as FlipThrottle;
            }
            base.OnPropertyChanged(e);
        }
        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            FlipThrottle throttle = Control as FlipThrottle;
            if (throttle != null)
            {
                int nextFrame = 1;

                if (throttle.Positions.Count > 0)
                {
                    nextFrame = throttle.Positions[throttle.Positions.Count - 1].Frame + 1;
                }

                FlipThrottlePosition position = new FlipThrottlePosition(throttle,
                    throttle.MaxPositionCount, // index
                    (throttle.MaxPositionCount + 1).ToString(CultureInfo.InvariantCulture),
                    nextFrame, 0);

                throttle.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new FlipThrottleADDUndoEvent(throttle, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            FlipThrottle throttle = Control as FlipThrottle;
            if (throttle != null)
            {
                e.CanExecute = (throttle.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FlipThrottle throttle = Control as FlipThrottle;
            if (throttle != null &&
                throttle.Positions.Contains((FlipThrottlePosition)PositionList.SelectedItem))
            {
                FlipThrottlePosition removedPosition = (FlipThrottlePosition)PositionList.SelectedItem;
                int index = throttle.Positions.IndexOf(removedPosition);
                throttle.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(new FlipThrottleDELUndoEvent(throttle, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlipThrottle throttle = Control as FlipThrottle;
            if (throttle != null)
            {
                throttle.CurrentPosition = PositionList.SelectedIndex + 1;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            FlipThrottle throttle = Control as FlipThrottle;
            var lastposition = throttle.LastPositionElement;
            if (lastposition != null)
            {
                throttle.Positions.Remove(lastposition);
            }
        }
    }
}
