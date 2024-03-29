﻿using GuardKeyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardKeyProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRecordPage : ContentPage

    {

       UserRecordViewModel userRecordViewModel;
        public UserRecordPage()
        {
           
            InitializeComponent();
            BindingContext= userRecordViewModel=new UserRecordViewModel(Navigation);
            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            userRecordViewModel.RefreshFilterOptionsAsync();
            userRecordViewModel.OnAppearing();
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();

        //    // Cleanup or handle anything when the page disappears
        //}

    }   
}