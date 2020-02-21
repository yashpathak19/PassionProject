using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_PhoneBlog_n01364240.Models.ViewModels
{
    public class ShowPhone
    {
        //one phone
        public virtual Phone phone { get; set; }
        //list of feature a phone has
        public List<Feature> features { get; set; }

        // list of all features for a phone
        public List<Feature> all_features { get; set; }

    }
}