using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GmapDemo.CustomMarkers;

namespace GmapDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PointLatLng start;
        PointLatLng end;

        GMapMarker currentMarker;
        GMapMarker marker;

        List<PointLatLng> points = new List<PointLatLng>();
        List<GMapMarker> markers = new List<GMapMarker>();

        private bool isMousePressed = false;
        private bool isMouseMoved = false;

        public MainWindow()
        {
            InitializeComponent();

            // 기본 설정
            mapControl.MapProvider = GMapProviders.GoogleSatelliteMap;      // 지도제공자
            mapControl.Position = new PointLatLng(35.164928, 128.127485);   // 초기 위치
            mapControl.MinZoom = 2;
            mapControl.MaxZoom = 20;
            mapControl.Zoom = 15;
            mapControl.ShowCenter = false;
            mapControl.DragButton = MouseButton.Left;
            mapControl.MouseWheelZoomEnabled = true;

            

            mapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
            mapControl.MouseLeftButtonUp += new MouseButtonEventHandler(mapControl_MouseLeftButtonUp);
            mapControl.MouseMove += new MouseEventHandler(mapControl_MouseMove);
            mapControl.MouseWheel += new MouseWheelEventHandler(mapControl_MouseWheel);
            
            

            setCurrentMarker();
            mapComboBoxSetting();

        }

        void setCurrentMarker()
        {
            currentMarker = new GMapMarker(mapControl.Position);


            /*currentMarker.Shape = new Ellipse
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.White),
                Width = 20,
                Height = 20
            };*/

            currentMarker.Shape = new CustomMarkerBlue(this, currentMarker);

            currentMarker.Offset = new Point(-22, -45);
            currentMarker.ZIndex = int.MaxValue;

            mapControl.Markers.Add(currentMarker);
            points.Add(mapControl.Position);
            markers.Add(currentMarker);
        }

        private void mapControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 0)
            {
                zoomSliderBar.Value += 1;
            }
            else
            {
                zoomSliderBar.Value -= 1;
            }
        }

        private void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMousePressed = true;
        }

        private void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(isMousePressed)
            {
                isMouseMoved = true;
            }
        }

        void mapControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isMouseMoved) 
            {
                Point clickPoint = e.GetPosition(mapControl);
                PointLatLng point = mapControl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
                marker = new GMapMarker(point);

                /*marker.Shape = new Ellipse
                {
                    Stroke = Brushes.Yellow,
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(Colors.Black),
                    Width = 20,
                    Height = 20
                };*/

                points.Add(point);
                markers.Add(marker);

                marker.Shape = new CustomMarker(this, marker, points, markers);
                marker.Offset = new Point(-22, -45);

                mapControl.Markers.Add(marker);

                // DrawRoute();
            }
            else 
            {
                isMouseMoved = false;
            }
            isMousePressed = false;
        }

        void mapComboBoxSetting()
        {
            mapComboBox.Items.Add("GoogleSatelliteMap");
            mapComboBox.Items.Add("GoogleMap");
            mapComboBox.Items.Add("GoogleTerrainMap");
            mapComboBox.SelectedIndex = 0;
        }

        private void mapComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Console.WriteLine(mapComboBox.SelectedItem);

            if(mapComboBox.SelectedIndex == 0)
            {
                mapControl.MapProvider = GMapProviders.GoogleSatelliteMap;
            }
            else if(mapComboBox.SelectedIndex == 1)
            {
                mapControl.MapProvider = GMapProviders.GoogleMap;
            }
            else if(mapComboBox.SelectedIndex == 2)
            {
                mapControl.MapProvider = GMapProviders.GoogleTerrainMap;
            }
            else
            {
                mapControl.MapProvider = GMapProviders.GoogleSatelliteMap;
            }
            
        }

        private void zoomSliderBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mapControl.Zoom = e.NewValue;
        }

        void DrawRoute()
        {
            GMapRoute route = new GMapRoute(points);
                       
            Path path = new Path();
            path.Stroke = Brushes.Red;
            path.StrokeThickness = 2;
            path.Effect = null;

            route.Shape = path;

            mapControl.Markers.Add(route);
        }

    }
}