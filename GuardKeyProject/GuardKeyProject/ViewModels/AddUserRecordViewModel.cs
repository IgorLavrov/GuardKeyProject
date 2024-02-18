using GuardKeyProject.Models;
using GuardKeyProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace GuardKeyProject.ViewModels
{
    public class AddUserRecordViewModel : BaseUserRecordViewModel
    {

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private ObservableCollection<string> _filterOptions;

        public ObservableCollection<string> FilterOptions
        {
            get => _filterOptions;
            set => setProperty(ref _filterOptions, value);
        }


        private async void InitializeFilterOptionsAsync()
        {
            // GetCategoriesAsync is asynchronous, so use await
            var categories = await App.CategoryService.GetCategoriesAsync();

            // Initialize the FilterOptions once the categories are retrieved
            FilterOptions = new ObservableCollection<string>(categories);
        }
        public AddUserRecordViewModel()
        {
            InitializeFilterOptionsAsync();

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            UserRecord = new UserRecord();
        }
        private async void OnSave()
        {
            var record = UserRecord;
            await App.Database.AddUserRecordAsync(record);

            Shell.Current.GoToAsync("..");
            //Shell.Current.GoToAsync(nameof(UserRecordPage));

            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //await navigationPage?.Navigation.PushAsync(new UserRecordPage());
        }

        private async void OnCancel()
        {
            //Shell.Current.GoToAsync(nameof(UserRecordPage));
            Shell.Current.GoToAsync("..");
            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //await navigationPage?.Navigation.PushAsync(new UserRecordPage());
        }




    }
}
