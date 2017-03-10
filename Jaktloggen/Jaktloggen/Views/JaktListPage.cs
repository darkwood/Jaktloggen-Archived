using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Jaktloggen.Views.Cells;
using Jaktloggen.Views.Input;
using Xamarin.Forms;

namespace Jaktloggen.Views
{
    public class JaktListPage : ContentPage
    {
        private JaktListVM VM;
        public JaktListPage()
        {
            BindingContext = VM = new JaktListVM();
            ToolbarItems.Add(new ToolbarItem("+", "add.png", CreateNewItem, ToolbarItemOrder.Primary));
        }

        private async void CreateNewItem()
        {
            var jakt = VM.CreateJakt();
            await Navigation.PushAsync(new LoggListPage(jakt));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        public void Init()
        {
            VM.BindData();

            ListView lv = new ListView();
            lv.RowHeight = 50;
            lv.HorizontalOptions = LayoutOptions.FillAndExpand;
            lv.VerticalOptions = LayoutOptions.FillAndExpand;
            lv.SetBinding(ListView.ItemsSourceProperty, new Binding("GroupedItems"));
            lv.IsGroupingEnabled = true;
            lv.GroupDisplayBinding = new Binding("Name");
            lv.GroupShortNameBinding = new Binding("ShortName");
            lv.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    Navigation.PushAsync(new LoggListPage((Jakt)e.SelectedItem));
                    ((ListView)sender).SelectedItem = null;
                }
            }; //Remember to remove this event handler on dispoing of the page;
            DataTemplate dt = new DataTemplate(typeof(CircleImageCell));
            dt.SetBinding(CircleImageCell.TextProperty, "Title");
            dt.SetBinding(CircleImageCell.DetailProperty, "Details");
            dt.SetBinding(CircleImageCell.ImageSourceProperty, "Image");
            lv.ItemTemplate = dt;
            if (VM.GroupedItems.Any())
            {
                Content = lv;
            }
            else
            {
                var btn = new Button()
                {
                    Text = "+ Opprett første jakt",
                    BackgroundColor = Color.FromHex("#74B058")
                };
                btn.Clicked += delegate (object sender, EventArgs args) { CreateNewItem(); };
                
                var myImage = new CircleImage()
                {
                    Source = FileImageSource.FromFile("placeholder_hunt.jpg"),
                    BorderThickness = 2,
                    BorderColor = Color.White,
                    Aspect = Aspect.AspectFill,
                    Margin = 20
                };
                
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        myImage,
                        btn
                    }
                };
            }
        }
    }
}
