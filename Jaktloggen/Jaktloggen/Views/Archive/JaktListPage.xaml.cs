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
    public partial class JaktListPage : ContentPage
    {
        private JaktListVM ViewModel;
        public JaktListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new JaktListVM();
            ToolbarItems.Add(new ToolbarItem("+", "add.png", async () =>
            {
                var jakt = await ViewModel.CreateJakt();
                await Navigation.PushAsync(new JaktPageCode(jakt));
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
                Navigation.PushAsync(new JaktPageCode((Jakt)e.SelectedItem));
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
