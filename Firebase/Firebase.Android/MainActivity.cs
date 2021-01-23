
using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Iid;
using Xamarin.Essentials;

namespace Firebase.Droid
{
    [Activity(Label = "Firebase", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            
             ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Preferences.Set("token", "");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            FirebaseOptions options = new FirebaseOptions.Builder()
             .SetApplicationId("1:609768415952:android:b2298730ff3f87176734d5")
             .SetApiKey("AIzaSyBtk5lUyMit_CCeb6I7oyfOZZFCAdtYS2Y")
             .SetGcmSenderId("609768415952")
             .Build();

            bool hasBeenInitialized = false;
            IList<FirebaseApp> firebaseApps = FirebaseApp.GetApps(Application.Context);
            foreach (FirebaseApp app in firebaseApps)
            {
                if (app.Name.Equals(FirebaseApp.DefaultAppName))
                {
                    hasBeenInitialized = true;
                    FirebaseApp firebaseApp = app;
                }
            }

            if (!hasBeenInitialized)
            {
                FirebaseApp firebaseApp = FirebaseApp.InitializeApp(Application.Context, options);

                com.somee.webserviceda.www.Service1 proxi = new com.somee.webserviceda.www.Service1();
                proxi.saveTokenAsync(FirebaseInstanceId.Instance.Token);
            }


#if DEBUG
            // Force refresh of the token. If we redeploy de app, no new token will be sent but the old will
            // be invalid.
            Task.Run(() => {
                /*    if(Preferences.Get("token", "") == null)
                    {
                        Preferences.Set("token", FirebaseInstanceId.Instance.Token);


                    }*/
                // This may not be executed on the main thread.

                com.somee.webserviceda.www.Service1 proxi = new com.somee.webserviceda.www.Service1();
                proxi.saveTokenAsync(FirebaseInstanceId.Instance.Token);
                Console.WriteLine("Forced Token: " + FirebaseInstanceId.Instance.Token);
                           });
#endif
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}   