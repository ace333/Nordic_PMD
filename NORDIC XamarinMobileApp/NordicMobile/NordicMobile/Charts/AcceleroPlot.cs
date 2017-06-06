/*
 * Copyright (c) 2015, Arkadiusz Chudy, Bartłomiej Cerek.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System.Collections.Generic;
using System.Linq;

using MikePhil.Charting.Data;
using MikePhil.Charting.Components;
using Android.Graphics;

using NordicDatabaseDLL;
using NordicMobile.Abstract;

namespace NordicMobile.Charts
{
    class AcceleroPlot : AbstractPlot
    {

        #region Class Fields

        private List<float> Xes = new List<float>();
        private List<float> Yes = new List<float>();
        private List<float> Zes = new List<float>();

        public FallChecker fall = new FallChecker();

        #endregion

        #region Constructor

        public AcceleroPlot()
        {
            AxisMin = -2.5F;
            AxisMax = 2.5F;
            Maximum = 50;
            Data = new LineData();
        }

        #endregion

        #region Fall Checker

        public bool CheckIfFall()
        {
            EditLists();
            return fall.CheckIfFall(Xes.ToArray(), Yes.ToArray(), Zes.ToArray(), 1.4F, 20);
        }

        #endregion

        #region Regulating and Editing Properties

        private void EditLists()
        {
            while (Xes.Count > Maximum)
            {
                Xes.RemoveAt(0);
                Yes.RemoveAt(0);
                Zes.RemoveAt(0);
            }
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

        public override void AddEntry(float[] X, float[] Y, float[] Z)
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

                Xes.Add(X[0]);
                Yes.Add(Y[0]);
                Zes.Add(Z[0]);

                Data.AddEntry(new Entry(Time, X[1]), 0);
                Data.AddEntry(new Entry(Time, Y[1]), 1);
                Data.AddEntry(new Entry(Time++, Z[1]), 2);

                Xes.Add(X[1]);
                Yes.Add(Y[1]);
                Zes.Add(Z[1]);

                Data.AddEntry(new Entry(Time, X[2]), 0);
                Data.AddEntry(new Entry(Time, Y[2]), 1);
                Data.AddEntry(new Entry(Time++, Z[2]), 2);

                Xes.Add(X[2]);
                Yes.Add(Y[2]);
                Zes.Add(Z[2]);

                Data.AddEntry(new Entry(Time, X[3]), 0);
                Data.AddEntry(new Entry(Time, Y[3]), 1);
                Data.AddEntry(new Entry(Time++, Z[3]), 2);

                Xes.Add(X[3]);
                Yes.Add(Y[3]);
                Zes.Add(Z[3]);

                Data.AddEntry(new Entry(Time, X[4]), 0);
                Data.AddEntry(new Entry(Time, Y[4]), 1);
                Data.AddEntry(new Entry(Time++, Z[4]), 2);

                Xes.Add(X[4]);
                Yes.Add(Y[4]);
                Zes.Add(Z[4]);
            }

            TimeRegulation();
        }

        

        public override IEnumerable<LineDataSet> CreateSets()
        {
            LineDataSet setX = new LineDataSet(null, "X");
            LineDataSet setY = new LineDataSet(null, "Y");
            LineDataSet setZ = new LineDataSet(null, "Z");

            List<LineDataSet> sets = new List<LineDataSet>();

            sets.Add(setX);
            sets.Add(setY);
            sets.Add(setZ);

            ModifyALineDataSet(setX, Color.Red);
            ModifyALineDataSet(setY, Color.Blue);
            ModifyALineDataSet(setZ, Color.Green);

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