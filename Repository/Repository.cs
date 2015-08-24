using System;
using Domain;
using NHibernate;
using Repository.Interfaces;

namespace Repository
{
    public abstract class Repository : IRepository
    {
        #region Implementation of IRepository

        private readonly ISession _session = SessionGenerator.Instance.GetSession();

        public void Save<TEntity>(TEntity entity) where TEntity : Entity
        {
            Console.WriteLine("general repository used");
            _session.Save(entity);
        }

        #endregion
    }


    public class CarRepository : Repository, ICarRepository
    {
    }
}