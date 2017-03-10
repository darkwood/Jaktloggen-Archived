using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Xamarin.Forms;

namespace Jaktloggen.Views.Selectors
{
    public partial class ArtSelectorPage : ContentPage
    {
        private ArtSelectorVM ViewModel;
        public ArtSelectorPage(Logg currentLogg)
        {
            InitializeComponent();
            BindingContext = ViewModel = new ArtSelectorVM(currentLogg);

            ToolbarItems.Add(new ToolbarItem("+", "add.png", () =>
            {
                Navigation.PushAsync(new ArtPage(new Art()), true);
            }, ToolbarItemOrder.Primary));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.BindData();
        }
        private void ListViewItems_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedArt = ((Art) e.SelectedItem);
                ViewModel.SetArt(selectedArt);
                Navigation.PopAsync(true);
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
