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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for FlipLeverBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.F5E.FlipLever", "Behavior")]
    public partial class FlipLeverBehaviorEditor : HeliosPropertyEditor
    {
        public FlipLeverBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ControlProperty)
            {
                FlipLever flipLever = Control as FlipLever;
                if (flipLever != null && flipLever.DefaultPosition > 0)
                {
                    DefaultPositionCombo.SelectedIndex = flipLever.DefaultPosition - 1;
                }
            }
            base.OnPropertyChanged(e);
        }

        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && flipLever != null)
            {
                int index = flipLever.Positions.IndexOf((FlipLeverPosition)senderControl.Tag);
                flipLever.CurrentPosition = index;
            }
        }

        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            if (flipLever != null)
            {
                var index = flipLever.Positions.Count + 1;
                FlipLeverPosition position = new FlipLeverPosition(
                    flipLever, index, $"{index}",1, 2, 100);
                flipLever.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new FlipLeverADDUndoEvent(flipLever, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            if (flipLever != null)
            {
                e.CanExecute = (flipLever.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            if (flipLever != null && flipLever.Positions.Contains((FlipLeverPosition)PositionList.SelectedItem))
            {
                FlipLeverPosition removedPosition = (FlipLeverPosition)PositionList.SelectedItem;
                int index = flipLever.Positions.IndexOf(removedPosition);
                flipLever.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(
                    new FlipLeverDELUndoEvent(flipLever, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            if (flipLever != null)
            {
                flipLever.CurrentPosition = PositionList.SelectedIndex;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            if (flipLever != null)
            {
                flipLever.DefaultPosition = DefaultPositionCombo.SelectedIndex;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            FlipLever flipLever = Control as FlipLever;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && flipLever != null)
            {
                FlipLeverPosition position = senderControl.Tag as FlipLeverPosition;
                if (position != null && flipLever.Positions.Contains(position))
                {
                    flipLever.Positions.Remove(position);
                }
            }
        }

    }
}
