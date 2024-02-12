using GuardKeyProject.Models;
using GuardKeyProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public Command UserRecordTappedDelete { get; }

        public Command ClearRecordCommand { get; }
      
        public Command SearchCommand { get; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }
     




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
            UserRecordTappedDelete = new Command<UserRecord>(OnDeleteUserRecord);
            ClearRecordCommand = new Command(ClearRecord);
            SearchCommand = new Command(ExecuteSearch);
            Navigation = _navigation;


        }
        private async void ExecuteSearch()
        {
            string searchText = SearchText;

            IEnumerable<UserRecord> prodlist;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search text is empty, load all records
                prodlist = await App.Database.GetUserRecordsAsync();
            }
            else
            {
                // If search text is not empty, perform search
                prodlist = await App.Database.SortRecord(searchText);
            }

            UpdateUserRecords(prodlist);
        }
        private void UpdateUserRecords(IEnumerable<UserRecord> records)
        {
            UserRecords.Clear();
            foreach (var prod in records)
            {
                UserRecords.Add(prod);
            }

            // Notify UI that UserRecords has changed
            OnPropertyChanged(nameof(UserRecords));
        }



        //private async void ExecuteSearch()
        //{

        //    string searchText = SearchText;
        //    if (string.IsNullOrEmpty(searchText))
        //    {
        //        UserRecords.Clear();
        //        var prodlist = await App.Database.GetUserRecordsAsync();
        //        foreach (var prod in prodlist)
        //        {
        //            UserRecords.Add(prod);
        //        }
        //    }
        //    else
        //    {
        //        UserRecords.Clear();
        //        var prodlist = await App.Database.SortRecord(searchText);
        //        foreach (var prod in prodlist)
        //        {
        //            UserRecords.Add(prod);
        //        }

        //    }

        //    //FilteredItems = new ObservableCollection<UserRecord>(UserRecords.Where(value => value.SourceGroupName.Contains(searchText)));

        //}

        private void ClearRecord()
        {
            UserRecords.Clear();
        }

        private async void OnDeleteUserRecord(UserRecord record)
        {
            if (record == null)
            {
                return;
            }
            await App.Database.DeleteUserRecordAsync(record.Id);
            await ExecuteLoadUserRecordCommand();
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
