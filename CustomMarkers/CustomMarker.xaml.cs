using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GmapDemo.CustomMarkers
{
    /// <summary>
    /// CustomMarker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomMarker
    {
        private GMapMarker _marker;
        private MainWindow _mainWindow;
        private List<PointLatLng> _points;
        private List<GMapMarker> _markerList;

        private GMapMarker oldValue;
        private GMapRoute route;

        public CustomMarker(MainWindow window, GMapMarker marker, List<PointLatLng> points, List<GMapMarker> markerList)
        {
            InitializeComponent();

            _mainWindow = window;
            _marker = marker;
            _points = points;
            _markerList = markerList;

            MouseLeftButtonUp += CustomMarker_MouseLeftButtonUp;
            MouseLeftButtonDown += CustomMarker_MouseLeftButtonDown;
            MouseMove += CustomMarker_MouseMove;


            DrawRoute(_points);
        }

        private void CustomMarker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
                _mainWindow.mapControl.CanDragMap = true;
            }

        }

        private void CustomMarker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!IsMouseCaptured) 
            {
                Mouse.Capture(this);
            }

            oldValue = _marker;
        }

        private void CustomMarker_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured) 
            { 
                var position = e.GetPosition(_mainWindow.mapControl);
                _marker.Position = _mainWindow.mapControl.FromLocalToLatLng((int)position.X, (int)position.Y);

                _mainWindow.mapControl.CanDragMap = false;
            }

            for (int i = 0; i < _markerList.Count; i++)
            {
                _points[i] = _markerList[i].Position;
            }

            DrawRoute(_points);

        }


        void DrawRoute(List<PointLatLng> pointList)
        {
            // 요소 초기화
            _mainWindow.mapControl.Markers.Clear();

            // 마커 추가
            foreach(GMapMarker marker in _markerList) 
            {
                _mainWindow.mapControl.Markers.Add(marker);
            }
            
            // 경로 추가
            route = new GMapRoute(pointList);

            Path path = new Path();
            path.Stroke = Brushes.Red;
            path.StrokeThickness = 2;
            path.Effect = null;

            route.Shape = path;

            _mainWindow.mapControl.Markers.Add(route);
        }
    }
}
