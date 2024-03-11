using GuardKeyProject.Models;
using GuardKeyProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuardKeyProject.ViewModels
{
    public class CategoryViewModel : BaseUserRecordViewModel,INotifyPropertyChanged
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


        private ObservableCollection<Category> categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set
            {
                categoryList = value;
                OnPropertyChanged(nameof(CategoryList));
            }
        }
     
        public Command cmdProcessTask { get; set; }
        public Command cmdCancelTask { get; set; }
        //-----------
        public Command cmdAddaTask { get; set; }
        public Command cmdDeleteaTask { get; set; }
        public Command cmdUpdateaTask { get; set; }
        public Command cmdMapTask { get; set; }
        public Command OpenCategoryPageCommand { get; set; }


        public CategoryViewModel()
        {
            cmdProcessTask = new Command(ProcessCategories);
            cmdCancelTask = new Command(CancelCategories);
  
            cmdAddaTask = new Command(AddCategories);
            cmdDeleteaTask = new Command(DeleteCategories);
            cmdUpdateaTask = new Command(UpdateaTask);
            OpenCategoryPageCommand = new Command(OpenCategoryPage);

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

        }

        private async void AddCategories(object obj)
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
            List<string> categoriesNames = new List<string>();
            categoriesNames = await App.CategoryService.GetCategoriesAsync();

            if (TypeCommand =="Add" || TypeCommand == "Update")
            {
                foreach (var item in categoriesNames)
                {
                    if (item == CategoryName)
                    {
                        await Application.Current.MainPage.DisplayAlert("Cannot Add", "Such category already existed.", "OK");
                        typeCommand = string.Empty;
                        break;

                    }
                }
            }

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
            var categories = await App.CategoryService.GetAllCategoriesAsync();

            // Convert List<Category> to ObservableCollection<Category>
            var observableCollection = new ObservableCollection<Category>(categories);

            // Assign the ObservableCollection to CategoryList
            CategoryList = observableCollection;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //private Category selectedCategory;
        //public Category SelectedCategory
        //{
        //    get { return selectedCategory; }
        //    set
        //    {
        //        selectedCategory = value;
        //        OnPropertyChanged(nameof(SelectedCategory));
        //    }
        //}

        private string selectedItem ;
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (setProperty(ref selectedItem, value))
                OnPropertyChanged(nameof(SelectedItem));
                FilterItemsAsync();
            }
              
        }

        public async Task FilterItemsAsync()
        {

            // Implement your filtering logic based on the selected filter
            // For example, you can update the CategoryList property with filtered data

            if (string.IsNullOrEmpty(selectedItem) || selectedItem == "All")
            {
                // Reset the filter and retrieve all categories
                getCategories();
            }
            else
            {
                var filteredCategories = await App.CategoryService.FilterCategoriesAsync(selectedItem);
                CategoryList = new ObservableCollection<Category>(filteredCategories);
            }
        }




        private int tapCount = 0;
        private const int maxTapCount = 1;
        private const int resetIntervalMilliseconds = 500; // Adjust the interval as needed

        private async void OpenCategoryPage(object obj)
        {
            tapCount++;

          
            if (tapCount == maxTapCount)
            {
               
             
               await Shell.Current.GoToAsync($"{nameof(ListPage)}");
                //await Shell.Current.GoToAsync($"//{nameof(UserRecordPage)}");
                tapCount = 0; // Reset tap count after navigation
            }
        }

    }
}

