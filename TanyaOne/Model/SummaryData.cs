using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyaOne.Model
{

    public class SummaryData
    {
        public Column[] columns { get; set; }
        public Row[] rows { get; set; }
    }

    public class Column
    {
        public string title { get; set; }
        public string subTitle { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public int id { get; set; }
    }

    public class Row
    {
        public string title { get; set; }
        public string subTitle { get; set; }
        public int locationId { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string value { get; set; }
        public bool drawsIcon { get; set; }
    }

}
