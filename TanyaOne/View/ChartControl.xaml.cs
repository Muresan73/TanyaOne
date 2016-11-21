using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TanyaOne.ViewModel;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TanyaOne.View
{
    public sealed partial class ChartControl : UserControl
    {
        //public ChartViewModel CViewModel => (ChartViewModel) this.DataContext;
        public ChartViewModel CViewModel { get; }

        public ChartControl()
        {
            this.InitializeComponent();
            CViewModel = new ChartViewModel();
            (LineChart2.Series[0] as LineSeries).ItemsSource = CViewModel.VisualDataElements;

            (LineChart2.Series[0] as LineSeries).IndependentAxis =
                new CategoryAxis
                {
                    Orientation = AxisOrientation.X,
                    Height = 0
                };
        }
    }
}
