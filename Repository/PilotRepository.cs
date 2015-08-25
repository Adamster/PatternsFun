using System;
using Domain;
using Domain.Persons;
using NHibernate;
using Repository.Interfaces;

namespace Repository
{
    public class PilotRepository : IPilotRepository
    {
        private readonly ISession _session = SessionGenerator.Instance.GetSession();

        public void AddPilot<TEntity>(TEntity entity) where TEntity : Entity
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("trying to add pilot in Database...");
                    _session.Save(entity);
                    Console.WriteLine("Succesfully!");
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tran.Rollback();
                }
            }
        }

        public void UpdatePilotAge(long PilotID, int newAge)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var pilot = _session.Load<Pilot>(PilotID);
                    pilot.Age = newAge;

                    Console.WriteLine("trying to update pilot in Database...");
                    _session.Update(pilot);
                    Console.WriteLine("Succesfully!");
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tran.Rollback();
                }
            }
        }

        public void DeletePilot(string name)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                  var pilot =  _session.Get<Pilot>(name);
                    Console.WriteLine("trying to delete pilot in Database...");
                    _session.Delete(pilot);
                    Console.WriteLine("Succesfully!");
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tran.Rollback();
                }
            }
        }

        public void DeletePilot<TEntity>(TEntity entity) where TEntity : Entity
        {
           
        }
    }
}