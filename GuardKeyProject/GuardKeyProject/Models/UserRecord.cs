using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardKeyProject.Models
{
    
    
        [Table("UserRecord")]
        public class UserRecord
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string SourceGroupName { get; set; }
            public string ResourceName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Description { get; set; }
        }



    public class GroupedUserRecord : List<UserRecord>
    {
        public string Key { get; private set; }


        public GroupedUserRecord(string key, IEnumerable<UserRecord> items) : base(items)
        {
            Key = key;
        }
    }
}


