using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_PhoneBlog_n01364240.Models
{
    public class Feature
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public string FeatureDetails { get; set; }

        //one feature can be present in many phones
        public ICollection<Phone> Phones { get; set; }
    }
}
