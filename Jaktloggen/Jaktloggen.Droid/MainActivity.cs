using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;


namespace Jaktloggen.Droid
{
    [Activity(Label = "Jaktloggen", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid

    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // New Xlabs
            var container = new SimpleContainer();
            container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
            container.Register<IGeolocator, Geolocator>();
            Resolver.SetResolver(container.GetResolver()); // Resolving the services
            // End new Xlabs
            

            FormsMaps.Init(this, bundle);
            ImageCircleRenderer.Init();

            LoadApplication(new App());
            
        }

        //private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        //{
        //    var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
        //    LogUnhandledException(newExc);
        //}

        //private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        //{
        //    var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
        //    LogUnhandledException(newExc);
        //}

        //internal static void LogUnhandledException(Exception exception)
        //{
        //    try
        //    {
        //        const string errorFileName = "Fatal.log";
        //        var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // iOS: Environment.SpecialFolder.Resources
        //        var errorFilePath = Path.Combine(libraryPath, errorFileName);
        //        var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
        //        DateTime.Now, exception.ToString());
        //        File.WriteAllText(errorFilePath, errorMessage);

        //        // Log to Android Device Logging.
        //        Android.Util.Log.Error("Crash Report", errorMessage);
        //    }
        //    catch
        //    {
        //        // just suppress any error logging exceptions
        //    }
        //}

    }
}


