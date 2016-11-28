using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TanyaOne.Model
{
        public class FieldData
        {
            public int id { get; set; }
            public string name { get; set; }
            public string plant { get; set; }
            public List<Polygon> polygon { get; set; }
            public List<object> rows { get; set; }
            public FieldLocationData[] locations { get; set; }
            public bool _default { get; set; }
        }

        public class Polygon
        {
            public float lon { get; set; }
            public float lat { get; set; }
        }

        public class FieldLocationData
        {
            public int fieldId { get; set; }
            public string name { get; set; }
            public float lat { get; set; }
            public float lon { get; set; }
            public long startTime { get; set; }
            public int endTime { get; set; }
            public string info { get; set; }
            public int type { get; set; }
            public bool _default { get; set; }
            public int id { get; set; }
        }

    

}
