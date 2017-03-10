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
    public partial class DogSelectorPage : ContentPage
    {
        private DogSelectorVM ViewModel;
        public DogSelectorPage(Logg currentLogg)
        {
            InitializeComponent();
            BindingContext = ViewModel = new DogSelectorVM(currentLogg);

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
                var selectedDog = ((Dog) e.SelectedItem);
                ViewModel.SetDog(selectedDog);
                Navigation.PopAsync(true);
                ((ListView)sender).SelectedItem = null;
            }
        }

        private void ButtonNewItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DogPage(new Dog()), true);
        }

    }
}
