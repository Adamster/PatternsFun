// File: CarFactory.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;
using Domain;
using Domain.CarTypes;
using Domain.EnginesTypes;
using InterfacesActions;

namespace Factories
{
    public class CarFactory
    {
        public CarFactory(ICarActionOnCreation carActionOnCreation)
        {
            _fillCarTank = carActionOnCreation;
        }

        private readonly ICarActionOnCreation _fillCarTank;

        public Car CreateNewCar(int fuelTankVolume, double weight, int horsePower,
            EngineTypes engineType, string name, Action<IParams> optionalParam)
        {
            var car = new Car(fuelTankVolume, weight, CreateGasolineEngine(horsePower, engineType), name,
                OptParamStr(optionalParam));
            OnCarCreation(car);

            return car;
        }

        private string OptParamStr(Action<IParams> optionalParam)
        {
            var optParams = new VehicleParams();
            if (optionalParam != null)
                optionalParam(optParams);
            var optParamStr = optParams.GetParams();
            if (string.IsNullOrWhiteSpace(optParamStr))
                optParamStr = "No additinional specs";
            return optParamStr;
        }

        public SportCar CreateNewSportCar(int fueltankVolume, double weight, int horsePower,
            EngineTypes engineType, string name, Action<IParams> optionalParam)
        {
            var sportCar = new SportCar(fueltankVolume, weight, CreateGasolineEngine(horsePower, engineType), name,
                OptParamStr(optionalParam));
            OnCarCreation(sportCar);
            return sportCar;
        }

        private GasolineEngine CreateGasolineEngine(int horsePowers, EngineTypes engineType)
        {
            var engine = new GasolineEngine(horsePowers, engineType);
            OnEngineCreation(engine);
            return engine;
        }

        private ElectroEngine CreateElectroEngine(int horsePower)
        {
            var engine = new ElectroEngine(horsePower);
            return engine;
        }

        private static void OnEngineCreation(GasolineEngine engine)
        {
            //test engine on stand
        }

        private void OnCarCreation(Car car)
        {
            _fillCarTank.FillCarTank(car);
        }

        public class VehicleParams : IParams
        {
            public IParams WithParams(Func<string> paramsDelegate)
            {
                _params = paramsDelegate();
                return this;
            }

            public string GetParams()
            {
                return _params;
            }

            private string _params;
        }
    }
}