using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Jaktloggen.Helpers;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Jaktloggen.Views.Cells;
using Jaktloggen.Views.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Jaktloggen.Views
{
    public class LoggListPage : Base.ContentPageJL
    {
        private JaktVM VM;
        private ActivityIndicator PositionActivityIndicator;

        public LoggListPage(Jakt jakt)
        {
            Title = "Loggføringer";
            PositionActivityIndicator = new ActivityIndicator();
            BindingContext = VM = new JaktVM(jakt);
            ToolbarItems.Add(new ToolbarItem("Ny logg", null, () =>
            {
                var logg = VM.CreateLogg();
                Navigation.PushAsync(new LoggPage(logg), true);
            }, ToolbarItemOrder.Primary));

            if (VM.IsNew)
            {
                TryGetPosition();
            }
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        private void ToggleLoadPosition()
        {
            VM.IsLoadingPosition = !VM.IsLoadingPosition;
            PositionActivityIndicator.IsRunning = VM.IsLoadingPosition;
        }


        private async Task TryGetPosition()
        {

            var acceptUseGps = await DisplayAlert("Hent posisjon", "Ønsker du å hente posisjon og sted fra GPS?", "Ja", "Nei");
            if (acceptUseGps)
            {
                ToggleLoadPosition();

                var position = await XLabsHelper.GetPosition();
                if (position != null)
                {
                    VM.CurrentJakt.Latitude = position.Latitude.ToString();
                    VM.CurrentJakt.Longitude = position.Longitude.ToString();

                    var sted = await XLabsHelper.GetLocationNameForPosition(position.Latitude, position.Longitude);
                    
                    if (!string.IsNullOrWhiteSpace(sted))
                    {
                        VM.CurrentJakt.Sted = sted;
                    }

                    VM.Save();
                }

                ToggleLoadPosition();
            }
        }
        
        public void Init()
        {
            VM.BindData();

            var editJaktBtn = new Image() {Source = "more.png", HeightRequest = 40, WidthRequest = 40, HorizontalOptions = LayoutOptions.EndAndExpand};
            var dateLabel = new Label() {FontSize = 12};
            var titleLabel = new Label() { FontSize = 16 };
            
            var circleImage = new CircleImage()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                BorderThickness = 2,
                BorderColor = Color.White,
                HeightRequest = 70,
                WidthRequest = 70,
                Aspect = Aspect.AspectFill
            };

            var headerTextLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { dateLabel, titleLabel }
            };

            dateLabel.SetBinding(Label.TextProperty, new Binding("CurrentJakt.DatoFraTil"));
            titleLabel.SetBinding(Label.TextProperty, new Binding("CurrentJakt.Title"));
            circleImage.SetBinding(CircleImage.SourceProperty, new Binding("CurrentJakt.Image"));

            headerTextLayout.SetBinding(StackLayout.IsVisibleProperty, new Binding("IsLoadingPosition", converter: new InverseBooleanConverter()));
            PositionActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoadingPosition"));
            

            var jaktSummary = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = 5,
                GestureRecognizers = {
                        new TapGestureRecognizer {
                                Command = new Command (()=>Navigation.PushAsync(new JaktPage(VM.CurrentJakt))),
                        },
                },
                Children =
                {
                    circleImage,
                    headerTextLayout,
                    PositionActivityIndicator,
                    editJaktBtn
                }
            };
            ListView lv = new ListView();
            lv.HorizontalOptions = LayoutOptions.FillAndExpand;
            lv.VerticalOptions = LayoutOptions.FillAndExpand;
            lv.SetBinding(ListView.ItemsSourceProperty, new Binding("ItemCollection"));
            lv.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    Navigation.PushAsync(new LoggPage((Logg)e.SelectedItem), true);
                    ((ListView)sender).SelectedItem = null;
                }
            }; //Remember to remove this event handler on dispoing of the page;
            DataTemplate dt = new DataTemplate(typeof(CircleImageCell));
            dt.SetBinding(CircleImageCell.TextProperty, new Binding("Title"));
            dt.SetBinding(CircleImageCell.DetailProperty, new Binding("Details"));
            dt.SetBinding(CircleImageCell.ImageSourceProperty, new Binding("Image"));
            lv.ItemTemplate = dt;

            if (VM.ItemCollection.Any())
            {
                Content = new StackLayout()
                {
                    Padding = 5,
                    Children =
                    {
                        jaktSummary,
                        lv
                    }
                };
            }
            else
            {
                var btn = new Button()
                {
                    Text = "Opprett første loggføring",
                    BackgroundColor = Color.FromHex("#74B058")
                };
                btn.Clicked += delegate (object sender, EventArgs args)
                {
                    var logg = VM.CreateLogg();
                    Navigation.PushAsync(new LoggPage(logg), true);
                };

                Content = new StackLayout()
                {
                    Children =
                    {
                        jaktSummary,
                        btn
                    }
                };
            }
        }
    }
}
