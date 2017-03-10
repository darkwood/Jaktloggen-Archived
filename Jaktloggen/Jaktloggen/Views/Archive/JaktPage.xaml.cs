using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Jaktloggen.Views
{
    public partial class JaktPage : ContentPage
    {
        private JaktVM ViewModel;
        public JaktPage(Jakt jakt)
        {
            InitializeComponent();
            BindingContext = ViewModel = new JaktVM(jakt);
            Title = jakt.ID == 0 ? "Ny jakt" : jakt.Sted;
            
            BindHunters();
            BindDogs();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.BindData();
            MyMap.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                new Position(67.28, 14.404916), Distance.FromMiles(1)));
        }
        
        private void BindHunters()
        {
            HuntersStackLayout.Children.Clear();

            foreach (var jeger in ViewModel.Jegere)
            {
                HuntersStackLayout.Children.Add(CreateRoundedImage(jeger.Image));
            }
        }

        private void BindDogs()
        {
            DogsStackLayout.Children.Clear();
            foreach (var dog in ViewModel.Dogs)
            {
                DogsStackLayout.Children.Add(CreateRoundedImage(dog.Image));
            }
        }
        private static CircleImage CreateRoundedImage(ImageSource imageSource)
        {
            return new CircleImage()
            {
                Source = imageSource,
                HeightRequest = 40,
                WidthRequest = 40,
                BorderThickness = 1,
                Aspect = Aspect.AspectFit,
                Margin = 5,
                BorderColor = Color.White
            };
        }

        private void EntryCell_OnCompleted(object sender, EventArgs e)
        {
            ViewModel.Save();
        }

        private void ItemImageCell_OnTapped(object sender, EventArgs e)
        {
            ViewModel.SelectPicture();
        }

        private async void ButtonDelete_OnClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlert("Bekreft sletting", "Jeger og alle koblinger til jegeren i loggføringer blir slettet.", "Slett", "Avbryt");
            if (ok)
            {
                ViewModel.Delete();
                await Navigation.PopAsync(true);
            }
        }

        private void ItemLoggsCell_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoggListPage(ViewModel.CurrentJakt));
        }

        private void ButtonNewLog_OnClicked(object sender, EventArgs e)
        {
            var logg = new Logg { JaktId = ViewModel.CurrentJakt.ID };
            Navigation.PushAsync(new LoggPage(logg), true);
        }

        private async void ViewCellLocation_OnTapped(object sender, EventArgs e)
        {
            try
            {
                var location = CrossGeolocator.Current;
                location.DesiredAccuracy = 50;
                var position = await location.GetPositionAsync(100000);
                Debug.WriteLine("Position Status: {0}", position.Timestamp);
                Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                Debug.WriteLine("Position Longitude: {0}", position.Longitude);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
