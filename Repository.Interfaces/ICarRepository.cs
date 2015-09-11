using System.Collections.Generic;
using Domain.Dto;
using Domain.CarTypes;

namespace Repository.Interfaces
{
    public interface ICarRepository : IRepository
    {
        IList<CarDetailsDto> GetCarDetails();
        IList<CarDetailsDto> GetCarDetailsWithPilot();
        IList<Car> GetAllCars();
        IList<SportCar> GetAllSportCars();
        IList<CarDetailsDto> GetCarDetails(long id);

    }
}