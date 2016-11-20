using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyaOne.Model
{

    public class SummaryData
    {
        public SumColumn[] columns { get; set; }
        public SumRow[] rows { get; set; }
    }

    public class SumColumn
    {
        public string title { get; set; }
        public string subTitle { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public int id { get; set; }
    }

    public class SumRow
    {
        public string title { get; set; }
        public string subTitle { get; set; }
        public int locationId { get; set; }
        public SumDatum[] data { get; set; }
    }

    public class SumDatum
    {
        public int id { get; set; }
        public string value { get; set; }
        public bool drawsIcon { get; set; }
    }

}
