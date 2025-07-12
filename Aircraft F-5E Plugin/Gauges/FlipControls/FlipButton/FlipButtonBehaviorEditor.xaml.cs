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
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Windows.Controls;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// UI logic for FlipButtonBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.F5E.FlipButton", "Behavior")]
    public partial class FlipButtonBehaviorEditor : HeliosPropertyEditor
    {

        public FlipButtonBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ControlProperty)
            {
                FlipButton button = Control as FlipButton;
            }
            base.OnPropertyChanged(e);
        }

        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            FlipButton button = Control as FlipButton;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && button != null)
            {
                int index = button.Positions.IndexOf((FlipButtonPosition)senderControl.Tag);
                button.CurrentPosition = index + 1;
            }
        }

        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            FlipButton button = Control as FlipButton;
            if (button != null)
            {
                int nextFrame = 1;

                if (button.Positions.Count > 0)
                {
                    nextFrame = button.Positions[button.Positions.Count - 1].Frame + 1;
                }

                FlipButtonPosition position = new FlipButtonPosition(button,
                    button.MaxPositionCount, // index
                    (button.MaxPositionCount + 1).ToString(CultureInfo.InvariantCulture),
                    nextFrame,0, 0, 0);

                button.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new FlipButtonADDUndoEvent(button, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            FlipButton button = Control as FlipButton;
            if (button != null)
            {
                e.CanExecute = (button.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FlipButton button = Control as FlipButton;
            if (button != null &&
                button.Positions.Contains((FlipButtonPosition)PositionList.SelectedItem))
            {
                FlipButtonPosition removedPosition = (FlipButtonPosition)PositionList.SelectedItem;
                int index = button.Positions.IndexOf(removedPosition);
                button.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(
                    new FlipButtonDELUndoEvent(button, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlipButton button = Control as FlipButton;
            if (button != null)
            {
                button.CurrentPosition = PositionList.SelectedIndex + 1;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            FlipButton button = Control as FlipButton;
            var lastposition = button.LastPositionElement;
            if (lastposition != null)
            {
                button.Positions.Remove(lastposition);
            }
        }
    }
}
