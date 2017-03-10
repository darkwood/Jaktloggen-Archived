using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace Jaktloggen.Views.Cells
{
    public class JL_ButtonCell : ViewCell
    {
        public JL_ButtonCell(string text, EventHandler onTapped = null)
        {
            var btn = new Button()
            {
                Text = text
            };
            
            btn.Clicked += onTapped;

            View = btn;

        }
    }
}
