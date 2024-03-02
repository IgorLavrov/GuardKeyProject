using GuardKeyProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuardKeyProject.Services
{
    public static class NavigationService
    {
        public static async Task GoBackAsync()
        {
            //await Shell.Current.GoToAsync("..");
            await Shell.Current.Navigation.PopAsync();
            
        }
    }
}
