using GuardKeyProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardKeyProject.Services
{
    public interface ICategoryRepository
    {
        Task<List<string>> GetCategoriesAsync();


        Task<int> SaveCategoriesAsync(Category categories);

        Task DeleteCategoriesAsync(Category categories);

        Task<int> UpdateCategoriesAsync(Category Categories);


        Task<Category> GetCategory(int id);

        Task<List<Category>> GetAllCategoriesAsync();


    }
}
