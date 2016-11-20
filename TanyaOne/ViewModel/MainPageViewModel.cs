using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TanyaOne.Annotations;
using TanyaOne.Model;
using TanyaOne.View;

namespace TanyaOne.ViewModel
{

    public class MainPageViewModel:INotifyPropertyChanged
    {
        public object SelectedField { get; set; }
        public ObservableCollection<FieldData> Fields { get; set; }
        public ObservableCollection<SumRow> SummaryRowDatas { get; set; }
        public ObservableCollection<Sensor> Tiles { get; set; }

        public MainPageViewModel()
        {
            Tiles = new ObservableCollection<Sensor>() {new Sensor() {displayName = "krumpli",displayPrimaryValue = "110",design = new Design() {icon = "kolbász",color = "red"} } };
            LoadData();
        }

        private async void LoadData()
        {
            var fieldsList = await App.MainDbViewModel.GetFieldListAsync();
            var summarydata = await App.MainDbViewModel.GetSummaryAsync();


            if (fieldsList == default(FieldData[]) || summarydata == default(SummaryData))
            {
                if (!App.MainDbViewModel.IsLoggedIn) UserLogOut();
                else
                {
                    var dialog = new MessageDialog("Can not retrieve data.\nTry again later");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                SummaryRowDatas = new ObservableCollection<SumRow>(summarydata.rows);
                Fields = new ObservableCollection<FieldData>(fieldsList);
                //var dialog = new MessageDialog(Fields[0].name);
                //await dialog.ShowAsync();
                //RefreshTileData(SummaryRowDatas.FirstOrDefault().locationId);

                if (SelectedField == null)
                    SelectedField = Fields[0];

                OnPropertyChanged("Fields");
                OnPropertyChanged("SelectedField");
            }

        }

        private async void RefreshTileData(int id)
        {
            if (SummaryRowDatas.Count > 0)
            {
                var tiles = await App.MainDbViewModel.GetTileDataAsync(id);
                Tiles = new ObservableCollection<Sensor>(tiles);
               // OnPropertyChanged("SelectedField");
                var dialog = new MessageDialog(tiles.First().displayName);
                await dialog.ShowAsync();
            }
            OnPropertyChanged("Tiles");
        }

        public async void UserLogOut()
        {
            await App.MainDbViewModel.LogOut();
            ((Frame)Window.Current.Content).Navigate(typeof(LoginView));
        }
        

        public async void testclick()
        {
            var chart = await App.MainDbViewModel.GetChartDataAsync(5, 8, 1);
            var dialog = new MessageDialog(chart.tiles.FirstOrDefault().text);
            await dialog.ShowAsync();
        }

        public void LocationSelected()
        {
            if (SelectedField == null) return;
            
            var location = SummaryRowDatas.FirstOrDefault(data => data.subTitle == (SelectedField as FieldData)?.name).locationId;
            RefreshTileData(location);
        }


        public async void TileClick(object sender, RoutedEventArgs e)
        {
            int indexOfTile = Tiles.IndexOf(((Button) sender).DataContext as Sensor);
            var dialog = new MessageDialog(((sender as Button).DataContext as Sensor).displayName);
            await dialog.ShowAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
