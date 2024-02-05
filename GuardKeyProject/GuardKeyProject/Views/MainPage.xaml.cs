﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuardKeyProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();

            this.Title = "Select Option";
            StackLayout stackLayout = new StackLayout();

            var absoluteLayout = new AbsoluteLayout();

            // Background Image
            var backgroundImage = new Image
            {
                Source = "tower.jpg", // replace with your image source
                Aspect = Aspect.AspectFill,
                Opacity = 0.20
            };
            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, 1));
            absoluteLayout.Children.Add(backgroundImage);

            Button button = new Button();
            button.Text = "Add Cart";
            button.HeightRequest = 60;
            button.WidthRequest = 170;
            button.BackgroundColor = Color.Goldenrod;
            button.Clicked += Button_Clicked;
            stackLayout.Children.Add(button);

            button = new Button();
            button.Text = "Get";
            button.HeightRequest = 60;
            button.WidthRequest = 170;
            button.BackgroundColor = Color.Goldenrod;
            button.Clicked += Button_Get_Clicked;
            stackLayout.Children.Add(button);

            button = new Button();
            button.Text = "Edit";
            button.BackgroundColor = Color.Goldenrod;
            button.HeightRequest = 60;
            button.WidthRequest = 170;
            button.Clicked += Button_Edit_Clicked;
            stackLayout.Children.Add(button);

            button = new Button();
            button.Text = "Delete";
            button.HeightRequest = 60;
            button.WidthRequest = 170;
            button.BackgroundColor = Color.Goldenrod;



            button.Clicked += Button_Delete_Clicked;
            stackLayout.Children.Add(button);

            stackLayout.VerticalOptions = LayoutOptions.CenterAndExpand;
            stackLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;

            AbsoluteLayout.SetLayoutFlags(stackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(stackLayout, new Rectangle(1, 1, 1, 1));
            absoluteLayout.Children.Add(stackLayout);

            // Set the AbsoluteLayout as the page content
            Content = absoluteLayout;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new AddUserRecordPage());
        }
        private async void Button_Get_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRecordPage());
        }
        private async void Button_Edit_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new EditCompanyPage());
        }
        private async void Button_Delete_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new DeleteCompanyPage());
        }
    }
}