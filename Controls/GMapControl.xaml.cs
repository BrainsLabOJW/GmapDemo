using System.Windows;
using System.Windows.Controls;

namespace GmapDemo.Controls
{
    /// <summary>
    /// GMapControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GMapControl : UserControl
    {
        public GMapControl()
        {
            InitializeComponent();
        }



        public decimal MinZoomProperty
        {
            get { return (decimal)GetValue(MinZoomPropertyProperty); }
            set { SetValue(MinZoomPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinZoomProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinZoomPropertyProperty =
            DependencyProperty.Register("MinZoomProperty", typeof(decimal), typeof(GMapControl), new PropertyMetadata(0m));

    }
}
