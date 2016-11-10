using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using TanyaOne.Annotations;
using TanyaOne.Model;

namespace TanyaOne.ViewModel
{

    public class MainPageViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Button> Tiles { get; set; }
        public Object SelectedField { get; set; }
        public ObservableCollection<FieldData> Fields { get; set; }

        public MainPageViewModel()
        {
            LoadData();
            Fields = new ObservableCollection<FieldData>();
            Tiles = new ObservableCollection<Button>()
            {
                new Button() {Content = "B1"},
                new Button() {Content = "B2"},
                new Button() {Content = "B3"},
                new Button() {Content = "B4"},
                new Button() {Content = "B5"},
            };
        }

        private async void LoadData()
        {
            var fieldsList = await App.MainDbViewModel.GetFieldListAsync();
            Fields = new ObservableCollection<FieldData>(fieldsList);
            //var dialog = new MessageDialog(Fields[0].name);
            //await dialog.ShowAsync();
            if (SelectedField == null)
                SelectedField = Fields[0];
            OnPropertyChanged("Fields");
            OnPropertyChanged("SelectedField");

        }

        public async void showselect()
        {
            var dialog = new MessageDialog((SelectedField as FieldData).name);
            await dialog.ShowAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
