using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW7.Models
{
    public class Request
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string SearchTerms { get; set; }

        [Required]
        public DateTime RequestedOn { get; set; }

        [Required]
        public string UserBrowser { get; set; }

        [Required]
        public string UserAddress { get; set; }
    }
}