using GuardKeyProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardKeyProject.Services
{
    public interface IUserRecordRepository
    {
        Task<bool> AddUserRecordAsync(UserRecord record);
        Task<bool> UpdateUserRecordAsync(UserRecord record);

        Task<bool> DeleteUserRecordAsync(int id);

        Task<UserRecord> GetUserRecordAsync(int id);

        Task<IEnumerable<UserRecord>> GetUserRecordsAsync();


    }
}
