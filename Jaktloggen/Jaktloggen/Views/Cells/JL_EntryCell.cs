using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace Jaktloggen.Views.Cells
{
    public class JL_EntryCell : EntryCell
    {
        public JL_EntryCell(string label, string text, string binding, EventHandler onComplete = null)
        {
            Label = label;
            Text = text;
            this.SetBinding(TextProperty, binding);
            HorizontalTextAlignment = TextAlignment.End;
            if (onComplete != null)
            {
                Completed += onComplete;
            }
        }
    }
}
