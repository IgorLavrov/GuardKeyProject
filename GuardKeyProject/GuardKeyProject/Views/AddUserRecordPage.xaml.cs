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
        public AddUserRecordPage()
        {
            InitializeComponent();
            BindingContext = new AddUserRecordViewModel();
        }

        public AddUserRecordPage(UserRecord record)
        {
            InitializeComponent();
            BindingContext = new AddUserRecordViewModel();

            if(record != null ) 
            {

                ((AddUserRecordViewModel)BindingContext).UserRecord = record;
            }
        }
    }
}