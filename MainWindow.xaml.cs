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

        List<GMapMarker> markers = new List<GMapMarker>();

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

            mapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
            setCurrentMarker();
        }


        void setCurrentMarker()
        {
            currentMarker = new GMapMarker(mapControl.Position);
            currentMarker.Shape = new Ellipse
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.White),
                Width = 12,
                Height = 12
            };

            currentMarker.Offset = new Point(-15, -15);
            currentMarker.ZIndex = int.MaxValue;
            mapControl.Markers.Add(currentMarker);
        }

        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(mapControl);
            PointLatLng point = mapControl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
            GMapMarker marker = new GMapMarker(point);

            marker.Shape = new Ellipse 
            { 
                Stroke = Brushes.Yellow,
                StrokeThickness = 2,
                Fill= new SolidColorBrush(Colors.Black),
                Width = 10,
                Height = 10
            };

            mapControl.Markers.Add(marker);
        }

        private void sliderBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}