using System.IO;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Provider;

using Jaktloggen.Droid.IO;
using Jaktloggen.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof(CameraDroid))]

namespace Jaktloggen.Droid.IO
{
    public class CameraDroid : ICamera
    {
        public void BringUpCamera()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            ((Activity)Forms.Context).StartActivityForResult(intent, 1);
        }

        public void BringUpPhotoGallery()
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);

            ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 1);
        }
    }
}       