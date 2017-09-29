using System;
using System.IO;

using Jaktloggen.Helpers;
using Jaktloggen.IO;
using Jaktloggen.Views.Extended;

using Xamarin.Forms;

namespace Jaktloggen.Views.Input
{
    public class MediaPage : Base.ContentPageJL
    {
        public Action<MediaPage> Callback;

        public string FileName { get; set; }
        public string ImageSource { get; set; }
        public MediaPage(string filename, string imageSource, Action<MediaPage> callback)
        {
            FileName = filename;
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

            if (!string.IsNullOrWhiteSpace(ImageSource))
            {
                imgView.Source = Xamarin.Forms.ImageSource.FromFile(ImageSource);
            }

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
            var stream = await XLabsHelper.SelectPicture();

            var byteStream = ReadFully(stream);

            var filePath = LocalFileStorage.SaveImage(FileName, byteStream);
            ImageSource = filePath;
            Init();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
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
