using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.EnginesTypes;

namespace Web.Models
{
    public class CarModel
    {
        [UIHint("TextBoxEditor")]
        public string Name { get; set; }

        public double Weight { get; set; }
         
         [Range(1, 65535)]
        public int HorsePowers { get; set; }

        public EngineTypes EngineType { get; set; }

        public double TankVolume { get; set; }

        [UIHint("TextBoxEditor")]
        public string AdditionalInfo { get; set; }

        public static object ModelForCreating()
        {
            return new CarModel {EngineType = EngineTypes.V2};
        }
    }
}