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
    public partial class JegerListPage : ContentPage
    {
        private JegerListVM ViewModel;
        public JegerListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new JegerListVM();

            ToolbarItems.Add(new ToolbarItem("+", "add.png", () =>
            {
                Navigation.PushAsync(new JegerPageCode(new Jeger()), true);
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
                Navigation.PushAsync(new JegerPageCode((Jeger)e.SelectedItem), true);
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
