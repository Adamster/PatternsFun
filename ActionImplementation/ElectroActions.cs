using System;
using Domain.CarTypes;
using InterfacesActions;

namespace ActionImplementation
{
    public class ElectroActions : IElectroCarActionOnCreation
    {
        public void ChargeCar(ElectroCar eCar)
        {
            eCar.Charge(100);
            Console.WriteLine(eCar.Name + " charged!");
        }
    }
}