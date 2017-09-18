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
using ContactManager;

namespace ContactManager.Resources.Adapters
{
    class MainWindowListViewAdapter : BaseAdapter
    {

        //Context context;
        private Activity Activity;
        private List<Model.AccountModel> AccountList;

        public MainWindowListViewAdapter(Activity activity, List<Model.AccountModel> accountList)
        {
            this.Activity = activity;
            this.AccountList = accountList;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return AccountList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? Activity.LayoutInflater.Inflate(Resource.Layout.MainWindowListViewDataTemplate, parent, false);

            var name = view.FindViewById<TextView>(Resource.Id.tv_Name);
            var phone = view.FindViewById<TextView>(Resource.Id.tv_phone);
            name.Text = AccountList[position].Name;
            phone.Text = AccountList[position].Phone;


            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return AccountList.Count;
            }
        }

    }

    //class MainWindowListViewAdapterViewHolder : Java.Lang.Object
    //{
    //    public TextView Name { get; set; }
    //    public EditText Phone { get; set; }
    //    //Your adapter views to re-use
    //    //public TextView Title { get; set; }
    //}
}