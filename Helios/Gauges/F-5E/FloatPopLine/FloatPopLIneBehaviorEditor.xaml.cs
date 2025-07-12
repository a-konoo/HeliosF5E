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
    /// Interaction logic for  FloatPopLineBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.Base.FloatPopLine", "Behavior")]
    public partial class FloatPopLineBehaviorEditor : HeliosPropertyEditor
    {
        public FloatPopLineBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ControlProperty)
            {
                FloatPopLine popLine = Control as FloatPopLine;
                if (popLine != null && popLine.DefaultPosition > 0)
                {
                    DefaultPositionCombo.SelectedIndex = popLine.DefaultPosition - 1;
                }
            }
            base.OnPropertyChanged(e);
        }

        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            FloatPopLine popLine = Control as FloatPopLine;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && popLine != null)
            {
                int index = popLine.Positions.IndexOf((FloatPopLinePosition)senderControl.Tag);
                popLine.CurrentPosition = index;
            }
        }

        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            FloatPopLine popLine = Control as FloatPopLine;
            if (popLine != null)
            {
                var index = popLine.Positions.Count + 1;
                FloatPopLinePosition position = new FloatPopLinePosition(popLine, index, 0, 0, 10, 10, false, 90);
                popLine.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new FloatPopLineAddPositionUndoEvent(popLine, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            FloatPopLine popLine = Control as FloatPopLine;
            if (popLine != null)
            {
                e.CanExecute = (popLine.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
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

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FloatPopLine popLine = Control as FloatPopLine;
            if (popLine != null && popLine.Positions.Contains((FloatPopLinePosition)PositionList.SelectedItem))
            {
                FloatPopLinePosition removedPosition = (FloatPopLinePosition)PositionList.SelectedItem;
                int index = popLine.Positions.IndexOf(removedPosition);
                popLine.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(
                    new FloatPopLineDeletePositionUndoEvent(popLine, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FloatPopLine popLine = Control as FloatPopLine;
            if (popLine != null)
            {
                popLine.CurrentPosition = PositionList.SelectedIndex;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            FloatPopLine popLine = Control as FloatPopLine;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && popLine != null)
            {
                FloatPopLinePosition position = senderControl.Tag as FloatPopLinePosition;
                if (position != null && popLine.Positions.Contains(position))
                {
                    popLine.Positions.Remove(position);
                }
            }
        }
    }
}
