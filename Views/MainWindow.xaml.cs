using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GmapDemo.CustomMarkers;
using GmapDemo.Models;
using GmapDemo.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace GmapDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region properties
        GMapMarker? currentMarker;  // 기본 위치 마커
        GMapMarker? marker;

        List<PointLatLng> points = new List<PointLatLng>();     // 마커 좌표 배열
        List<GMapMarker> markers = new List<GMapMarker>();      // 마커 배열

        private bool isMousePressed = false;    // 마커를 클릭 했는지 판단
        private bool isMouseMoved = false;      // 마커를 드래그 하고 있는지 판단

        string? routeText = string.Empty;    // 불러온 경로 파일 내용

        MainViewModel model = new MainViewModel();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = model;

            // 기본 설정
            mapControl.MapProvider = GMapProviders.GoogleSatelliteMap;      // 지도제공자 설정
            mapControl.Position = new PointLatLng(35.164928, 128.127485);   // 기본 위치 설정

            mapControl.MinZoom = model.GMapModel.minimumZoom;
            mapControl.MaxZoom = model.GMapModel.maximumZoom;
            mapControl.Zoom = model.GMapModel.defaultZoom;

            mapControl.ShowCenter = false;              // 지도 가운데 +표시 허용 여부
            mapControl.DragButton = MouseButton.Left;   // 마우스 좌클릭으로 지도 이동함
            mapControl.MouseWheelZoomEnabled = true;    // 마우스 휠로 지도 줌인/아웃 허용 여부

            // 마우스 이벤트 등록
            mapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
            mapControl.MouseLeftButtonUp += new MouseButtonEventHandler(mapControl_MouseLeftButtonUp);
            mapControl.MouseMove += new MouseEventHandler(mapControl_MouseMove);
            mapControl.MouseWheel += new MouseWheelEventHandler(mapControl_MouseWheel);

            // 맵 제공자 콤보박스 기본 값 설정
            mapComboBox.SelectedIndex = 0;

            // 기본 위치에 표시할 마커 설정
            setCurrentMarker();
        }


        void setCurrentMarker()
        {
            currentMarker = new GMapMarker(mapControl.Position);

            currentMarker.Shape = new CustomMarkerBlue(this, currentMarker);

            currentMarker.Offset = new Point(-22, -45);
            currentMarker.ZIndex = int.MaxValue;

            mapControl.Markers.Add(currentMarker);  // 지도에 해당 마커 추가
            points.Add(mapControl.Position);    // 좌표 배열에 값 추가
            markers.Add(currentMarker);         // 마커 배열에 추가
        }

        #region Mouse Evnet

        private void mapControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 0) // 휠 움직임에 따라 슬라이더 값도 바뀌게 함
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
            // 마우스를 단순히 클릭했을 때만 마커가 찍히게 함
            if (!isMouseMoved) 
            {
                Point clickPoint = e.GetPosition(mapControl);
                PointLatLng point = mapControl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
                marker = new GMapMarker(point);

                points.Add(point);
                markers.Add(marker);

                marker.Shape = new CustomMarker(this, marker, points, markers);
                marker.Offset = new Point(-22, -45);
                marker.ZIndex = int.MaxValue;

                mapControl.Markers.Add(marker);
            }
            else // 마우스 이동 중에 up이벤트가 발생하면 마커가 생성되지 않도록 처리 
            {
                isMouseMoved = false;
            }

            isMousePressed = false;
        }
        #endregion

        private void mapComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 콤보박스 index 값에 따라 지도타입(제공자)가 바뀌도록 설정

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
            mapControl.Zoom = e.NewValue; // 슬라이더 값과 같이 지도 zoom 값이 변함
        }

        #region 경로 저장/불러오기
        private void SaveRoute(object sender, RoutedEventArgs e)
        {
            object routeJson = JToken.Parse(JsonConvert.SerializeObject(points));   // 배열을 json으로 변환
            Console.WriteLine(routeJson);
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();   // 파일탐색기
            saveFileDialog.Filter = "JSON File(*.json)|*.json";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // 파일 탐색기 기본 위치 설정

            if(saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, routeJson.ToString());
            }
        }

        private void LoadRoute(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files | *.json";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);     // 위치: 내 문서

            if(openFileDialog.ShowDialog() == true)
            {
                
                points.Clear();     // 좌표 배열 비움  
                markers.Clear();    // 마커 배열 비움


                routeText = File.ReadAllText(openFileDialog.FileName);

                List<PointLatLng>? results = JsonConvert.DeserializeObject<List<PointLatLng>>(routeText);   // 파일 내용을 다시 배열로 바꿔서 좌표 배열에 넣어줌
                
                if(results != null)
                {
                    points = results;
                }


                // 맵 요소(마커,경로) 비움
                mapControl.Markers.Clear();

                if (points != null)
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        marker = new GMapMarker(points[i]);
                        markers.Add(marker);

                        if (i == 0)
                        {
                            marker.Shape = new CustomMarkerBlue(this, marker);  // 첫번째 인덱스(초기 위치)만 다른 마커로 표시
                        }
                        else
                        {
                            marker.Shape = new CustomMarker(this, marker, points, markers);
                        }

                        marker.Offset = new Point(-22, -45);
                        marker.ZIndex = int.MaxValue;

                        mapControl.Markers.Add(marker);
                    }
                }
            }
        }
        #endregion
    }
}