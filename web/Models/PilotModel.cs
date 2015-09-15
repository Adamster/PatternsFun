using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PilotModel
    {   
       
        [Required]
        public string Name { get; set; }
       
        [Range(18,100)]
        public int Age { get; set; }
        [UIHint("TextBoxEditor")]
        public string Team { get; set; }
        [Display(Name = "Debut date")]
        public DateTime DebutDate { get; set; }
    }
}