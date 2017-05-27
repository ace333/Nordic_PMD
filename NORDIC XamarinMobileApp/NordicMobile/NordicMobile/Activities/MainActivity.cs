/*
 * Copyright (c) 2015, Arkadiusz Chudy, Bartłomiej Cerek.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using Android.App;
using Android.Widget;
using Android.OS;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Data;
using MikePhil.Charting.Components;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

using Android.Net;

using NordicDatabaseDLL;
using NordicMobile.Charts;
using NordicMobile.Enums;
using Android.Content;

namespace NordicMobile.Activities
{
    [Activity(Label = "Patient Monitoring Device")]
    public class MainActivity : Activity
    {
        private int x_id;
        private int y_id;
        private int z_id;
        private int heart_id;

        private LineChart chart;
        private LineData data;
        private YAxis yaxis;

        private Button button;

        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker internetWorker = new BackgroundWorker();

        private ConnectionClass conn = new ConnectionClass();
        private HeartPlot heartPlot = new HeartPlot();
        private AcceleroPlot acceleroPlot = new AcceleroPlot();

        private ChartEnum whichPlot;

        private bool doubleBackToExitPressedOnce = false;
        private bool firstData = true;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            conn.OpenConnection();

            worker.DoWork += Worker_DoWork;
            worker.WorkerSupportsCancellation = true;

            internetWorker.DoWork += InternetWorker_DoWork;
            internetWorker.WorkerSupportsCancellation = true;

            worker.RunWorkerAsync();
            internetWorker.RunWorkerAsync();

            //getting parameters from thread earlier
            GetParameters();

            chart = FindViewById<LineChart>(Resource.Id.lineChart1);

            button = FindViewById<Button>(Resource.Id.chartButton);
            button.Click += Button_Click;
            button.Enabled = false;

            SetChartParameters();
            SetAxisesParameters();

            OnFirstStart();

        }

        private async void InternetWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!IsInternetConnectionReachable())
                {
                    worker.CancelAsync();
                    conn.CloseConnection();

                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetMessage("no internet connectivity");
                    dialog.SetTitle("ERROR!");

                    Action showDialog  = () => dialog.Show();
                    RunOnUiThread(showDialog);                    
                                        

                    await Task.Delay(2000);


                    var activity = new Intent(this, typeof(WebConnection));
                    activity.PutExtra("conn_lost", 1);
                    StartActivity(activity);

                    break;
                }
            }
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

        #region Checkers

        private bool CheckIfXAxisIDChanged()
        {
            int newId = conn.GetLastXAxisID();
            if (newId > x_id)
            {
                x_id = newId;
                return true;
            }
            else
                return false;
        }

        private bool CheckIfYAxisIDChanged()
        {
            int newId = conn.GetLastYAxisID();
            if (newId > y_id)
            {
                y_id = newId;
                return true;
            }
            else
                return false;
        }

        private bool CheckIfZAxisIDChanged()
        {
            int newId = conn.GetLastZAxisID();
            if (newId > z_id)
            {
                z_id = newId;
                return true;
            }
            else
                return false;
        }

        private bool CheckIfHeartIDChanged()
        {
            int newId = conn.GetLastHeartRateID();
            if (newId > heart_id)
            {
                heart_id = newId;
                return true;
            }
            else
                return false;
        }

        #endregion        

        #region Workers

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(worker.CancellationPending)
                {
                    break;
                }

                if(CheckAccelero())
                {
                    CheckHeart();
                }
                    
            }
        }

        private void CheckHeart()
        {
            if (CheckIfHeartIDChanged())
            {
                float[] data = conn.SelectDataHeartRate();
                AddHeartRateEntries(data);
            }
            else
                AddFalseHeartRateEntries();

        }

        private bool CheckAccelero()
        {
            if (CheckIfXAxisIDChanged())
            {
                if (CheckIfYAxisIDChanged())
                {
                    if (CheckIfZAxisIDChanged())
                    {
                        PerformAcceleroAddingEntries();

                        if (acceleroPlot.CheckIfFall())
                        {
                            chart.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
                        }
                        else
                            chart.SetBackgroundColor(Android.Graphics.Color.White);
                            
                                                

                        if(firstData)
                        {
                            Action buttonEnable = () => button.Enabled = true;
                            RunOnUiThread(buttonEnable);
                            firstData = false;
                        }

                        return true;
                    }

                    return false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }


        #endregion

        private void PerformAcceleroAddingEntries()
        {
            float[] x = conn.SelectDataXAccelero();
            float[] y = conn.SelectDataYAccelero();
            float[] z = conn.SelectDataZAccelero();
            AddAcceleroEntries(x, y, z);
        }

        private void GetParameters()
        {
            x_id = Intent.GetIntExtra("x_id", 0);
            y_id = Intent.GetIntExtra("y_id", 0);
            z_id = Intent.GetIntExtra("z_id", 0);
            heart_id = Intent.GetIntExtra("heart_id", 0);
        }

        public override void OnBackPressed()
        {
            if(doubleBackToExitPressedOnce)
            {
                conn.CloseConnection();
                var activity = (Activity)this;
                activity.FinishAffinity();
                return;
            }

            doubleBackToExitPressedOnce = true;
            Toast.MakeText(this, "Click again to exit application", ToastLength.Short).Show();

            Action action = () => doubleBackToExitPressedOnce = false;
            new Handler().PostDelayed(action, 2000);
                
        }

        private void SetChartParameters()
        {
            chart.SetNoDataText("NO DATA FOR NOW. PLEASE WAIT");

            chart.SetTouchEnabled(false);
            chart.SetScaleEnabled(false);
            chart.DragEnabled = false;

            chart.SetDrawGridBackground(false);
            chart.Description.Enabled = false;

            chart.SetPinchZoom(true);

            chart.SetBackgroundColor(Android.Graphics.Color.White);
            chart.SetBorderColor(21);

            //legend
            Legend l = chart.Legend;
            l.Form = Legend.LegendForm.Line;
            l.TextColor = Android.Graphics.Color.Black;
            l.TextSize = 20F;
        }

        private void SetAxisesParameters()
        {
            //axises
            XAxis xasis = chart.XAxis;
            xasis.Position = XAxis.XAxisPosition.Bottom;
            xasis.TextColor = Android.Graphics.Color.Blue;
            xasis.SetDrawGridLines(false);
            xasis.SetAvoidFirstLastClipping(true);

            yaxis = chart.AxisLeft;
            yaxis.TextColor = Android.Graphics.Color.Blue;
            yaxis.SetDrawGridLines(true);

            YAxis yrightaxist = chart.AxisRight;
            yrightaxist.Enabled = false;
        }

        private void ChangeCharts()
        {
            if (whichPlot == ChartEnum.AcceleroChart)
            {
                data = heartPlot.Data;
                chart.Data = data;
                whichPlot = ChartEnum.HeartChart;
                DefineChart();
            }
            else
            {
                data = acceleroPlot.Data;
                chart.Data = data;
                whichPlot = ChartEnum.AcceleroChart;
                DefineChart();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ChangeCharts();
        }

        private void OnFirstStart()
        {
            //data to Chart
            data = acceleroPlot.Data;
            chart.Data = data;
            whichPlot = ChartEnum.AcceleroChart;

            //yAxis
            yaxis.AxisMaximum = acceleroPlot.AxisMin;
            yaxis.AxisMaximum = acceleroPlot.AxisMax;

        }

        private void AddAcceleroEntries(float[] X, float[] Y, float[] Z)
        {
            acceleroPlot.AddEntry(X, Y, Z);

            chart.NotifyDataSetChanged();
            DefineChart();
        }

        private void AddFalseHeartRateEntries()
        {
            heartPlot.AddFalseEntry();

            chart.NotifyDataSetChanged();
            DefineChart();
        }

        private void AddHeartRateEntries(float[] data)
        {
            heartPlot.AddEntry(data);

            chart.NotifyDataSetChanged();
            DefineChart();
        }


        private void DefineChart()
        {
            if (whichPlot == ChartEnum.AcceleroChart)
            {
                chart.SetVisibleXRangeMaximum(acceleroPlot.Maximum);
                chart.MoveViewToX(acceleroPlot.Maximum * acceleroPlot.Counter);

                chart.AxisLeft.AxisMinimum = acceleroPlot.AxisMin;
                chart.AxisLeft.AxisMaximum = acceleroPlot.AxisMax;
            }
            else
            {
                chart.SetVisibleXRangeMaximum(heartPlot.Maximum);
                chart.MoveViewToX(heartPlot.Maximum * heartPlot.Counter);

                chart.AxisLeft.AxisMinimum = heartPlot.AxisMin;
                chart.AxisLeft.AxisMaximum = heartPlot.AxisMax;
            }
        }

    }
}

