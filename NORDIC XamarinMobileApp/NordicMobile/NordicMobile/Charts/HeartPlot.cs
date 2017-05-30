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
    class HeartPlot : AbstractPlot
    {

        #region Class Fields

        private float axisMinimum = 0;
        private float axisMaximum = 0;

        #endregion
        
        #region Constructor

        public HeartPlot()
        {
            AxisMin = 0;
            AxisMax = 12;
            Maximum = 50;
            Data = new LineData();
        }

        #endregion

        #region Setting values

        private void SetAxixBordersWithValidValues(float[] data)
        {
            float min = ParseValueByScale(data.Min());
            float max = ParseValueByScale(data.Max());

            if (axisMinimum == 0 || min < axisMinimum)
            {
                float finalMin = min - 0.001F;
                axisMinimum = finalMin;
                AxisMin = finalMin;
            }

            if (max > axisMaximum)
            {
                float finalMax = max + 0.001F;
                axisMaximum = finalMax;
                AxisMax = finalMax;
            }
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

        #endregion

        #region Regulating and editing

        private float ParseValueByScale(float data)
        {
            return data / 100000;
        }

        private void TimeRegulation()
        {
            if (Time % Maximum == 0)
            {
                Counter++;
            }
        }

        #endregion

        #region Override methods

        public override void AddEntry(float[] heartData)
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

        public override IEnumerable<LineDataSet> CreateSets()
        {
            LineDataSet set = new LineDataSet(null, "HeartRate");
            List<LineDataSet> sets = new List<LineDataSet>();

            sets.Add(set);
            ModifyALineDataSet(set, Color.Red);

            return sets;
        }

        public override void ModifyALineDataSet(LineDataSet set, Color color)
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

        #endregion

    }
}