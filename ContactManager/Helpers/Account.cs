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
using Newtonsoft.Json;

namespace ContactManager.Helpers
{
    public static class Helpers
    {
        public static Model.AccountModel ConvertToModel(this Intent data)
        {
            string z = nameof(Model.AccountModel);
            var jsonObject = data.GetStringExtra(nameof(Model.AccountModel));
            if (!string.IsNullOrEmpty(jsonObject))
                return JsonConvert.DeserializeObject<Model.AccountModel>(jsonObject);
            else
                return new Model.AccountModel();
        }

        public static string ConvertToObject(this Model.AccountModel am)
        {
            return JsonConvert.SerializeObject(am);
        }
    }
}