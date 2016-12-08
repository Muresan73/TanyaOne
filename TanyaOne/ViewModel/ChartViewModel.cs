using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using TanyaOne.Annotations;
using TanyaOne.Model;

namespace TanyaOne.ViewModel
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        public Visibility isVisible { get; set; }
        public string Title { get; private set; }
        public List<Datum> VisualDataElements { get; private set; }
        public Tile[] ChartTiles { get; private set; }
        public string FirstDate => VisualDataElements!=null ? VisualDataElements.FirstOrDefault().date : "";
        public string LastDate => VisualDataElements != null ? VisualDataElements.LastOrDefault().date : "";

        public ChartViewModel()
        {
            isVisible = Visibility.Collapsed;
            OnPropertyChanged(nameof(isVisible));
        }

        public void UpdateChartData(string title, Datum[] dates, Tile[] tiles)
        {
            isVisible = Visibility.Collapsed;
            OnPropertyChanged(nameof(isVisible));
            Title = title;
            ChartTiles = new Tile[4];
            for (int i = 0; i < 4; i++)
            {
                ChartTiles[i] = tiles.Length > i ? tiles[i] : new Tile() { text = "", value = "" };
            }
            VisualDataElements = new List<Datum>(dates);
            isVisible = Visibility.Visible;

            OnPropertyChanged(nameof(VisualDataElements));
            OnPropertyChanged(nameof(ChartTiles));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(isVisible));
            OnPropertyChanged(nameof(FirstDate));
            OnPropertyChanged(nameof(LastDate));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void hide()
        {
            isVisible = Visibility.Collapsed;
            OnPropertyChanged(nameof(isVisible));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
