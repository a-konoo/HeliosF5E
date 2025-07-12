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
    /// UI logic for Flip3WayToggleBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.Base.Flip3WayToggle", "Behavior")]
    public partial class Flip3WayToggleBehaviorEditor : HeliosPropertyEditor
    {
        public Flip3WayToggleBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ControlProperty)
            {
                Flip3WayToggle toggle = Control as Flip3WayToggle;
            }
            base.OnPropertyChanged(e);
        }

        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            Flip3WayToggle toggle = Control as Flip3WayToggle;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && toggle != null)
            {
                int index = toggle.Positions.IndexOf((Flip3WayTogglePosition)senderControl.Tag);
                toggle.CurrentPosition = index + 1;
            }
        }

        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            Flip3WayToggle toggle = Control as Flip3WayToggle;
            if (toggle != null)
            {
                int nextFrame = 1;

                if (toggle.Positions.Count > 0)
                {
                    nextFrame = toggle.Positions[toggle.Positions.Count - 1].Frame + 1;
                }

                Flip3WayTogglePosition position = new Flip3WayTogglePosition(toggle,
                    toggle.MaxPositionCount, // index
                    (toggle.MaxPositionCount + 1).ToString(CultureInfo.InvariantCulture),
                    nextFrame, 0, 0, 0);

                toggle.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new Flip3WayToggleADDUndoEvent(toggle, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Flip3WayToggle toggle = Control as Flip3WayToggle;
            if (toggle != null)
            {
                e.CanExecute = (toggle.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Flip3WayToggle toggle = Control as Flip3WayToggle;
            if (toggle != null &&
                toggle.Positions.Contains((Flip3WayTogglePosition)PositionList.SelectedItem))
            {
                Flip3WayTogglePosition removedPosition = (Flip3WayTogglePosition)PositionList.SelectedItem;
                int index = toggle.Positions.IndexOf(removedPosition);
                toggle.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(new Flip3WayToggleDELUndoEvent(toggle, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Flip3WayToggle toggle = Control as Flip3WayToggle;
            if (toggle != null)
            {
                toggle.CurrentPosition = PositionList.SelectedIndex + 1;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            Flip3WayToggle toggle = Control as Flip3WayToggle;
            var lastposition = toggle.LastPositionElement;
            if (lastposition != null)
            {
                toggle.Positions.Remove(lastposition);
            }
        }
    }
}
