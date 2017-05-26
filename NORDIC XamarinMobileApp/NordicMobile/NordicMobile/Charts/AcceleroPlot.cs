using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikePhil.Charting.Data;
using MikePhil.Charting.Components;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace NordicMobile.Charts
{
    class AcceleroPlot //: IChartModifier
    {
        public int Time { get; set; }

        public float AxisMin { get; private set; }
        public float AxisMax { get; private set; }
        public float Maximum { get; private set; }

        public int Counter { get; set; }
        public LineData Data { get; set; }

        public AcceleroPlot()
        {
            AxisMin = -2.5F;
            AxisMax = 2.5F;
            Maximum = 15;
            Data = new LineData();
        }

        public void AddEntry(float[] X, float[] Y, float[] Z)
        {
            if (Data != null)
            {
                LineDataSet setX = (LineDataSet)Data.GetDataSetByIndex(0);
                LineDataSet setY = (LineDataSet)Data.GetDataSetByIndex(1);
                LineDataSet setZ = (LineDataSet)Data.GetDataSetByIndex(2);

                if (setX == null)
                {
                    setX = CreateSets().ToList()[0];
                    setY = CreateSets().ToList()[1];
                    setZ = CreateSets().ToList()[2];

                    Data.AddDataSet(setX);
                    Data.AddDataSet(setY);
                    Data.AddDataSet(setZ);
                }

                Data.AddEntry(new Entry(Time, X[0]), 0);
                Data.AddEntry(new Entry(Time, Y[0]), 1);
                Data.AddEntry(new Entry(Time++, Z[0]), 2);

                Data.AddEntry(new Entry(Time, X[1]), 0);
                Data.AddEntry(new Entry(Time, Y[1]), 1);
                Data.AddEntry(new Entry(Time++, Z[1]), 2);

                Data.AddEntry(new Entry(Time, X[2]), 0);
                Data.AddEntry(new Entry(Time, Y[2]), 1);
                Data.AddEntry(new Entry(Time++, Z[2]), 2);

                Data.AddEntry(new Entry(Time, X[3]), 0);
                Data.AddEntry(new Entry(Time, Y[3]), 1);
                Data.AddEntry(new Entry(Time++, Z[3]), 2);

                Data.AddEntry(new Entry(Time, X[4]), 0);
                Data.AddEntry(new Entry(Time, Y[4]), 1);
                Data.AddEntry(new Entry(Time++, Z[4]), 2);
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
            LineDataSet setX = new LineDataSet(null, "X");
            LineDataSet setY = new LineDataSet(null, "Y");
            LineDataSet setZ = new LineDataSet(null, "Z");

            List<LineDataSet> sets = new List<LineDataSet>();

            sets.Add(setX);
            sets.Add(setY);
            sets.Add(setZ);

            ModifyALineDataSet(setX, Color.Red);
            ModifyALineDataSet(setY, Color.Green);
            ModifyALineDataSet(setZ, Color.Blue);          

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