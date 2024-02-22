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

        public CustomMarker(MainWindow window, GMapMarker marker, List<PointLatLng> points)
        {
            InitializeComponent();

            _mainWindow = window;
            _marker = marker;

            MouseLeftButtonUp += CustomMarker_MouseLeftButtonUp;
            MouseLeftButtonDown += CustomMarker_MouseLeftButtonDown;
            MouseMove += CustomMarker_MouseMove;

            Console.WriteLine(String.Join("\n", points));

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
        }

        private void CustomMarker_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured) 
            { 
                var position = e.GetPosition(_mainWindow.mapControl);
                _marker.Position = _mainWindow.mapControl.FromLocalToLatLng((int)position.X, (int)position.Y);

                _mainWindow.mapControl.CanDragMap = false;


                Console.WriteLine(_marker.Position);
            }

        }
    }
}
