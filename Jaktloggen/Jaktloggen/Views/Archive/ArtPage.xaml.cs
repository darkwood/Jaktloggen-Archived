using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Xamarin.Forms;




namespace Jaktloggen.Views
{
    public partial class ArtPage : ContentPage
    {
        private ArtVM ViewModel;
        public ArtPage(Art art)
        {
            InitializeComponent();
            BindingContext = ViewModel = new ArtVM(art);
            Title = art.ID == 0 ? "Ny art" : art.Navn;
        }

        private void EntryCell_OnCompleted(object sender, EventArgs e)
        {
            ViewModel.Save();
        }

        private async void ItemImageCell_OnTapped(object sender, EventArgs e)
        {

        }

        private async void ButtonDelete_OnClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlert("Bekreft sletting", "Art og alle koblinger til arten i loggføringer blir slettet.", "Slett", "Avbryt");
            if (ok)
            {
                ViewModel.Delete();
                await Navigation.PopAsync(true);
            }
        }
    }
}
