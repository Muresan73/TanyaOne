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
        public object SelectedNode { get; set; }
        public FieldLocationData[] NodeList => (SelectedField as FieldData)?.locations;
        public object SelectedField { get; set; }
        public ObservableCollection<FieldData> Fields { get; set; }
        public ObservableCollection<SumRow> SummaryRowDatas { get; set; }
        public ObservableCollection<Sensor> Tiles { get; set; }

        public MapViewModel MVM { get; set; }
        public ChartViewModel ChartControlViewModel { get; set; }

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

                OnPropertyChanged(nameof(Fields));
                OnPropertyChanged(nameof(SelectedField));

                MVM.LoadMapElements(Fields.Select(i => i.polygon).ToList(),Fields.Select(i => i.locations).ToList());
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
            OnPropertyChanged(nameof(Tiles));
        }

        public async void UserLogOut()
        {
            await App.MainDbViewModel.LogOut();
            ((Frame)Window.Current.Content).Navigate(typeof(LoginView));
        }

        public void LocationSelected()
        {
            if (SelectedField == null) return;

            FieldLocationData location = NodeList.FirstOrDefault();//SummaryRowDatas.FirstOrDefault(data => data.subTitle == (SelectedField as FieldData)?.name).locationId;
            OnPropertyChanged(nameof(NodeList));
            SelectedNode = NodeList[0];
            OnPropertyChanged(nameof(SelectedNode));

            MVM.SetCenter(location.lat,location.lon);

            NodeSelected();
        }

        public void NodeSelected()
        {
            if(SelectedNode != null)
            RefreshTileData((SelectedNode as FieldLocationData).id);
        }

        public async void TileClick(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog(((sender as Button).DataContext as Sensor).displayName);
            await dialog.ShowAsync();
            //int indexOfTile = Tiles.IndexOf(((Button) sender).DataContext as Sensor);
            int sensorid = ((sender as Button)?.DataContext as Sensor).sensorId;
            int locasionId = (SelectedNode as FieldLocationData).id;
            
            // TODO 
            var chartData = await App.MainDbViewModel.GetChartDataAsync(locasionId, sensorid, 1);
            ChartControlViewModel.UpdateChartData(chartData.data,chartData.tiles);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
