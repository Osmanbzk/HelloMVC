using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloMVC.Models
{
    public class Customer
    {
        public string Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "String Lenght is too long!")]
        [DisplayName("Enter Your Name")]
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}