using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Xamarin.Forms;

namespace Jaktloggen.Views
{
    public partial class LoggPage : ContentPage
    {
        private LoggVM ViewModel;
        
        public LoggPage(Logg logg)
        {
            InitializeComponent();
            BindingContext = ViewModel = new LoggVM(logg);
            Title = logg.ID == 0 ? "Ny loggføring" : "Loggføring";

            for (int i = 0; i <= 50; i++)
            {
                PickerSett.Items.Add(i.ToString());
                PickerSkudd.Items.Add(i.ToString());
                PickerTreff.Items.Add(i.ToString());
            }
        }
        

        protected override void OnAppearing()
        {
            ViewModel.BindData();
            BindingContext = ViewModel = new LoggVM(ViewModel.CurrentLogg);
            base.OnAppearing();
        }

        private void ItemImageCell_OnTapped(object sender, EventArgs e)
        {
            ViewModel.SelectPicture();
        }

        private void Jeger_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Selectors.JegerSelectorPage(ViewModel.CurrentLogg));
        }
        private void Dog_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Selectors.DogSelectorPage(ViewModel.CurrentLogg));
        }
        private void Art_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Selectors.ArtSelectorPage(ViewModel.CurrentLogg));
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.Save();
            BindingContext = ViewModel = new LoggVM(ViewModel.CurrentLogg);
        }

        private async void LocationCell_OnTapped(object sender, EventArgs e)
        {
            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;

            //var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            //Debug.WriteLine("Position Status: {0}", position.Timestamp);
            //Debug.WriteLine("Position Latitude: {0}", position.Latitude);
            //Debug.WriteLine("Position Longitude: {0}", position.Longitude);
        }
    }
}
