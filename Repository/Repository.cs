using System;
using Domain;
using NHibernate;
using Repository.Interfaces;

namespace Repository
{
    public abstract class Repository : IRepository
    {
        private readonly ISession _session = SessionGenerator.Instance.GetSession();

        public void Save<TEntity>(TEntity entity) where TEntity : Entity
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("general repository used");
                    _session.Save(entity);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    tran.Rollback();
                }
            }
        }

        public void Update(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}