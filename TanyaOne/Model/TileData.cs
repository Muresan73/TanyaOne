using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TanyaOne.Model
{/*
    public class TileData
    {
        public Sensor[] Sensors { get; set; }
    }*/

    public class Sensor
    {
        public string DisplayNameToShow => displayPrimaryValue != "" ?(displayPrimaryValue + design.siunit):"";
        public ImageSource AssetUri => new BitmapImage(new Uri(string.Format("ms-appx:///Assets/TileIcons/{0}.png", design.icon)));


        public int sensorId { get; set; }
        public string displayName { get; set; }
        public string displayPrimaryValue { get; set; }
        public string displaySecondaryText { get; set; }
        public string displaySecondaryValue { get; set; }
        public Design design { get; set; }
    }

    public class Design
    {
        public string color { get; set; }
        public string icon { get; set; }
        public bool visible { get; set; }
        public float order { get; set; }
        public float? suborder { get; set; }
        public string siunit { get; set; }
        public int rounding { get; set; }
    }

}
