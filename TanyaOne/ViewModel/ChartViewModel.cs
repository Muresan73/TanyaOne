using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanyaOne.Model;

namespace TanyaOne.ViewModel
{
    public class ChartViewModel
    {
        public bool isValid { get; set; }
        public List<Datum> VisualDataElements { get; set; }
        public Tile[] ChartTiles { get; set; }

        public ChartViewModel()
        {
            var visualDataElements = new Datum[]{ new Datum() { date = "k0rte", value = 3 }, new Datum() { date = "cser", value = 4 }, new Datum() { date = "alma", value = 2 }, new Datum() { date = "has", value = 1 } };
            var chartTiles = new[] { new Tile() { text = "08:88", value = "5" }, new Tile() { text = "08:90", value = "10" }, new Tile() { text = "09:88", value = "8" } };

            UpdateChartData(visualDataElements,chartTiles);

        }

        public void UpdateChartData(Datum[] dates, Tile[] tiles)
        {
            ChartTiles = new Tile[4];
            for (int i = 0; i < 4; i++)
            {
                ChartTiles[i] = tiles.Length > i ? tiles[i] : new Tile() {text = "", value = ""};
            }
            VisualDataElements = new List<Datum>(dates);
        }
    }
}
