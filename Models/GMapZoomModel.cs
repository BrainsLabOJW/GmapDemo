using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GmapDemo.Models
{
    public class GMapModel : ObservableObject
    {

        private int _defaultZoom;
        private int _minimumZoom;
        private int _maximumZoom;

        public int defaultZoom
        {
            get { return _defaultZoom; }
            set { SetProperty(ref _defaultZoom, value); }
        }

        public int minimumZoom
        {
            get { return _minimumZoom; }
            set { SetProperty(ref _minimumZoom, value); }
        }

        public int maximumZoom
        {
            get { return _maximumZoom; }
            set { SetProperty(ref _maximumZoom, value); }
        }
    }
}
