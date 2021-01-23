using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace Firebase.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            var body = message.GetNotification().Body;
            var title = message.GetNotification().Title;
            Log.Debug(TAG, "Notification Message Body: " + body);
            SendNotification(body,title);
        }

        void SendNotification(string messageBody,string title)
        {

            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(ApplicationContext, typeof(MainActivity));
            NotificationCompat.Builder builder = new NotificationCompat.Builder(ApplicationContext);

            var notification = builder.SetContentIntent(PendingIntent.GetActivity(ApplicationContext, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymDefAppIcon)
                .SetTicker("TaskList")
                .SetContentTitle(title)
                .SetContentText(messageBody)
                
                .SetAutoCancel(true)
                .Build();

            notificationManager.Notify(1, notification);
        }
    }

}