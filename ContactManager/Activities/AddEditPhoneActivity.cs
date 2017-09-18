using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ContactManager;

namespace ContactManager
{
    [Activity(Label = "Редактирование записи")]
    public class AddEditPhoneActivity : Activity
    {
        EditText personName;
        EditText phone;
        Model.AccountModel accountModel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewWindow);
            personName = FindViewById<EditText>(Resource.Id.editText1);
            phone = FindViewById<EditText>(Resource.Id.editText2);
            Button btn = FindViewById<Button>(Resource.Id.button1);
            btn.Click += Btn_Click;

            //Intent.Convert
            
            accountModel = Helpers.Helpers.ConvertToModel(Intent);
            personName.Text = accountModel.Name;
            phone.Text = accountModel.Phone;
            
            //Id = Intent.GetIntExtra(nameof(accountModel.Id), -1);
            //if (Id!=-1)
            //{
            //    personName.Text = (Intent.GetStringExtra(nameof(accountModel.Name)));
            //    phone.Text = (Intent.GetStringExtra(nameof(accountModel.Phone)));
            //}
            

            // Create your application here
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            if (accountModel == null)
                accountModel = new Model.AccountModel(personName.Text, phone.Text);
            else
            {
                accountModel.Name = personName.Text;
                accountModel.Phone = phone.Text;
            }
            intent.PutExtra(nameof(Model.AccountModel), Helpers.Helpers.ConvertToObject(accountModel));

            //intent.PutExtra(nameof(am.Name),am.Name);
            //intent.PutExtra(nameof(am.Phone),am.Phone);
            //if (Id!=-1)
            //intent.PutExtra(nameof(am.Id), Id);

            SetResult(Result.Ok, intent);
            Finish();
            //Intent intent = new Intent(this, typeof(MainActivity));
            //StartActivity(intent);
    }
    }
}