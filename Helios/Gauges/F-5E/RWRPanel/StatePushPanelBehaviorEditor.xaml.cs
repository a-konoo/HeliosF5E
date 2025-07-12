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
    /// UI logic for StatePushPanelBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Helios.Base.StatePushPanel", "Behavior")]
    public partial class StatePushPanelBehaviorEditor : HeliosPropertyEditor
    {

        public StatePushPanelBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        private void Position_GotFocus(object sender, RoutedEventArgs e)
        {
            StatePushPanel panel = Control as StatePushPanel;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && panel != null)
            {
                int index = panel.Positions.IndexOf((StatePushPanelPosition)senderControl.Tag);
                panel.CurrentState = index + 1;
            }
        }

        private void Add_Position_Click(object sender, RoutedEventArgs e)
        {
            StatePushPanel panel = Control as StatePushPanel;
            if (panel != null)
            {

                StatePushPanelPosition position = new StatePushPanelPosition(
                    panel,
                    panel.Positions.Count, // index
                    string.Empty);

                panel.Positions.Add(position);
                ConfigManager.UndoManager.AddUndoItem(new StatePushPanelADDUndoEvent(panel, position));
            }
        }

        private void DeletePosition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            StatePushPanel panel = Control as StatePushPanel;
            if (panel != null)
            {
                e.CanExecute = (panel.Positions.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeletePosition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatePushPanel panel = Control as StatePushPanel;
            if (panel != null &&
                panel.Positions.Contains((StatePushPanelPosition)PositionList.SelectedItem))
            {
                StatePushPanelPosition removedPosition = (StatePushPanelPosition)PositionList.SelectedItem;
                int index = panel.Positions.IndexOf(removedPosition);
                panel.Positions.Remove(removedPosition);
                ConfigManager.UndoManager.AddUndoItem(
                    new StatePushPanelDELUndoEvent(panel, removedPosition, index));
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatePushPanel panel = Control as StatePushPanel;
            if (panel != null)
            {
                panel.CurrentState = PositionList.SelectedIndex;
            }
        }

        private void Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            StatePushPanel panel = Control as StatePushPanel;
            var lastposition = panel.LastPositionElement;
            if (lastposition != null)
            {
                panel.Positions.Remove(lastposition);
            }
        }
    }
}
