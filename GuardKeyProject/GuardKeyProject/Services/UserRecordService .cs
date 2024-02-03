using GuardKeyProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardKeyProject.Services
{
    public class UserRecordService : IUserRecordRepository
    {
        public SQLiteAsyncConnection _database;
        public UserRecordService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserRecord>().Wait();


        }
        public async Task<bool> AddUserRecordAsync(UserRecord record)
        {
            if (record.Id > 0)
            {
                await _database.UpdateAsync(record);
            }
            else
            {
                await _database.InsertAsync(record);

            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUserRecordAsync(int id)
        {
            await _database.DeleteAsync<UserRecord>(id);
            return await Task.FromResult(true);
        }

        public async Task<UserRecord> GetUserRecordAsync(int id)
        {
            return await _database.Table<UserRecord>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserRecord>> GetUserRecordsAsync()
        {
            return await Task.FromResult(await _database.Table<UserRecord>().ToListAsync());
        }

        public async Task<bool> UpdateUserRecordAsync(UserRecord record)
        {
            throw new NotImplementedException();
            //if (record.Id > 0)
            //{
            //    await _database.UpdateAsync(record);
            //}
            //else
            //{
            //    await _database.InsertAsync(record);

            //}
            //return await Task.FromResult(true);
        }

    }
}
