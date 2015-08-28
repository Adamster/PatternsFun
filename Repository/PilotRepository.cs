using System;
using Domain;
using Domain.CarTypes;
using Domain.Persons;
using NHibernate;
using Repository.Interfaces;
using Utils;

namespace Repository
{
    public class PilotRepository : Repository, IPilotRepository
    {
        private readonly ISession _session = SessionGenerator.Instance.GetSession();

        public void AddPilot(Pilot pilot)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("trying to add pilot in Database...");
                    _session.Save(pilot);
                    tran.Commit();
                    Console.WriteLine("Succesfully!");
                    Logger.AddMsgToLog("Succesfully! commited a pilot in database");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
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
                    Console.WriteLine("Pilot cars count : {0} ", pilot.CarVehicles.Count);

                    foreach (var vehicle in pilot.CarVehicles)
                    {
                        Console.WriteLine(vehicle.Name);
                    }
                    Console.WriteLine("trying to update pilot in Database...");
                    _session.Update(pilot);
                    tran.Commit();
                    Console.WriteLine("Succesfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
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
                    tran.Commit();
                    Console.WriteLine("Succesfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
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
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    tran.Rollback();
                }
            }
        }

        public object GetPilotsCount()
        {
            return new object();
        }


        public void DeletePilot<TEntity>(TEntity entity) where TEntity : Entity
        {
        }
    }
}