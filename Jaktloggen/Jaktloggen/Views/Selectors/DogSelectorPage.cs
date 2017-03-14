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
    public class DogSelectorPage : Base.ContentPageJL
    {
        private DogSelectorVM VM;
        public DogSelectorPage(Logg currentLogg)
        {
            BindingContext = VM = new DogSelectorVM(currentLogg);
            ToolbarItems.Add(new ToolbarItem("+", "add.png", () =>
            {
                Navigation.PushAsync(new DogPage(new Dog()), true);
            }, ToolbarItemOrder.Primary));
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
                    var selectedDog = ((Dog)e.SelectedItem);

                    if (VM.CurrentLogg.DogId == selectedDog.ID)
                    {
                        VM.RemoveDog();
                    }
                    else
                    {
                        VM.SetDog(selectedDog);
                    }
                    
                    Navigation.PopAsync(true);
                    ((ListView)sender).SelectedItem = null;
                }
            };
            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.TextProperty, "Navn");
            dt.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
            lv.ItemTemplate = dt;
            Content = lv;
        }
    }
}
