using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_PhoneBlog_n01364240.Models
{
    public class Phone
    {
        [Key]
        public int PhoneID { get; set; }
        public string PhoneName { get; set; }
        public DateTime PhoneReleaseDate { get; set; }

        //phonebattery is in mAh
        public int PhoneBattery { get; set; }

        //phone weight is in gram (i.e  1200 gram = 1.2 kg)
        public int PhoneWeight { get; set; }

        //many to one (many phones belongs to one brand)     
        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public virtual Brand Brands { get; set; }

        //Representing the "Many" in (Many Owners to Many Pets)
        public ICollection<Feature> Features { get; set; }
    }
}