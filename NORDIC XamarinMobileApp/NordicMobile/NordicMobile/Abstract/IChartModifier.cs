using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikePhil.Charting.Data;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace NordicMobile.Abstract
{
    interface IChartModifier
    {
        void AddEntry(float[] heartData);
        IEnumerable<LineDataSet> CreateSets();
        void ModifyALineDataSet(LineDataSet set, Color color);
        
    }
}