using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Android.App;
using Firebase.Iid;
using Android.Util;

namespace AndroidAppClient.Platforms.Android
{
	[Service(Exported = false)]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class MyFirebaseIIDService : FirebaseInstanceIdService
	{
		const string TAG = "MyFirebaseIIDService";
		public override void OnTokenRefresh()
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Log.Debug(TAG, "Refreshed token: " + refreshedToken);
			SendRegistrationToServer(refreshedToken);
		}
		void SendRegistrationToServer(string token)
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Log.Debug(TAG, "Refreshed token: " + refreshedToken);
		}
	}
}
