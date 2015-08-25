using System;
using Domain;
using Domain.CarTypes;
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

        public void UpdatePilotAge(long pilotId, int newAge)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var pilot = _session.Load<Pilot>(pilotId);
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

        public void DeletePilot(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var pilot = _session.Get<Pilot>(id);
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

        public void AddCar(Pilot pilot, Car car)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    pilot = _session.Load<Pilot>(pilot.Id);

                    pilot.AddCar(car);
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