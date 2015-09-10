using System.Collections.Generic;
using Domain.Dto;

namespace Repository.Interfaces
{
    public interface ICarRepository : IRepository
    {
        IList<CarDetailsDto> GetCarDetails();
        IList<CarDetailsDto> GetCarDetailsWithPilot();
    }
}