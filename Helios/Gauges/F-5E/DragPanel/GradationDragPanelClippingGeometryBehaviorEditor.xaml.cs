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
    /// Interaction logic for  GradationDragPanelClippingGeometryBehaviorEditor.xaml
    /// </summary>
    [HeliosPropertyEditor("Drag.Panel", "Behavior")]
    public partial class GradationDragPanelClippingGeometryBehaviorEditor : HeliosPropertyEditor
    {
        public GradationDragPanelClippingGeometryBehaviorEditor()
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
                int index = dragPanel.GeometryCollections.
                    IndexOf((GradationDragPanelClippingGeometry)senderControl.Tag);
            }
        }

        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            if (dragPanel != null)
            {
                var index = dragPanel.GeometryCollections.Count + 1;
                var feature = new EllipseGeometry(new Point(10 * index, 10 * index), 10d, 10d);
                GradationDragPanelClippingGeometry geometry
                    = new GradationDragPanelClippingGeometry(dragPanel, index, 0, 0, 1, 10, 0, feature, "");
                dragPanel.GeometryCollections.Add(geometry);
                ConfigManager.UndoManager.AddUndoItem(
                    new GradationDragPanelAddClippingGeometryUndoEvent(dragPanel, geometry));
            }
        }



        private void DeleteItem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            if (dragPanel != null)
            {
                e.CanExecute = (dragPanel.GeometryCollections.Count > 2);
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void DeleteItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            if (dragPanel != null && dragPanel.GeometryCollections.
                Contains((GradationDragPanelClippingGeometry)Collections.SelectedItem))
            {
                GradationDragPanelClippingGeometry removeItem = (GradationDragPanelClippingGeometry)Collections.SelectedItem;
                int index = dragPanel.GeometryCollections.IndexOf(removeItem);
                dragPanel.GeometryCollections.Remove(removeItem);
                ConfigManager.UndoManager.AddUndoItem(
                    new GradationDragPanelClippingGeometryUndoEvent(dragPanel, removeItem, index));
            }
        }

        private void Delete_Item_Click(object sender, RoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && dragPanel != null)
            {
                GradationDragPanelClippingGeometry geometry = senderControl.Tag as GradationDragPanelClippingGeometry;
                if (geometry != null && dragPanel.GeometryCollections.Contains(geometry))
                {
                    dragPanel.GeometryCollections.Remove(geometry);
                }
            }
        }

        private void Create_Geometry_Click(object sender, RoutedEventArgs e)
        {
            GradationDragPanel dragPanel = Control as GradationDragPanel;
            FrameworkElement senderControl = sender as FrameworkElement;
            if (senderControl != null && dragPanel != null)
            {
                GradationDragPanelClippingGeometry geometry = senderControl.Tag as GradationDragPanelClippingGeometry;
                
                if (geometry != null && dragPanel.GeometryCollections.Contains(geometry))
                {
                    int geomType = (int)geometry.GeometryType;
                    var geom = dragPanel.CreateGeometry(
                        geomType, geometry.X, geometry.Y, geometry.Radius, geometry.RectHeight);
                    geometry.Geometry = geom;
                    geometry.GeomText = dragPanel.ExportB64Geometry(geom);
                    dragPanel.Renderer.Refresh();
                }
            }
        }
    }
}
