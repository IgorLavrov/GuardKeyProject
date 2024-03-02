using GuardKeyProject.Models;
using GuardKeyProject.Services;
using GuardKeyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardKeyProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        


        UserRecordViewModel userRecordViewModel;
        public ListPage()
        {
            InitializeComponent();
            userRecordViewModel = new UserRecordViewModel(Navigation);
            BindingContext = userRecordViewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            userRecordViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
{
            base.OnDisappearing();
            userRecordViewModel.ClearRecord();

        }

    }
}
    