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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for RoundSolidPullKnobBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.Base.RoundSolidPullKnob", "Behavior")]
    public partial class RoundSolidPullKnobBehaviorEditor : HeliosPropertyEditor
    {
        public RoundSolidPullKnobBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ControlProperty)
            {
                RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
                if (rotary != null && rotary.DefaultPosition > 0)
                {
                    DefaultPositionCombo.SelectedIndex = rotary.DefaultPosition - 1;
                }
            }
            base.OnPropertyChanged(e);
        }

        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && rotary != null)
            {
                int index = rotary.Positions.IndexOf((RoundSolidPullKnobPosition)senderControl.Tag);
                rotary.CurrentPosition = index;
            }
        }

        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            if (rotary != null)
            {
                var index = rotary.Positions.Count + 1;
                RoundSolidPullKnobPosition position = new RoundSolidPullKnobPosition(rotary, index, false, 0, 0, "", "");
                rotary.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new RoundSolidPullKnobAddUndoEvent(rotary, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            if (rotary != null)
            {
                e.CanExecute = (rotary.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            if (rotary != null && rotary.Positions.Contains((RoundSolidPullKnobPosition)PositionList.SelectedItem))
            {
                RoundSolidPullKnobPosition removedPosition = (RoundSolidPullKnobPosition)PositionList.SelectedItem;
                int index = rotary.Positions.IndexOf(removedPosition);
                rotary.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(
                    new RoundSolidPullKnobDelUndoEvent(rotary, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            if (rotary != null)
            {
                rotary.CurrentPosition = PositionList.SelectedIndex;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            if (rotary != null)
            {
                rotary.DefaultPosition = DefaultPositionCombo.SelectedIndex;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            RoundSolidPullKnob rotary = Control as RoundSolidPullKnob;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && rotary != null)
            {
                RoundSolidPullKnobPosition position = senderControl.Tag as RoundSolidPullKnobPosition;
                if (position != null && rotary.Positions.Contains(position))
                {
                    rotary.Positions.Remove(position);
                }
            }
        }
    }
}
