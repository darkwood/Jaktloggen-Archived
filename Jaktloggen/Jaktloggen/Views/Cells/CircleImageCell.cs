using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace Jaktloggen.Views.Cells
{
    public class CircleImageCell : ViewCell
    {
        private Label TitleLabel = new Label();
        private Label DetailsLabel = new Label();
        private CircleImage CircleImage;
        private Image SecondaryImage { get; set; }
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text", typeof(string), typeof(CircleImageCell), "");
        public static readonly BindableProperty DetailProperty =
       BindableProperty.Create("Detail", typeof(string), typeof(CircleImageCell), "");
        public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(CircleImageCell));
        public static readonly BindableProperty SecondaryImageSourceProperty =
        BindableProperty.Create("SecondaryImageSource", typeof(ImageSource), typeof(CircleImageCell));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public ImageSource SecondaryImageSource
        {
            get { return (ImageSource)GetValue(SecondaryImageSourceProperty); }
            set { SetValue(SecondaryImageSourceProperty, value); }
        }
        public CircleImageCell()
        {
            CircleImage = new CircleImage()
            {

                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BorderThickness = 1,
                BorderColor = Color.White,
                HeightRequest = 40,
                WidthRequest = 40,
                Aspect = Aspect.AspectFill
            };
            SecondaryImage = new Image()
            {

                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 40,
                WidthRequest = 40,
                Aspect = Aspect.AspectFill
            };


            TitleLabel.VerticalOptions = LayoutOptions.CenterAndExpand;
            TitleLabel.HorizontalOptions = LayoutOptions.StartAndExpand;

            DetailsLabel.FontSize = 10;
            DetailsLabel.HorizontalOptions = LayoutOptions.EndAndExpand;
            DetailsLabel.VerticalOptions = LayoutOptions.CenterAndExpand;
            

            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 5,
                Children =
                {
                    CircleImage,
                    TitleLabel,
                    DetailsLabel,
                    SecondaryImage
                }
            };

            View = stackLayout;
        }

        

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                TitleLabel.Text = Text;
                DetailsLabel.Text = Detail;
                CircleImage.Source = ImageSource;
                SecondaryImage.Source = SecondaryImageSource;

                DetailsLabel.FontSize = Detail.Length < 10 ? 14 : 10;

            }
        }
    }
}
