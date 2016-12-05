using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using TanyaOne.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TanyaOne.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public DBViewModel DB => App.MainDbViewModel;
        public MainPageViewModel MPageViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 200));
            MPageViewModel = new MainPageViewModel();
            this.DataContext = MPageViewModel;
            MPageViewModel.ChartControlViewModel = (ChartViewModel)ChartControl12.DataContext;
            MPageViewModel.MVM = new MapViewModel(MapControl);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            MPageViewModel.TileClick(sender, e);
        }
    }
}
