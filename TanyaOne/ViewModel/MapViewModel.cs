using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using TanyaOne.Annotations;
using TanyaOne.Model;

namespace TanyaOne.ViewModel
{
    public class MapViewModel : INotifyPropertyChanged
    {
        public Geopoint Center { get; set; }
        public List<MapElement> Elements { get; set; }
        public Geopath Path { get; set; }
        //Till MapControl Element binding not available
        private MapControl MapControlViewElement { get; }

        public string MapKey => "Atczi54OBH138Uzdr8yW~_stpOFGsfxPD7B7HQWu0Vw~AoSKaPL-tVAO0045sOdIzjlm61ebgady-3RoP2kQD2sVbCxZXRRFVUdMYRGqfIwI";

        public MapViewModel(MapControl mapControlViewElement)
        {
            MapControlViewElement = mapControlViewElement;

            Center = new Geopoint(new BasicGeoposition
            {
                Latitude = 46,
                Longitude = 20

            });

            //MapControlViewElement.MapElements.Clear();

        }

        public void LoadMapElements(List<List<Polygon>> poligonslist, List<FieldLocationData[]> fieldPois)
        {
            MapControlViewElement.MapElements.Clear();
            foreach (var poligons in poligonslist)
            {
                var a = poligons.Select(i => new BasicGeoposition { Latitude = i.lat, Longitude = i.lon }).ToList();
                var path = new Geopath(poligons.Select(i => new BasicGeoposition { Latitude = i.lat, Longitude = i.lon }).ToList());
                var line = new MapPolygon
                {
                    Path = path,
                    StrokeColor = Colors.Green,
                    FillColor = Colors.Teal,
                    StrokeThickness = 2,

                };
                MapControlViewElement.MapElements.Add(line);
            }


            //SetCenter(46.595600128173828, 20.308347702026367);
            foreach (var pois in fieldPois)
            {
                foreach (var poi in pois)
                {
                    MapControlViewElement.MapElements.Add(new MapIcon() { Location = new Geopoint(new BasicGeoposition() { Latitude = poi.lat, Longitude = poi.lon }) });
                }
            }
        }

        public void SetCenter(double lat, double lon)
        {
            Center = new Geopoint(new BasicGeoposition
            {
                Latitude = lat,
                Longitude = lon

            });
            OnPropertyChanged(nameof(Center));
            MapControlViewElement.ZoomLevel = 15;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
