using Domain;

namespace Repository.Interfaces
{
    public interface IPilotRepository
    {
        void AddPilot<TEntity>(TEntity entity) where TEntity : Entity;
        void UpdatePilotAge(long id, int newAge);
        void DeletePilot(string name);

    }
}