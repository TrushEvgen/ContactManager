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

namespace ContactManager.Enums
{
    public class Enums
    {
       public enum RequestType : int
        {
            Add =1,
            Update = 2,
            Delete = 3
        };
    }
}