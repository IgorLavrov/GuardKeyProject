using GuardKeyProject.Models;
using GuardKeyProject.ViewModels;
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
    public partial class AddUserRecordPage : ContentPage
    {

        public UserRecord UserRecord { get; set; }
        private AddUserRecordViewModel viewModel;
        //public AddUserRecordPage()
        //{
        //    InitializeComponent();
        //    BindingContext = new AddUserRecordViewModel();
        //}

        //public AddUserRecordPage(UserRecord record)
        //{
        //    InitializeComponent();
        //    BindingContext = new AddUserRecordViewModel();

        //    if (record != null)
        //    {

        //        ((AddUserRecordViewModel)BindingContext).UserRecord = record;
        //    }
        //}
        public AddUserRecordPage()
        {
            InitializeComponent();
            viewModel = new AddUserRecordViewModel();
            BindingContext = viewModel;
            UserRecord = viewModel.UserRecord; // Initialize UserRecord here
        }

        public AddUserRecordPage(UserRecord record)
        {
            InitializeComponent();
            viewModel = new AddUserRecordViewModel();

            if (record != null)
            {
                viewModel.UserRecord = record;
            }

            BindingContext = viewModel;
            UserRecord = viewModel.UserRecord; // Initialize UserRecord here
        }

        private void OnFilterOptionsPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterOptionsPicker.SelectedItem != null)
            {
                // Update UserRecord.SourceGroupName with the selected item
                UserRecord.SourceGroupName = filterOptionsPicker.SelectedItem.ToString();
            }
        }


    }
}