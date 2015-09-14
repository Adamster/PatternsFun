using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PilotModel
    {
        [UIHint("TextBoxEditor")]
        public string Name { get; set; }
        [UIHint("TextBoxEditor")]
        public string Age { get; set; }
        [UIHint("TextBoxEditor")]
        public string Team { get; set; }
        [UIHint("DateTimeEditor")]
        [DataType(DataType.Date)]
        public DateTime DebutDate { get; set; }
    }
}