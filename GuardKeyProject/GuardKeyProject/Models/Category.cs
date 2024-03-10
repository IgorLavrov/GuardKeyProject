using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardKeyProject.Models
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string CategoryName { get; set; }

    }

}
