using GuardKeyProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardKeyProject.Services
{
    public class CategoryService : ICategoryRepository
    {
        readonly SQLiteAsyncConnection _database;

        public CategoryService(SQLiteAsyncConnection database)
        {
            _database = database;
            Task.Run(async () => await InitializeDatabaseAsync()).Wait();
            // Other initialization code if needed
        }

        public async Task InitializeDatabaseAsync()
        {
            await _database.CreateTableAsync<Category>();
            await SeedCategoriesAsync();
        }

        public async Task SeedCategoriesAsync()
        {
            var existingCategories = await _database.Table<Category>().ToListAsync();

            if (existingCategories.Count == 0)
            {
                // Add some initial categories
                var initialCategories = new List<Category>
        {
            new Category { CategoryName = "All" },
            new Category { CategoryName = "Home" },
            new Category { CategoryName = "Groceries" },
            
        };

                await _database.InsertAllAsync(initialCategories);
            }
        }

     

        public async Task<List<string>> GetCategoriesAsync()
        {
            var categories = await _database.Table<Category>().ToListAsync();
            return categories.Select(c => c.CategoryName).ToList();
        }
        public Task<int> SaveCategoriesAsync(Category categories)
        {
            return _database.InsertAsync(categories);

        }
        public async Task DeleteCategoriesAsync(Category categories)
        {
            await _database.DeleteAsync(categories);
        }
        public async Task<int> UpdateCategoriesAsync(Category categories)
        {
            return await _database.UpdateAsync(categories);
        }
        public async Task<Category> GetCategory(int id)

        {
            return await _database.GetAsync<Category>(id);
            //return await _database.FindAsync<TaskModel>(id);
        }
    }
}
