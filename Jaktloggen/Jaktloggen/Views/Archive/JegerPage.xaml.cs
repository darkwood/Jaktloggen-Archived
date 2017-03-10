using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.Helpers;
using Jaktloggen.IO;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Xamarin.Forms;




namespace Jaktloggen.Views
{
    public partial class JegerPage : ContentPage
    {
        private JegerVM ViewModel;
        public JegerPage(Jeger jeger)
        {
            InitializeComponent();
            BindingContext = ViewModel = new JegerVM(jeger);
            Title = jeger.ID == 0 ? "Ny jeger" : jeger.Navn;
        }

        private void EntryCell_OnCompleted(object sender, EventArgs e)
        {
            ViewModel.Save();
        }

        private async void ItemImageCell_OnTapped(object sender, EventArgs e)
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
    }
}
