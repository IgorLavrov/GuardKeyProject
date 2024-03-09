using GuardKeyProject.Models;
using GuardKeyProject.Views;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardKeyProject.ViewModels
{
    public class UserRecordViewModel: CategoryViewModel
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

        public string Key { get; set; }
        public ObservableCollection<string> FilterOptions { get; set; }

        private ObservableCollection<GroupedUserRecord> _groupedUserRecords;
        public ObservableCollection<GroupedUserRecord> GroupedUserRecords
        {
            get { return _groupedUserRecords; }
            set
            {
                _groupedUserRecords = value;
                OnPropertyChanged(nameof(GroupedUserRecords));
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

    


        private async void InitializeFilterOptionsAsync()
        {
            // GetCategoriesAsync is asynchronous, so use await
            var categories = await App.CategoryService.GetCategoriesAsync();

            // Initialize the FilterOptions once the categories are retrieved
            FilterOptions = new ObservableCollection<string>(categories);
        }
       
       


            public UserRecordViewModel(INavigation _navigation)
        {
            InitializeFilterOptionsAsync();
            LoadUserRecordCommand = new Command(async () => await ExecuteLoadUserRecordCommand());
            UserRecords = new ObservableCollection<UserRecord>();
            AddUserRecordCommand = new Command(OnAddUserRecord);
            UserRecordTappedEdit=new Command<UserRecord> (OnEditUserRecord);
            GroupedUserRecords = new ObservableCollection<GroupedUserRecord>();
            UserRecordTappedDelete = new Command<UserRecord>(OnDeleteUserRecord);
            ClearRecordCommand = new Command(ClearRecord);
            SearchCommand = new Command(ExecuteSearch);
            Navigation = _navigation;
            


        }

        public UserRecordViewModel()
        {
           
        }

        public async Task RefreshFilterOptionsAsync()
        {
            var categories = await App.CategoryService.GetCategoriesAsync();
            FilterOptions = new ObservableCollection<string>(categories);
            OnPropertyChanged(nameof(FilterOptions));
        }

        async Task FilterItemsAsync()
        {


            try
            {
                IsBusy = true;

                if (SelectedFilter == "All")
                {
                    // Load all records
                    await ExecuteLoadUserRecordCommand();
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    


        private async void ExecuteSearch()
        {
            string searchText = SearchText;

            IEnumerable<UserRecord> prodlist;

            //if (searchText=="")
            //{
               
            //    prodlist = await App.Database.GetUserRecordsAsync();
            //}
            //else
            //{
                prodlist = await App.Database.SortRecord(searchText);
            //}

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



    

        public void ClearRecord()
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


        private async void OnAddUserRecord(object obj)
        {
            Shell.Current.GoToAsync(nameof(AddUserRecordPage));
            //await Application.Current.MainPage.Navigation.PushAsync(new AddUserRecordPage());
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        //async Task ExecuteLoadUserRecordCommand()
        //{
           

        //    IsBusy = true;
        //    try
        //    {

        //        UserRecords.Clear();
        //        var prodlist = await App.Database.GetUserRecordsAsync();
        //        foreach (var prod in prodlist)
        //        {
        //            UserRecords.Add(prod);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    { IsBusy = false; }
        //}

        async Task ExecuteLoadUserRecordCommand()
        {
            IsBusy = true;
            try
            {
                var prodlist = await App.Database.GetUserRecordsAsync();

                // Update UserRecords with the loaded records
                UserRecords.Clear();
                foreach (var prod in prodlist)
                {
                    UserRecords.Add(prod);
                }

                // Group the user records
                var groupedRecords = UserRecords
                    .GroupBy(record => record.SourceGroupName)
                    .Select(group => new GroupedUserRecord(group.Key, group.ToList()));

                // Update GroupedUserRecords with the grouped records
                GroupedUserRecords = new ObservableCollection<GroupedUserRecord>(groupedRecords);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
       

    }
}
