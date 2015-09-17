using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain.CarTypes;
using Domain.EnginesTypes;

namespace Web.Models
{
    public class CarModel
    {
        public CarModel(IList<SelectListItem> pilots)
        {
            Pilots = pilots;
        }

        public CarModel(Car car)
        {
            Name = car.Name;
            Weight = car.Weight;
            HorsePowers = car.Engine.HorsePowers;
            EngineType = (EngineTypes)car.Engine.NumberOfCylinders;
            TankVolume = car.FuelTank;
            AdditionalInfo = car.SpecialAdds;
            PilotId = car.OwnerPilot.Id;
        }

        [Obsolete]
        public CarModel()
        {
        }

        [Required]
        public string Name { get; set; }
        [Range(1, 65535)]
        public double Weight { get; set; }

        [Range(1, 65535)]
        public int HorsePowers { get; set; }
        [Range(1,24)]
        public EngineTypes EngineType { get; set; }

        public double TankVolume { get; set; }

        [UIHint("TextBoxEditor")]
        public string AdditionalInfo { get; set; }


        public long PilotId { get; set; }

        [Display(Name = "Owner")]
        public IList<SelectListItem> Pilots { get; set; }
    }
}