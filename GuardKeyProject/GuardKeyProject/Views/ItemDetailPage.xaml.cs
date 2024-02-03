using GuardKeyProject.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GuardKeyProject.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}