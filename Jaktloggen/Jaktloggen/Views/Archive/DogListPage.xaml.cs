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
    public partial class DogListPage : ContentPage
    {
        private DogListVM ViewModel;
        public DogListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new DogListVM();

            ToolbarItems.Add(new ToolbarItem("+", "add.png", () =>
            {
                Navigation.PushAsync(new DogPage(new Dog()), true);
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
                Navigation.PushAsync(new DogPage((Dog)e.SelectedItem), true);
                ((ListView)sender).SelectedItem = null;
            }
        }

        private void ButtonNewItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DogPage(new Dog()), true);
        }
    }
}
