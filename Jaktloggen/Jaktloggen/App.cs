﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaktloggen.Data;
using Jaktloggen.IO;
using Xamarin.Forms;

namespace Jaktloggen
{
    public class App : Application
    {
        public static bool SyncWithServer { get; set; }
        public App()
        {
            SyncWithServer = true;
            Database.Init();
            MainPage = new MainPage();
            
        }

        private static FileRepository _database;
        public static FileRepository Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new FileRepository();
                }
                return _database;
            }
        }

        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
