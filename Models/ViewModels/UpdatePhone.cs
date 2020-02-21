using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_PhoneBlog_n01364240.Models.ViewModels
{
    public class UpdatePhone
    {
        //one phone
        public Phone Phone { get; set; }
        //many brands
        public List<Brand> Brands { get; set; }
    }
}
