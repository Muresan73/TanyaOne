using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyaOne.Model
{
    public class ChartData
    {
        public string type { get; set; }
        public Datum[] data { get; set; }
        public Tile[] tiles { get; set; }
    }

    public class Datum
    {
        public string date { get; set; }
        public float value { get; set; }
    }

    public class Tile
    {
        public string text { get; set; }
        public string value { get; set; }
    }

}
