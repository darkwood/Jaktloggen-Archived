using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Jaktloggen.Interfaces;
using Jaktloggen.IO;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Jaktloggen.Helpers
{
    public static class XLabsHelper
    {
        public static async Task<Stream> SelectPicture()
        {
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                return stream;
            }
            return null;
        }

        public static async Task<Position> GetPosition(double desiredAccuracy = 50)
        {
            return await Task.FromResult(new Position(0,0));
            //var geolocator = Resolver.Resolve<IGeolocator>();

            //Position position = null;
            //if (geolocator.IsGeolocationEnabled)
            //{
            //    geolocator.DesiredAccuracy = desiredAccuracy;
            //    if (!geolocator.IsListening)
            //        geolocator.StartListening(1000, 200);

            //    position = await geolocator.GetPositionAsync(4000);
            //    geolocator.StopListening();
            //}
            //return position;
        }


        public static async Task<string> GetLocationNameForPosition(double latitude, double longitude)
        {
            string sted = string.Empty;
            try
            {
                var geoCoder = new Geocoder();
                var geoPos = new Xamarin.Forms.Maps.Position(latitude, longitude);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(geoPos);
                if (possibleAddresses.Any())
                {
                    sted = possibleAddresses.First();
                    if (sted.IndexOf(Environment.NewLine) > 0) //removes line 2
                    {
                        sted = sted.Substring(0, sted.IndexOf(Environment.NewLine));
                    }
                    if (sted.Length > 5 && Regex.IsMatch(sted, "^\\d{4}[\" \"]")) //removes zipcode
                    {
                        sted = sted.Substring(5);
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Log this
            }
            return sted;
        }
    }

    public class MediaFile
    {
        public MediaFile(string path)
        {
            Path = path;
        }
        public string Path { get; set; }
    }
}
