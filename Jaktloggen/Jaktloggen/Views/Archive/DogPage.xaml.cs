using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.IO;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Xamarin.Forms;




namespace Jaktloggen.Views
{
    public partial class DogPage : ContentPage
    {
        private DogVM ViewModel;
        public DogPage(Dog dog)
        {
            InitializeComponent();
            BindingContext = ViewModel = new DogVM(dog);
            Title = dog.ID == 0 ? "Ny dog" : dog.Navn;
        }

        private void EntryCell_OnCompleted(object sender, EventArgs e)
        {
            ViewModel.Save();
        }

        private async void ItemImageCell_OnTapped(object sender, EventArgs e)
        {
            //var device = Resolver.Resolve<IDevice>();
            //var oMediaPicker = device.MediaPicker;
            
            //try
            //{
            //    var mediaFile = await oMediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
            //    {
            //        DefaultCamera = CameraDevice.Front,
            //        MaxPixelDimension = 400
            //    });
                
            //    var imageSource = ImageSource.FromStream(() => mediaFile.Source);
            //    ViewModel.CurrentDog.Image = imageSource;
            //}
            //catch (Exception)
            //{

            //}

        }

        private async void ButtonDelete_OnClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlert("Bekreft sletting", "Dog og alle koblinger til dogen i loggføringer blir slettet.", "Slett", "Avbryt");
            if (ok)
            {
                ViewModel.Delete();
                await Navigation.PopAsync(true);
            }
        }
    }
}
