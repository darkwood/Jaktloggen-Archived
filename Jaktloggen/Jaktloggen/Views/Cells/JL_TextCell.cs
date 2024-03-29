﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Jaktloggen.Views.Cells
{
    public class JL_TextCell : ViewCell
    {
        public JL_TextCell(string label, string text, EventHandler onTapped = null)
        {
            View = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 10,
                Children =
                {
                    new Label()
                    {
                        Text = label,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        LineBreakMode = LineBreakMode.NoWrap,
                    },
                    new Label()
                    {
                        Text = text,
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
            if (onTapped != null)
            {
                Tapped += onTapped;
            }
        }
    }
}
