using Android.App;
using Android.Content.PM;
using Android.OS;

using Plugin.Geolocator.Abstractions;

using Xamarin;
using Xamarin.Forms.Platform.Android;

namespace Jaktloggen.Droid
{
    [Activity(Label = "Jaktloggen", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity

    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            LoadApplication(new App());
            
        }
    }
}


