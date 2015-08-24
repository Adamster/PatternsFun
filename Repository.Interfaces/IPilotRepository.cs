using Domain;

namespace Repository.Interfaces
{
    public interface IPilotRepository
    {
        void AddPilot<TEntity>(TEntity entity) where TEntity : Entity;
        void UpdatePilot<TEntity>(TEntity entity) where TEntity : Entity;
        void DeletePilot<TEntity>(TEntity entity) where TEntity : Entity;

    }
}