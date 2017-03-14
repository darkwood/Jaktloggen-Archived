using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Jaktloggen.Models;
using Jaktloggen.Views.Extended;
using MvvmHelpers;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Jaktloggen.Views.Input
{
    [ImplementPropertyChanged]
    public class PositionPageVM
    {
        public Position Position { get; set; }
        public string Status { get; set; } = "Sett posisjon";
        public PositionPageVM(Position position)
        {
            Position = position;
        }
    }
    public class PositionPage : Base.ContentPageJL
    {
        public Position Position { get; set; }
        private Action<PositionPage> _callback;
        private IPosition _page;

        public ExtendedMap CurrentMap { get; set; }
        public PositionPageVM VM { get; set; }
        public PositionPage(IPosition page, Action<PositionPage> callback)
        {
            _page = page;
            double lat, lon;
            if (double.TryParse(page.Latitude, out lat) && double.TryParse(page.Longitude, out lon))
            {
                Position = new Position(lat, lon);
            }

            BindingContext = VM = new PositionPageVM(Position);

            _callback = callback;
            InitMap();
        }

        
        private void InitMap()
        {
            
            CurrentMap = new ExtendedMap()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            SetPinAtPosition();
            CurrentMap.Tap += MapOnTap;
            CurrentMap.IsShowingUser = true;

            var btnSave = new Button() { Text = "Lagre" };
            btnSave.Clicked += delegate (object sender, EventArgs args)
            {
                _callback(this);
                Navigation.PopModalAsync(true);
            };
            var btnCancel = new Button() { Text = "Avbryt" };
            btnCancel.Clicked += delegate (object sender, EventArgs args)
            {
                Navigation.PopModalAsync(true);
            };

            var lblStatus = new Label()
            {
                FontSize = 12,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            lblStatus.SetBinding(Label.TextProperty, "Status");
            var stackLayout = new StackLayout()
            {
                Children =
                {
                    CurrentMap,
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            btnCancel,
                            lblStatus,
                            btnSave
                        }
                    }
                }
            };

            Content = stackLayout;

        }
        
        private async void MapOnTap(object sender, TapEventArgs tapEventArgs)
        {
            //var ok = await DisplayAlert("Flyttet markør", "Ønsker du å flytte markøren hit?", "OK", "Avbryt");
            //if (ok)
            //{
                Position = tapEventArgs.Position;
                SetPinAtPosition();
            //}
        }

        //todo: make MapOnZoom event and store radius value (distance)

        private void SetPinAtPosition()
        {
            if (CurrentMap.Pins.Count > 0)
            {
                VM.Status = "Ny posisjon satt";
                CurrentMap.Pins.Clear();
            }
            
            CurrentMap.Pins.Add(CreatePin());

            CurrentMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    Position,
                    Distance.FromKilometers(2)
                )
            );
            
            VM.Position = Position;
        }

        private Pin CreatePin()
        {
            return new Pin
            {
                Type = PinType.Place,
                Position = Position,
                Label = "Valgt posisjon"
            };
        }
    }
}
