using GuardKeyProject.Models;
using GuardKeyProject.Views;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

    
        public ObservableCollection<string> FilterOptions { get; }
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

        //private ObservableCollection<UserRecord> _userRecord;
        //public ObservableCollection<UserRecord> UserRecords
        //{
        //    get { return _userRecord; }
        //    set
        //    {
        //        _userRecord = value;
        //        OnPropertyChanged(nameof(UserRecords));
        //    }
        //}

        string selectedFilter = "All";
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (setProperty(ref selectedFilter, value))
                    FilterItemsAsync();
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
            FilterOptions = new ObservableCollection<string>()
    {
        "All",
        "affa",
        "shsh",
        "Editor",
        "Student"
    };


        }

        async Task FilterItemsAsync()
        {
            if (SelectedFilter == "All")
            {
                // Load all records
                ExecuteLoadUserRecordCommand();

                // Reset SelectedFilter to allow choosing a different option
                SelectedFilter = null;
            }
            else
            {
                IEnumerable<UserRecord> filteredRecords;
                // Filter records based on the selected option
                filteredRecords = await App.Database.SortRecordByPicker(SelectedFilter);

                // Clear the existing records and add the filtered ones
                UserRecords.Clear();
                foreach (var record in filteredRecords)
                {
                    UserRecords.Add(record);
                }
            }
        }

        private async void ExecuteSearch()
        {
            string searchText = SearchText;

            IEnumerable<UserRecord> prodlist;

            if (searchText==null)
            {
               
                prodlist = await App.Database.GetUserRecordsAsync();
            }
            else
            {
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            { IsBusy = false; }
        }


    }
}
