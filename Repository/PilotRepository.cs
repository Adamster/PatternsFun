using System;
using System.Collections.Generic;
using Domain;
using Domain.CarTypes;
using Domain.Dto;
using Domain.EnginesTypes;
using Domain.Persons;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
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

        public IList<Car> GetVehicleName()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pilotAlias = null;
                Vehicle vehicleAlias = null;
                Car carAlias = null;
                GasolineEngine geAlias = null;
                try
                {
                    var res = _session.QueryOver(() => carAlias)
                        .JoinAlias(() => carAlias.Engine, () => geAlias, JoinType.InnerJoin)
                        .Where(() => geAlias.HorsePowers < 500)
                        .List();

                    tran.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    tran.Rollback();
                    return null;
                }
            }
        }

        public IList<object> GetCarCountPerPilotCrutchVersion()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pilotAlias = null;
                Vehicle vehicleAlias = null;
                try
                {
                    var res = _session.QueryOver(() => vehicleAlias)
                        .JoinAlias(() => vehicleAlias.OwnerPilot, () => pilotAlias)
                        .SelectList(list => list
                            .SelectGroup(() => pilotAlias.Name)
                            .SelectCount(() => pilotAlias.Id)
                            )
                        .Where(Restrictions.Gt(Projections.Count(Projections.Property(() => pilotAlias.Id)), 1))
                        .List<object>();


                    tran.Commit();
                    return res;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    tran.Rollback();
                    return null;
                }
            }
        }

        public IList<PilotDetailsDto> GetCarCountPerPilot()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pilotAlias = null;
                Vehicle vehicleAlias = null;
                PilotDetailsDto pilotDetailsDtoAlias = null;

                try
                {
                    var res = _session.QueryOver(() => vehicleAlias)
                        .JoinAlias(() => vehicleAlias.OwnerPilot, () => pilotAlias)
                        .SelectList(list => list
                            .SelectGroup(() => pilotAlias.Name).WithAlias(() => pilotDetailsDtoAlias.Name)
                            .SelectGroup(() => pilotAlias.Team).WithAlias(() => pilotDetailsDtoAlias.Team)
                            .SelectCount(() => pilotAlias.Id).WithAlias(() => pilotDetailsDtoAlias.CarCount)
                        )
                        .Where(Restrictions.Gt(Projections.Count(Projections.Property(() => pilotAlias.Id)), 1))
                        .TransformUsing(Transformers.AliasToBean<PilotDetailsDto>())
                        .List<PilotDetailsDto>();
                    return res;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    tran.Rollback();
                    return null;
                }
            }
        }
    }
}