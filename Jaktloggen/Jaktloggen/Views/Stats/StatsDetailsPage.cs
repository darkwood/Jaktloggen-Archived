using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Jaktloggen.ViewModels.Stats;
using Jaktloggen.Views.Cells;
using Jaktloggen.Views.Input;
using Xamarin.Forms;

namespace Jaktloggen.Views
{
    public class StatsDetailsPage : Base.ContentPageJL
    {
        public StatsDetailsVM VM;
        public StatsDetailsPage(StatItem item)
        {
            BindingContext = VM = new StatsDetailsVM(item);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }
        
        public void Init()
        {
            ListView lv = new ListView();
            lv.HorizontalOptions = LayoutOptions.FillAndExpand;
            lv.VerticalOptions = LayoutOptions.FillAndExpand;
            lv.SetBinding(ListView.ItemsSourceProperty, new Binding("ItemCollection"));
            lv.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    var statItem = ((StatItem) e.SelectedItem);
                    DisplayAlert(statItem.Title, statItem.Details, "OK");
                    ((ListView)sender).SelectedItem = null;
                }
            }; //TODO Remember to remove this event handler on dispoing of the page;
            DataTemplate dt = new DataTemplate(typeof(CircleImageCell));
            dt.SetBinding(CircleImageCell.TextProperty, "Title");
            dt.SetBinding(CircleImageCell.DetailProperty, "Details");
            lv.ItemTemplate = dt;
            Content = lv;
        }
    }
}
