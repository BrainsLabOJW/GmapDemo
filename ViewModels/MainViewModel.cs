using GmapDemo.Base;
using GmapDemo.Models;
using System.Collections.ObjectModel;

namespace GmapDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region properties
        private GMapModel _gmapmodel = new GMapModel();
        
        private ObservableCollection<ComboBoxItemModel> _items = new();
        private ComboBoxItemModel _comboBoxItemModel = new();
        #endregion


        public GMapModel GMapModel
        {
            get => _gmapmodel;
            set => SetProperty(ref _gmapmodel, value);
        }

        #region ItemModel
        public ObservableCollection<ComboBoxItemModel> Items
        {
            get { return (ObservableCollection<ComboBoxItemModel>)_items; }
            set 
            {
                _items = (ObservableCollection<ComboBoxItemModel>)value; 
                OnPropertyChanged(nameof(Items)); 
            }
        }

        public ComboBoxItemModel ComboBoxItemModels
        {
            get { return _comboBoxItemModel; }
            set
            {
                _comboBoxItemModel = value;
                OnPropertyChanged(nameof(ComboBoxItemModels));
            }
        }
        #endregion



        public MainViewModel()
        {
            GMapModel = new GMapModel();
            GMapModel.defaultZoom = 15;
            GMapModel.minimumZoom = 2;
            GMapModel.maximumZoom = 20;


            ComboBoxItemModels = new ComboBoxItemModel();

            Items = new ObservableCollection<ComboBoxItemModel>
            {
                new ComboBoxItemModel {Id = 0, MapProviderName = "GoogleSatelliteMap"},
                new ComboBoxItemModel {Id = 1, MapProviderName = "GoogleMap"},
                new ComboBoxItemModel {Id = 2, MapProviderName = "GoogleTerrainMap"}
            };
        }
    }
}