using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ImageCircle.Forms.Plugin.Abstractions;

using Jaktloggen.Helpers;
using Jaktloggen.Views.Extended;

using Xamarin.Forms;

using XLabs.Forms.Behaviors;

namespace Jaktloggen.Views.Input
{
    public class MediaPage : Base.ContentPageJL
    {
        public Action<MediaPage> Callback;

        public string ImageSource { get; set; }
        public MediaPage(string imageSource, Action<MediaPage> callback)
        {
            ImageSource = imageSource;
            Callback = callback;
            ToolbarItems.Add(new ToolbarItem("Ferdig", null, SaveEntryAndExit));
            Init();
        }

        private void Init()
        {
            var imgView = new CircleImage()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BorderThickness = 2,
                    BorderColor = Color.White,
                    Aspect = Aspect.AspectFill
                };
            imgView.Source = Xamarin.Forms.ImageSource.FromFile(ImageSource);

            var toolbar = new Grid();

            var btnLibrary = new Button() { Text = "Bibliotek" };
            btnLibrary.Clicked += BtnLibraryOnClicked;

            toolbar.Children.Add(btnLibrary, 0, 0);
            toolbar.Children.Add(new Button() { Text = "Bilde" }, 1, 0);
            toolbar.Children.Add(new Button() { Text = "Video" }, 2, 0);

            var layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 5,
                Children = {
                    imgView,
                    toolbar
                }
            };

            Content = layout;
        }

        private async void BtnLibraryOnClicked(object sender, EventArgs eventArgs)
        {
            var mediaFile = await XLabsHelper.SelectPicture();
            ImageSource = mediaFile.Path;
            Init();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        
        private void SaveEntryAndExit()
        {
            Callback?.Invoke(this);
            Navigation.PopAsync();
        }
    }
}
