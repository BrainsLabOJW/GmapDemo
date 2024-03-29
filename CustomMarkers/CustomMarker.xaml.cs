﻿using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace GmapDemo.CustomMarkers
{
    /// <summary>
    /// CustomMarker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomMarker
    {
        #region properties
        private GMapMarker _marker;
        private MainWindow _mainWindow = new MainWindow();
        private List<PointLatLng> _points = new List<PointLatLng>();
        private List<GMapMarker> _markerList = new List<GMapMarker>();

        private GMapMarker? oldValue;
        private GMapRoute? route;
        #endregion


        public CustomMarker(MainWindow window, GMapMarker marker, List<PointLatLng> points, List<GMapMarker> markerList)
        {
            InitializeComponent();

            _mainWindow = window;
            _marker = marker;
            _points = points;
            _markerList = markerList;

            MouseLeftButtonUp += CustomMarkerMouseLeftButtonUp;
            MouseLeftButtonDown += CustomMarkerMouseLeftButtonDown;
            MouseMove += CustomMarkerMouseMove;


            DrawRoute(_points);
        }

        private void CustomMarkerMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
                _mainWindow.mapControl.CanDragMap = true;
            }

        }

        private void CustomMarkerMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!IsMouseCaptured) 
            {
                Mouse.Capture(this);
            }
            oldValue = _marker;
        }

        private void CustomMarkerMouseMove(object sender, MouseEventArgs e)
        {
            // 마커를 클릭한 상태로 마우스를 움직일 때 마커도 이동되게끔
            if(e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured) 
            { 
                var position = e.GetPosition(_mainWindow.mapControl);
                _marker.Position = _mainWindow.mapControl.FromLocalToLatLng((int)position.X, (int)position.Y);

                _mainWindow.mapControl.CanDragMap = false;
            }

            // 마커 움직임에 따라서 좌표 배열의 값도 바뀌게 함
            for (int i = 0; i < _markerList.Count; i++)
            {
                _points[i] = _markerList[i].Position;
            }

            // 마커 움직임에 따라 경로 표시도 새로 그림
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
            path.Stroke = Brushes.Red;  // 색상
            path.StrokeThickness = 2;   // 선 굵기
            path.Effect = null;

            route.Shape = path;

            _mainWindow.mapControl.Markers.Add(route);

        }
    }
}



