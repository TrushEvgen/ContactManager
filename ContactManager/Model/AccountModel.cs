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
using Java.IO;
using SQLite;

namespace ContactManager.Model
{
  
    public class AccountModel
    {
        public AccountModel(string name,string phone)
        {
            Name = name;
            Phone = phone;
        }
        public AccountModel()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
       

    }
}