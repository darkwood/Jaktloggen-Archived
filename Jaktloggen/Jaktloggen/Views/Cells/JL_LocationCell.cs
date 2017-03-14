//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading.Tasks;
//using ImageCircle.Forms.Plugin.Abstractions;
//using Jaktloggen.Helpers;
//using Xamarin.Forms;
//using XLabs.Forms.Controls;

//namespace Jaktloggen.Views.Cells
//{
//    public class JL_LocationCell : ViewCell
//    {
//        public JL_LocationCell(string latitude, string longitude, EventHandler onLocationFound = null, EventHandler onTapped = null)
//        {
//            View = new StackLayout()
//            {
//                Orientation = StackOrientation.Horizontal,
//                Padding = 10,
//                Children =
//                {
//                    new ActivityIndicator()
//                    {
//                        IsRunning = true
//                    },
//                    new Label()
//                    {
//                        Text = latitude,
//                        VerticalOptions = LayoutOptions.CenterAndExpand,
//                        HorizontalOptions = LayoutOptions.End,
//                        LineBreakMode = LineBreakMode.NoWrap,
//                    },
//                    new Label()
//                    {
//                        Text = longitude,
//                        HorizontalOptions = LayoutOptions.End,
//                        VerticalOptions = LayoutOptions.CenterAndExpand
//                    }
//                }
//            };
//            if (onTapped != null)
//            {
//                Tapped += onTapped;
//            }
//        }

//        private async Task TryGetPosition(string latitude, string longitude)
//        {

//            //var acceptUseGps = await DisplayAlert("Hent posisjon", "Ønsker du å hente posisjon og sted fra GPS?", "Ja", "Nei");
//            //if (acceptUseGps)
//            //{

//            //}
//            ToggleLoadPosition();

//            var position = await XLabsHelper.GetPosition();
//            if (position != null)
//            {
//                VM.CurrentLogg.Latitude = position.Latitude.ToString();
//                VM.CurrentLogg.Longitude = position.Longitude.ToString();

//                VM.Save();
//            }

//            ToggleLoadPosition();
//        }

//        private void ToggleLoadPosition()
//        {
//            VM.IsLoadingPosition = !VM.IsLoadingPosition;
//            PositionActivityIndicator.IsRunning = VM.IsLoadingPosition;
//        }
//    }
//}
