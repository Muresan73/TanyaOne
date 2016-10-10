using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace TanyaOne.ViewModel
{

    public class MainPageViewModel
    {
        public ObservableCollection<Button> Tiles { get; set; }

        public MainPageViewModel()
        {
            
            Tiles = new ObservableCollection<Button>()
            {
                new Button() {Content = "B1"},
                new Button() {Content = "B2"},
                new Button() {Content = "B3"},
                new Button() {Content = "B4"},
                new Button() {Content = "B5"},
            };
        }
    }
}
