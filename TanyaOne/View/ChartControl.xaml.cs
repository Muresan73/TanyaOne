using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TanyaOne.Model;
using TanyaOne.ViewModel;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TanyaOne.View
{
    public sealed partial class ChartControl : UserControl
    {
        //public ChartViewModel CViewModel => (ChartViewModel) this.DataContext;
        public ChartViewModel CViewModel => (ChartViewModel) this.DataContext;

        public ChartControl()
        {
            this.InitializeComponent();
           // (LineChart2.Series[0] as LineSeries).ItemsSource = CViewModel.VisualDataElements;
            CViewModel.PropertyChanged += updateChart;
            (LineChart2.Series[0] as LineSeries).IndependentAxis =
                new CategoryAxis
                {
                    Orientation = AxisOrientation.X,
                    Height = 0
                };          
        }

        public void updateChart(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VisualDataElements")
            (LineChart2.Series[0] as LineSeries).ItemsSource = CViewModel.VisualDataElements;
        }

    }
}
