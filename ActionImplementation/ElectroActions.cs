using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CarTypes;
using InterfacesActions;

namespace ActionImplementation
{
    public class ElectroActions : IElectroCarActionOnCreation
    {
        public void ChargeCar(ElectroCar eCar)
        {
            eCar.Charge(100);
            Console.WriteLine(eCar.Name +" charged!");
        }
    }
}
