using System.Collections.Generic;
using Domain.CarTypes;
using Domain.Dto;
using Domain.Persons;

namespace Repository.Interfaces
{
    public interface IPilotRepository : IRepository
    {
        Pilot GetPilot(long id);
        void AddPilot(Pilot pilot);
        void UpdatePilotAge(long pilotId, int newAge);
        void DeletePilot(long id);
        void AddCar(Pilot pilot, Car car);
        IList<Car> GetVehicleName();
        IList<object> GetCarCountPerPilotCrutchVersion();
        IList<PilotDetailsDto> GetCarCountPerPilot();
        IList<Pilot> GetAllPilots();
        IList<object> GetAvgHorsePowerPerPilot();
        Pilot GetOldestPilot();
        IList<object> GetMTeamDrivers();
        IList<object> GetCarClassifciationByHp();

        void UpdatePilot(Pilot oldPilot, PilotUpdateDto pilotUpdateDto);
    }
}