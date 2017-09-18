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
using SQLite;
using Android.Util;

namespace ContactManager.Data
{
    public class DataBase
    {
        public string FolderPath
        {
            get
            {
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            }
        }

        public string DbName
        {
            get
            {
                return "ContactManager.db";
            }
        }
        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    connection.CreateTable<Model.AccountModel>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("CreateDbError",ex.Message);
                return false;
            }
        }

        public bool Insert(Model.AccountModel value)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    connection.Insert(value);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("InserDbError", ex.Message);
                return false;
            }
        }

        public bool UpdateQuery(Model.AccountModel value)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    connection.Query<Model.AccountModel>("UPDATE AccountModel SET Name=?, Phone=? Where Id=?", value.Name, value.Phone, value.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("UpdateDbError", ex.Message);
                return false;
            }
        }
        public bool Update(Model.AccountModel value)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    connection.Update(value);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("UpdateDbError", ex.Message);
                return false;
            }
        }

        public bool Delete(Model.AccountModel value)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    connection.Delete(value);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("DeleteDbError", ex.Message);
                return false;
            }
        }

        public bool SelectAccount(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    connection.Query<Model.AccountModel>("SELECT * FROM AccountModel WHERE Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SelectAccountDbError", ex.Message);
                return false;
            }
        }

        public List<Model.AccountModel> GetAccounts()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(FolderPath, DbName)))
                {
                    return connection.Table<Model.AccountModel>().ToList();                    
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SelectFromDbError", ex.Message);
                return null;
            }
        }

    }
}