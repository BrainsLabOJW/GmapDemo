using GmapDemo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GmapDemo.Models;
using System.Collections.ObjectModel;

namespace GmapDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private GMapModel _gmapmodel = new GMapModel();
        
        private ObservableCollection<ItemModel> _items = new();
        private ItemModel _itemModel = new();

        public GMapModel GMapModel
        {
            get => _gmapmodel;
            set => SetProperty(ref _gmapmodel, value);
        }

        public int DefaultZoom
        {
            get { return _gmapmodel.defaultZoom; }
            set { _gmapmodel.defaultZoom = value; OnPropertyChanged(); }
        }

        public int MinimumZoom
        {
            get { return _gmapmodel.minimumZoom; }
            set { _gmapmodel.minimumZoom = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ItemModel> Items
        {
            get { return (ObservableCollection<ItemModel>)_items; }
            set 
            {
                _items = (ObservableCollection<ItemModel>)value; 
                OnPropertyChanged(nameof(Items)); 
            }
        }

        public ItemModel ItemModels
        {
            get { return _itemModel; }
            set
            {
                _itemModel = value;
                OnPropertyChanged(nameof(ItemModels));
            }
        }

        public MainViewModel()
        {
            GMapModel = new GMapModel();
            GMapModel.defaultZoom = 15;
            GMapModel.minimumZoom = 2;
            GMapModel.maximumZoom = 20;


            ItemModels = new ItemModel();

            Items = new ObservableCollection<ItemModel>
            {
                new ItemModel {Id = 0, MapProviderName = "GoogleSatelliteMap"},
                new ItemModel {Id = 1, MapProviderName = "GoogleMap"},
                new ItemModel {Id = 2, MapProviderName = "GoogleTerrainMap"}
            };

        }

    }
}
