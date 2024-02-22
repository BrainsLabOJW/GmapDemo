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
    /// CustomMarkerBlue.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomMarkerBlue
    {

        private GMapMarker _marker;
        private MainWindow _mainWindow;

        public CustomMarkerBlue(MainWindow window, GMapMarker marker)
        {
            InitializeComponent();

            _mainWindow = window;
            _marker = marker;
        }
    }
}
