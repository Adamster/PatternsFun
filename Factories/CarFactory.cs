using System;
using Domain.CarTypes;
using Domain.EnginesTypes;
using Domain.Persons;
using InterfacesActions;

namespace Factories
{
    public class CarFactory
    {
        private readonly IElectroCarActionOnCreation _chargeCar;
        private readonly ICarActionOnCreation _fillCarTank;

        public CarFactory(ICarActionOnCreation carActionOnCreation, IElectroCarActionOnCreation eCarActionOnCreation)
        {
            _fillCarTank = carActionOnCreation;
            _chargeCar = eCarActionOnCreation;
        }

        public Car CreateNewCar(int fuelTankVolume, double weight, int horsePower,
            EngineTypes engineType, string name, string param, Pilot pilot)
        {
            var car = new Car(name, null, weight, param, pilot, fuelTankVolume,
                CreateGasolineEngine(horsePower, engineType));

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

        public SportCar CreateNewSportCar(int fuelTankVolume, double weight, int horsePower,
            EngineTypes engineType, string name, Action<IParams> optionalParam, Pilot pilot)
        {
            var sportCar = new SportCar(name, null, weight, null, pilot, fuelTankVolume,
                CreateGasolineEngine(horsePower, engineType));
            OnCarCreation(sportCar);
            return sportCar;
        }

        private GasolineEngine CreateGasolineEngine(int horsePowers, EngineTypes engineTypes)
        {
            var engine = new GasolineEngine(horsePowers, engineTypes);
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

        public ElectroCar CreateNewElectroCar(string name, double weight, int hpValue, Pilot pilot)
        {
            var electro = new ElectroCar(name, null, CreateElectroEngine(hpValue), weight, null, pilot, 0);
            ChargeCar(electro);
            return electro;
        }

        private void ChargeCar(ElectroCar electroCar)
        {
            _chargeCar.ChargeCar(electroCar);
        }

        #region Nested type: VehicleParams

        public class VehicleParams : IParams
        {
            private string _params;

            #region IParams Members

            public IParams WithParams(Func<string> paramsDelegate)
            {
                _params = paramsDelegate();
                return this;
            }

            public string GetParams()
            {
                return _params;
            }

            #endregion
        }

        #endregion
    }
}