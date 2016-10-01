using System.Collections.Generic;

namespace TanyaOne.ViewModel
{
    public class Record
    {
        public string Name { get; set; }
        public int  Value { get; set; }
    }

    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            ChartData = new List<Record>
            {
                new Record() {Name = "alma", Value = 20},
                new Record() {Name = "alma", Value = 22},
                new Record() {Name = "alma", Value = 23}
            };
        }

        public List<Record> ChartData { get; set; }

       

    }
}
