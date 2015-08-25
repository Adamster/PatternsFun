using System;
using Domain.Persons;
using NHibernate;
using Repository.Interfaces;

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
                    foreach (var vehicle in pilot.CarVehicles)
                    {
                        Console.WriteLine(vehicle.Name);
                    }
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
    }
}