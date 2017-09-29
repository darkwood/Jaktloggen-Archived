using System;
using System.IO;

using Jaktloggen.Helpers;
using Jaktloggen.Interfaces;
using Jaktloggen.IO;
using Jaktloggen.Views.Extended;

using Xamarin.Forms;

namespace Jaktloggen.Views.Input
{
    public class MediaPage : Base.ContentPageJL
    {
        public Action<MediaPage> Callback;

        public string FileName { get; set; }
        public string ImagePath { get; set; }
        public CircleImage ImageView { get; set; }

        public MediaPage(string filename, string imagePath, Action<MediaPage> callback)
        {
            FileName = filename;
            ImagePath = imagePath;
            Callback = callback;
            Init();

            ToolbarItems.Add(new ToolbarItem("Ferdig", null, SaveEntryAndExit));

            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (bytes) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Set the source of the image view with the byte array
                    ImageView.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                    var filePath = LocalFileStorage.SaveImage(FileName, bytes);
                    ImagePath = filePath;
                    
                });
            });
        }

        private void Init()
        {
            ImageView = new CircleImage()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            BorderThickness = 2,
                            BorderColor = Color.White,
                            Aspect = Aspect.AspectFill
                        };

            if (!string.IsNullOrWhiteSpace(ImagePath))
            {
                ImageView.Source = ImageSource.FromFile(ImagePath);
            }

            var toolbar = new Grid();

            var btnLibrary = new Button() { Text = "Bibliotek" };
            btnLibrary.Clicked += delegate { DependencyService.Get<ICamera>().BringUpPhotoGallery(); };
            toolbar.Children.Add(btnLibrary, 0, 0);

            var btnTakePhoto = new Button() { Text = "Ta bilde" };
            btnTakePhoto.Clicked += delegate { DependencyService.Get<ICamera>().BringUpCamera(); };
            toolbar.Children.Add(btnTakePhoto, 1, 0);
            
            //toolbar.Children.Add(new Button() { Text = "Video" }, 2, 0);

            var layout = new StackLayout
                         {
                             HorizontalOptions = LayoutOptions.FillAndExpand,
                             VerticalOptions = LayoutOptions.FillAndExpand,
                             Padding = 0,
                             Children = {
                                            ImageView,
                                            toolbar
                                        }
                         };

            Content = layout;
        }
        
        private void SaveEntryAndExit()
        {
            Callback?.Invoke(this);
            Navigation.PopAsync();
        }
    }
}
