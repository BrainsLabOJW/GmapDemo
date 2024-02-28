using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GmapDemo.Base
{
    public class ViewModelBase : ObservableObject, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;

        public new void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
