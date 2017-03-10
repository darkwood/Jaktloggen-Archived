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
    public partial class ArtListPage : ContentPage
    {
        private ArtListVM ViewModel;
        public ArtListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new ArtListVM();

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
                var art = (Art)e.SelectedItem;
                ViewModel.ArtSelected(art);
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
