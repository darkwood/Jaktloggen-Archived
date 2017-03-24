using Jaktloggen.Droid.Extended;
using Jaktloggen.Views.Extended;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(JEntry), typeof(JEntryRenderer))]
namespace Jaktloggen.Droid.Extended
{
    class JEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetSelectAllOnFocus(true);
            }
        }
    }
}