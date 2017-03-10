using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Xamarin.Forms;

namespace Jaktloggen.Views
{
    public partial class LoggListPage : ContentPage
    {
        private JaktVM ViewModel;
        public LoggListPage(Jakt jakt)
        {
            InitializeComponent();
            BindingContext = ViewModel = new JaktVM(jakt);
            ToolbarItems.Add(new ToolbarItem("+", "add.png", () =>
            {
                var logg = new Logg { JaktId = ViewModel.CurrentJakt.ID };
                Navigation.PushAsync(new LoggPage(logg), true);
            }, ToolbarItemOrder.Primary));
        }
        
        protected override void OnAppearing()
        {
            ViewModel.BindData();
            Title = ViewModel.CurrentJakt.Title;


            base.OnAppearing();
        }

        


        private void ListViewItems_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new LoggPage((Logg) e.SelectedItem), true);
                ((ListView) sender).SelectedItem = null;
            }
        }
 
    }
}
