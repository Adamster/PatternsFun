using Domain.Persons;

namespace Repository.Interfaces
{
    public interface IPilotRepository: IRepository
    {
        void AddPilot(Pilot pilot);
        void UpdatePilotAge(long pilotId, int newAge);
        void DeletePilot(long id);
        void AddCar(Pilot pilot, Domain.CarTypes.Car car);
    }
}