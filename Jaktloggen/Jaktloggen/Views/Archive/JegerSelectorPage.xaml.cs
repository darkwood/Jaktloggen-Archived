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
    public partial class JegerSelectorPage : ContentPage
    {
        private JegerSelectorVM ViewModel;
        public JegerSelectorPage(Logg currentLogg)
        {
            InitializeComponent();
            BindingContext = ViewModel = new JegerSelectorVM(currentLogg);

            ToolbarItems.Add(new ToolbarItem("+", "add.png", () =>
            {
                Navigation.PushAsync(new JegerPage(new Jeger()), true);
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
                var selectedJeger = ((Jeger) e.SelectedItem);
                ViewModel.SetJeger(selectedJeger);
                Navigation.PopAsync(true);
                ((ListView)sender).SelectedItem = null;
            }
        }
        
    }
}
