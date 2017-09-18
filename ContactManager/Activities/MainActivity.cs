using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ContactManager.Data;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ContactManager.Helpers;

namespace ContactManager
{
    [Activity(Label = "Менеджер контактов", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {       
        Button addButton;
        Button editButton;
        Button deleteButton;
        DataBase db;
        ListView listViewData;
        List<Model.AccountModel> AccountList = new List<Model.AccountModel>();

        Model.AccountModel SelectedAccount = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            db = new DataBase();
            db.CreateDataBase();

            listViewData = FindViewById<ListView>(Resource.Id.listView);
            // Get our button from the layout resource,
            // and attach an event to it
            
            addButton = FindViewById<Button>(Resource.Id.btnAdd);
            editButton = FindViewById<Button>(Resource.Id.btnEdit);
            deleteButton = FindViewById<Button>(Resource.Id.btnDelete);
            addButton.Click += AddButton_Click;
            editButton.Click += EditButton_Click;
            deleteButton.Click += delegate
            {
                if (SelectedAccount != null)
                    db.Delete(SelectedAccount);
                LoadData();
            };
            LoadData();         

            listViewData.ItemClick += ListViewData_ItemClick;
            
        }



        int oldID = -1;
        private void ListViewData_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (oldID != -1)
            { 
                listViewData.GetChildAt(oldID).SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            listViewData.GetChildAt(e.Position).SetBackgroundColor(Android.Graphics.Color.DarkGray);
            SelectedAccount = AccountList.Where(x => x.Id == e.Id).FirstOrDefault();

            oldID = e.Position;


        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AddEditPhoneActivity));
            StartActivityForResult(intent, (int)Enums.Enums.RequestType.Add);
        }
        private void EditButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AddEditPhoneActivity));
            intent.PutExtra(nameof(Model.AccountModel), Helpers.Helpers.ConvertToObject(SelectedAccount));
            //intent.PutExtra(nameof(SelectedAccount.Id), SelectedAccount.Id);
            //intent.PutExtra(nameof(SelectedAccount.Name), SelectedAccount.Name);
            //intent.PutExtra(nameof(SelectedAccount.Phone), SelectedAccount.Phone);

            StartActivityForResult(intent, (int)Enums.Enums.RequestType.Update);
        }
        //метод который вызывается после завершения вызова нового окна
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (data == null)
                return;
            if (resultCode != Result.Ok)
                return;
            switch ((Enums.Enums.RequestType)requestCode)
            {
                case Enums.Enums.RequestType.Add:
                    AddAccountToDataBase(data);
                    break;
                case Enums.Enums.RequestType.Update:
                    UpdateAccountInDataBase(data);
                    break;
            }
            LoadData();

        }

        private void AddAccountToDataBase(Intent data)
        {
            Model.AccountModel am = Helpers.Helpers.ConvertToModel(data);
            db.Insert(am);
        }
        private void UpdateAccountInDataBase(Intent data)
        {
            Model.AccountModel am;
            am = Helpers.Helpers.ConvertToModel(data);
            //int iD = -1;
            //iD = data.GetIntExtra(nameof(am.Id),-1);
            //if (iD == -1)
            //    return;
            //am = AccountList.Where(x => x.Id == iD).FirstOrDefault();
            //am.Name = data.GetStringExtra(nameof(am.Name));
            //am.Phone = data.GetStringExtra(nameof(am.Phone));
            //db.UpdateQuery(am);
            db.Update(am);
        }
        
        private void LoadData()
        {
            AccountList = db.GetAccounts();
            var adapter = new Resources.Adapters.MainWindowListViewAdapter(this, AccountList);
            listViewData.Adapter = adapter;
        }
    }
}

