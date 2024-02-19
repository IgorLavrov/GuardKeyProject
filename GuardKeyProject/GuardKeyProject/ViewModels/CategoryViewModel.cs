using GuardKeyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuardKeyProject.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isViewDetail = false;
        public bool IsViewDetail
        {
            get { return isViewDetail; }
            set
            {
                isViewDetail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsViewDetail"));
            }
        }

        private string typeCommand = string.Empty;
        public string TypeCommand
        {
            get { return typeCommand; }
            set
            {
                typeCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TypeCommand"));
            }
        }
        //----------------------------------------
        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CategoryName"));
            }
        }


        private List<Category> categoryList;
        public List<Category> CategoryList
        {
            get { return categoryList; }
            set
            {


                categoryList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CategoryList"));


            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCategory"));
            }
        }
        //------------
        public Command cmdProcessTask { get; set; }
        public Command cmdCancelTask { get; set; }
        //-----------
        public Command cmdAddaTask { get; set; }
        public Command cmdDeleteaTask { get; set; }
        public Command cmdUpdateaTask { get; set; }
        public Command cmdMapTask { get; set; }

        public CategoryViewModel()
        {
            cmdProcessTask = new Command(ProcessCategories);
            cmdCancelTask = new Command(CancelCategories);
            //------------------
            cmdAddaTask = new Command(AddCategories);
            cmdDeleteaTask = new Command(DeleteCategories);
            cmdUpdateaTask = new Command(UpdateaTask);
            getCategories();
         

        }

        private void UpdateaTask(object obj)
        {
            IsViewDetail = true;
            TypeCommand = "Update";
            if (selectedCategory != null)
            {
                CategoryName = selectedCategory.CategoryName;


            }
            else
            {
                IsViewDetail = false;
                typeCommand = string.Empty;
            }
        }

        private async void DeleteCategories(object obj)
        {
            if (selectedCategory != null && selectedCategory.CategoryName != "All")
            {
                IsViewDetail = true;
                TypeCommand = "Delete";
                CategoryName = selectedCategory.CategoryName;
            }
            else
            {
                // Display a message when the "All" option is selected
                IsViewDetail = false;
                typeCommand = string.Empty;
                await Application.Current.MainPage.DisplayAlert("Cannot Delete", "The 'All' option cannot be deleted.", "OK");
            }


            //IsViewDetail = true;
            //TypeCommand = "Delete";
            //if (selectedCategory != null)
            //{
            //    CategoryName = selectedCategory.CategoryName;

            //}
            //else
            //{
            //    IsViewDetail = false;
            //    typeCommand = string.Empty;
            //}

        }

        private void AddCategories(object obj)
        {
            IsViewDetail = true;
            TypeCommand = "Add";
            CategoryName = string.Empty;
        }

        private void CancelCategories(object obj)
        {
            IsViewDetail = false;
            typeCommand = string.Empty;
        }

        private async void ProcessCategories(object obj)
        {

            if (TypeCommand == "Add")
            {
                var r = await App.CategoryService.SaveCategoriesAsync(new Category
                {
                    CategoryName = CategoryName,

                });

            }
            else if (TypeCommand == "Update")
            {
                selectedCategory.CategoryName = CategoryName;

                await App.CategoryService.UpdateCategoriesAsync(SelectedCategory);

            }
            else if (TypeCommand == "Delete")
            {
                await App.CategoryService.DeleteCategoriesAsync(SelectedCategory);
            }
            IsViewDetail = false;
            typeCommand = string.Empty;
            selectedCategory = null;
            getCategories();
        }
        public async void getCategories()
        {
            CategoryList = await App.CategoryService.GetAllCategoriesAsync();
        }






    }
}

