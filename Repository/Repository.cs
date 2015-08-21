// File: Repository.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

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
            _session.Save(entity);
        }

        #endregion
    }


    public class CarRepository : Repository, ICarRepository
    {
    }
}