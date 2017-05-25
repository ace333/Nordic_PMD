using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikePhil.Charting.Data;
using MikePhil.Charting.Components;
using NordicMobile.Abstract;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace NordicMobile.Charts
{
    class HeartPlot : IChartModifier
    {
        public int Time { get; set; }
        public int Counter { get; set; }

        public float AxisMin { get; private set; }
        public float AxisMax { get; private set; }
        public float Maximum { get; private set; }

        public LineData Data { get; set; }

        private bool isFirstValidValuesAdded = true;

        public HeartPlot()
        {
            AxisMin = 0;
            AxisMax = 12;
            Maximum = 15;
            Data = new LineData();
        }

        public void AddFalseEntry()
        {
            if (Data != null)
            {
                LineDataSet set = (LineDataSet)Data.GetDataSetByIndex(0);

                if (set == null)
                {
                    set = CreateSets().ToList()[0];

                    Data.AddDataSet(set);
                }

                Data.AddEntry(new Entry(Time++, -5), 0);
                Data.AddEntry(new Entry(Time++, -5), 0);
                Data.AddEntry(new Entry(Time++, -5), 0);
                Data.AddEntry(new Entry(Time++, -5), 0);
                Data.AddEntry(new Entry(Time++, -5), 0);
            }

            isFirstValidValuesAdded = true;
            TimeRegulation();
        }

        private void SetAxixBordersWithValidValues(float[] data)
        {
            float min = ParseValueByScale(data.Min());
            float max = ParseValueByScale(data.Max());

            //float min = ParseValueByScale(data);
            //int indexOf = min.ToString().IndexOf(',');
            //string value = min.ToString().Substring(0, indexOf + 3);
            //float validMin = float.Parse(value, System.Globalization.NumberStyles.Float);

            float finalMin = min - 0.002F;
            float finalMax = max + 0.003F;

            AxisMin = finalMin;
            AxisMax = max;
        }

        private float ParseValueByScale(float data)
        {
            return data / 100000;
        }

        public void AddEntry(float[] heartData)
        {
            if (Data != null)
            {
                LineDataSet set = (LineDataSet)Data.GetDataSetByIndex(0);

                if (set == null)
                {
                    set = CreateSets().ToList()[0];

                    Data.AddDataSet(set);
                }


                SetAxixBordersWithValidValues(heartData);                

                Data.AddEntry(new Entry(Time++, ParseValueByScale(heartData[0])), 0);
                Data.AddEntry(new Entry(Time++, ParseValueByScale(heartData[1])), 0);
                Data.AddEntry(new Entry(Time++, ParseValueByScale(heartData[2])), 0);
                Data.AddEntry(new Entry(Time++, ParseValueByScale(heartData[3])), 0);
                Data.AddEntry(new Entry(Time++, ParseValueByScale(heartData[4])), 0);
            }

            TimeRegulation();
        }

        private void TimeRegulation()
        {
            if (Time % Maximum == 0)
            {
                Counter++;
            }
        }


        public IEnumerable<LineDataSet> CreateSets()
        {
            LineDataSet set = new LineDataSet(null, "HeartRate");
            List<LineDataSet> sets = new List<LineDataSet>();

            sets.Add(set);
            ModifyALineDataSet(set, Color.Red);

            return sets;
        }

        public void ModifyALineDataSet(LineDataSet set, Color color)
        {
            set.CubicIntensity = 0.2F;
            set.AxisDependency = YAxis.AxisDependency.Left;
            set.LineWidth = 2F;
            set.FillAlpha = 65;
            set.Color = color;
            set.FillColor = color;
            set.SetDrawValues(false);
            set.ValueTextSize = 10F;
        }
    }
}