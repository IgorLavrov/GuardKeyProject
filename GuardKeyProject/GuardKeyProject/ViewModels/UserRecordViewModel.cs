using GuardKeyProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardKeyProject.ViewModels
{
    public class UserRecordViewModel: BaseUserRecordViewModel
    {
        public Command LoadUserRecordCommand { get; }
        public Command AddUserRecordCommand { get; }

        public ObservableCollection<UserRecord> UserRecords { get; }
        public UserRecordViewModel()
        {

            LoadUserRecordCommand = new Command(async () => await ExecuteLoadUserRecordCommand());
            UserRecords = new ObservableCollection<UserRecord>();
            AddUserRecordCommand = new Command(OnAddUserRecord);


        }
        /// <summary>
        /// // review

        private async void OnAddUserRecord(object obj)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new AddUserRecordPage());
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadUserRecordCommand()
        {
            IsBusy = true;
            try
            {
                UserRecords.Clear();
                var prodlist = await App.Database.GetUserRecordsAsync();
                foreach (var prod in prodlist)
                {
                    UserRecords.Add(prod);
                }
            }
            catch
            {
                throw;

            }
            finally
            { IsBusy = false; }
        }


    }
}
