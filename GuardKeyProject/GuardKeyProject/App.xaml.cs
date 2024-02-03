using GuardKeyProject.Services;
using GuardKeyProject.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardKeyProject
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "record.db";
        public static UserRecordService _database;
        public static UserRecordService Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new UserRecordService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return _database;
            }
        }
        public App()
        {
            InitializeComponent();

            var pin = Preferences.Get("UserPIN", "");

            if (string.IsNullOrEmpty(pin))
            {

                MainPage = new NavigationPage(new RegistrarionPage());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
