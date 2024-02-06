using GuardKeyProject.Models;
using GuardKeyProject.Views;
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
        public Command UserRecordTappedEdit { get; }

        private ObservableCollection<UserRecord> _userRecord;
        public ObservableCollection<UserRecord> UserRecords
        {
            get { return _userRecord; }
            set
            {
                _userRecord = value;
                OnPropertyChanged(nameof(UserRecords));
            }
        }
        public UserRecordViewModel(INavigation _navigation)
        {

            LoadUserRecordCommand = new Command(async () => await ExecuteLoadUserRecordCommand());
            UserRecords = new ObservableCollection<UserRecord>();
            AddUserRecordCommand = new Command(OnAddUserRecord);
            UserRecordTappedEdit=new Command<UserRecord> (OnEditUserRecord);
            Navigation = _navigation;


        }

        private async void OnEditUserRecord(UserRecord record)
        {
            await Navigation.PushAsync(new AddUserRecordPage(record));
        }

        /// <summary>
        /// // review

        private async void OnAddUserRecord(object obj)
        {
            Shell.Current.GoToAsync(nameof(AddUserRecordPage));
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
