using System.Collections.Generic;
using Domain.CarTypes;
using Domain.Dto;

namespace Repository.Interfaces
{
    public interface ICarRepository : IRepository
    {
        IList<CarDetailsDto> GetCarDetails();
        IList<CarDetailsDto> GetCarDetailsWithPilot();
        IList<Car> GetAllCars();
        IList<SportCar> GetAllSportCars();
        CarDetailsDto GetCarDetails(long id);
        CarDetailsDto GetCarDetailsWithPilotbyCarId(long id);
    }
}