using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms.Maps;

namespace Jaktloggen.Helpers
{
    public static class XLabsHelper
    {
        public static async Task<MediaFile> SelectPicture()
        {
            throw new NotImplementedException();
            //var device = Resolver.Resolve<IDevice>();
            //var oMediaPicker = device.MediaPicker;
            ////ImageSource imageSource = null;
            //MediaFile mediaFile = null;
            //try
            //{
            //    mediaFile = await oMediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
            //    {
            //        DefaultCamera = CameraDevice.Front,
            //        MaxPixelDimension = 400
            //    });
            //    //imageSource = ImageSource.FromStream(() => mediaFile.Source);
            //    //return imageSource;
            //}
            //catch (Exception ex)
            //{

            //}
            //return mediaFile;
        }

        public static async Task<Position> GetPosition(double desiredAccuracy = 50)
        {
            throw new NotImplementedException();
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
        public string Path { get; set; }
    }
}
