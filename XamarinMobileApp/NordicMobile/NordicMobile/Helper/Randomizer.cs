using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NordicMobile.Helper
{
    static class Randomizer
    {
        private static Random random = new Random();

        public static int RandomUpTo2()
        {
            return random.Next(-2, 3);
        }

        public static int RandomUpTo11()
        {
            return random.Next(0, 12);
        }
    }
}