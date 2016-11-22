using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TanyaOne.Annotations;
using TanyaOne.Model;

namespace TanyaOne.ViewModel
{
    public class ChartViewModel:INotifyPropertyChanged
    {
        public bool isValid { get; set; }
        public List<Datum> VisualDataElements { get; set; }
        public Tile[] ChartTiles { get; set; }

        public ChartViewModel()
        {


        }

        public void UpdateChartData(Datum[] dates, Tile[] tiles)
        {
            ChartTiles = new Tile[4];
            for (int i = 0; i < 4; i++)
            {
                ChartTiles[i] = tiles.Length > i ? tiles[i] : new Tile() {text = "", value = ""};
            }
            VisualDataElements = new List<Datum>(dates);
            OnPropertyChanged(nameof(VisualDataElements));
            OnPropertyChanged(nameof(ChartTiles));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
