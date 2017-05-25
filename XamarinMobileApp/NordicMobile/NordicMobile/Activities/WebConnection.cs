using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using System.Threading.Tasks;

namespace NordicMobile.Activities
{
    [Activity(Label = "Patient Monitoring Device", MainLauncher = true, Icon = "@drawable/icon")]
    public class WebConnection : Activity
    {
        private BackgroundWorker worker = new BackgroundWorker();
        private bool isWorkerActive = true;
        private bool doubleBackToExitPressedOnce = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            worker.DoWork += Worker_DoWork;

            if(IsInternetConnectionReachable())
            {
                StartActivity(typeof(InfoActivity));
            }
            else
            {
                SetContentView(Resource.Layout.Internet);
                worker.RunWorkerAsync();
            }            
        }

        public override void OnBackPressed()
        {
            if (doubleBackToExitPressedOnce)
            {
                var activity = (Activity)this;
                activity.FinishAffinity();
                return;
            }

            doubleBackToExitPressedOnce = true;
            Toast.MakeText(this, "Click again to exit application", ToastLength.Short).Show();

            Action action = () => doubleBackToExitPressedOnce = false;
            new Handler().PostDelayed(action, 2000);

        }

        private bool IsInternetConnectionReachable()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo info = connectivityManager.ActiveNetworkInfo;
            if (info == null)
                return false;
            else
                return info.IsConnected;
        }

        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(isWorkerActive)
            {
                if(IsInternetConnectionReachable())
                {
                    Action action = () => Toast.MakeText(this, "Connected to internet!", ToastLength.Short).Show();
                    RunOnUiThread(action);
                    isWorkerActive = false;

                    await Task.Delay(500);
                    
                    StartActivity(typeof(InfoActivity));
                }
                await Task.Delay(1000);
            }
        }
    }
}