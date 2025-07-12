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
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for  GradationDragPanelPositionBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Drag.Panel", "Behavior")]
    public partial class GradationDragPanelPositionBehaviorEditor : HeliosPropertyEditor
    {
        public GradationDragPanelPositionBehaviorEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && dragPanel != null)
            {
                int index = dragPanel.PositionCollections.
                    IndexOf((GradationDragPanelPosition)senderControl.Tag);
            }
        }

        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            if (dragPanel != null)
            {
                var index = dragPanel.PositionCollections.Count + 1;
                GradationDragPanelPosition position
                     = new GradationDragPanelPosition(dragPanel, index, 0, 0);
                dragPanel.PositionCollections.Add(position);
                ConfigManager.UndoManager.AddUndoItem(
                    new GradationDragPanelAddPositionUndoEvent(dragPanel, position));
            }
        }


        private void DeleteItem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            if (dragPanel != null)
            {
                e.CanExecute = (dragPanel.PositionCollections.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeleteItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            if (dragPanel != null && dragPanel.PositionCollections.
                Contains((GradationDragPanelPosition)Positions.SelectedItem))
            {
                GradationDragPanelPosition removeItem = (GradationDragPanelPosition)Positions.SelectedItem;
                int index = dragPanel.PositionCollections.IndexOf(removeItem);
                dragPanel.PositionCollections.Remove(removeItem);
                ConfigManager.UndoManager.AddUndoItem(
                    new GradationDragPanelPositionUndoEvent(dragPanel, removeItem, index));
            }
        }

        private void Delete_Item_Click(object sender, RoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && dragPanel != null)
            {
                GradationDragPanelPosition position = senderControl.Tag as GradationDragPanelPosition;
                if (position != null && dragPanel.PositionCollections.Contains(position))
                {
                    dragPanel.PositionCollections.Remove(position);
                }
            }
        }
    }
}
