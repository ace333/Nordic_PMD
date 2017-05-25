using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using NordicDatabaseDLL;
using NordicMobile.Model;

namespace NordicMobile.Activities
{
    [Activity(Label = "Patient Monitoring Device")]
    public class InfoActivity : Activity
    {
        private int x_id;
        private int y_id;
        private int z_id;
        private int heart_id;

        private ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
        private ListView listView;
        private List<string> names = new List<string>();
        private ConnectionClass conn = new ConnectionClass();

        private bool isSelected;
        private bool doubleBackToExitPressedOnce = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            SetContentView(Resource.Layout.Info);

            GetLastIds();

            SetPatientsList();

            Button button = FindViewById<Button>(Resource.Id.startButton);
            button.Click += Button_Click;

            listView = FindViewById<ListView>(Resource.Id.listView);
            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, names);
            listView.ChoiceMode = ChoiceMode.Single;

            listView.ItemClick += ListView_ItemClick;

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

        private void GetLastIds()
        {
            conn.OpenConnection();
            x_id = conn.GetLastXAxisID();
            y_id = conn.GetLastYAxisID();
            z_id = conn.GetLastZAxisID();
            heart_id = conn.GetLastHeartRateID();
            conn.CloseConnection();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if(isSelected)
            {
                var activity = new Intent(this, typeof(MainActivity));
                activity.PutExtra("x_id", x_id);
                activity.PutExtra("y_id", y_id);
                activity.PutExtra("z_id", z_id);
                activity.PutExtra("heart_id", heart_id);

                StartActivity(activity);
            }
            else
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetMessage("Please choose a PATIENT");
                dialog.SetTitle("ERROR!");
                dialog.Show();
            }
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int id = Convert.ToInt32(e.Id);
            SetTextViews(id);
            listView.SetSelection(id);
            isSelected = true;
        }

        private void SetTextViews(int index)
        {
            TextView name = FindViewById<TextView>(Resource.Id.nameText);
            name.Text = patients[index].Name + " " + patients[index].Surname;

            TextView age = FindViewById<TextView>(Resource.Id.ageText);
            age.Text = patients[index].Age.ToString();

            TextView city = FindViewById<TextView>(Resource.Id.cityText);
            city.Text = patients[index].City;

            TextView info = FindViewById<TextView>(Resource.Id.infoText);
            info.Text = patients[index].Info;
        }

        private void SetPatientsList()
        {
            conn.OpenConnection();
            DataTable dt = conn.SelectAllPatients();
            conn.CloseConnection();

            foreach (DataRow row in dt.Rows)
            {
                names.Add(row[1].ToString() + " " + row[2].ToString());

                Patient patient = new Patient
                {
                    ID = Convert.ToInt32(row[0]),
                    Name = row[1].ToString(),
                    Surname = row[2].ToString(),
                    Age = Convert.ToInt32(row[3]),
                    City = row[4].ToString(),
                    Info = row[5].ToString()
                };

                patients.Add(patient);
            }

        }
    }
}