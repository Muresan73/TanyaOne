using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace TanyaOne.ViewModel
{
    public class MapViewModel
    {
        public Geopoint center { get; set; }
        public List<MapElement> Elements { get; set; }
        public Geopath Path { get; set; }
        //Till MapControl Element binding not available
        private MapControl MapControlViewElement { get; }

        public string MapKey =>"Atczi54OBH138Uzdr8yW~_stpOFGsfxPD7B7HQWu0Vw~AoSKaPL-tVAO0045sOdIzjlm61ebgady-3RoP2kQD2sVbCxZXRRFVUdMYRGqfIwI";

        public MapViewModel( MapControl mapControlViewElement)
        {
            this.MapControlViewElement = mapControlViewElement;

            center = new Geopoint(new BasicGeoposition()
            {
                Latitude = 48.8530,
                Longitude = 2.3498

            });
            List<BasicGeoposition> positions = new List<BasicGeoposition>();
            positions.Add(new BasicGeoposition() { Latitude = 48, Longitude = 2 });   //<== this
            positions.Add(new BasicGeoposition() { Latitude = 49, Longitude = 2 });  //<== and this
            positions.Add(new BasicGeoposition() { Latitude = 49, Longitude = 3 });  //<== and this
            Geopath path = new Geopath(positions);
            var line = new MapPolygon()
            {
                Path = path,
                StrokeColor = Windows.UI.Colors.Red,
                FillColor = Colors.Wheat,
                StrokeThickness = 2,
                
            };
            MapControlViewElement.MapElements.Add(line);
            // Set your path

        }
    }
}
