using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PassionProject_PhoneBlog_n01364240.Data
{
    public class PassionProjectContext : DbContext
    {
        public PassionProjectContext() : base("name=PassionProjectContext")
        {
        }
            public System.Data.Entity.DbSet<PassionProject_PhoneBlog_n01364240.Models.Brand> Brands { get; set; }
            public System.Data.Entity.DbSet<PassionProject_PhoneBlog_n01364240.Models.Feature> Features { get; set; }
            public System.Data.Entity.DbSet<PassionProject_PhoneBlog_n01364240.Models.Phone> Phones { get; set; }
    }
}
