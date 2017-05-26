/*
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