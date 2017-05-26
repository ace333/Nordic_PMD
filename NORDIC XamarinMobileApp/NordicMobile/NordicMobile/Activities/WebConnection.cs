﻿/*
 * Copyright (c) 2015, Arkadiusz Chudy, Bartłomiej Cerek.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

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
        private int conn_lost;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            conn_lost = Intent.GetIntExtra("conn_lost", 0);
            worker.DoWork += Worker_DoWork;


            if(IsInternetConnectionReachable())
            { 
                if(conn_lost == 1)
                {
                    StartActivity(typeof(MainActivity));
                }
                else
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

                    if (conn_lost == 1)
                    {
                        StartActivity(typeof(MainActivity));
                    }
                    else
                        StartActivity(typeof(InfoActivity));
                }
                await Task.Delay(1000);
            }
        }
    }
}