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
    public partial class ListCategories : ContentPage
    {
      
        public ListCategories()
        {

            InitializeComponent();

            BindingContext = new CategoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }

    }
}