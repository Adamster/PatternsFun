using System;
using System.Collections.Generic;
using System.Linq;
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

        public IList<Pilot> GetUniquePilots()
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var res = _session.QueryOver<Pilot>()
                        .TransformUsing(Transformers.DistinctRootEntity)
                        .Future();


                    var resL = res.ToList();
                    tran.Commit();

                    return resL;
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

        public IList<object> GetAvgHorsePowerPerPilot()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Vehicle vAlias = null;
                Engine eAlias = null;

                try
                {
                    var pilotsWithAvgHp = _session.QueryOver(() => pAlias)
                        .JoinAlias(() => pAlias.CarVehicles, () => vAlias)
                        .SelectList(list => list
                            .SelectSubQuery(
                                QueryOver.Of<Car>()
                                    .JoinAlias(x => x.Engine, () => eAlias)
                                    .Where(x => x.OwnerPilot.Id == pAlias.Id)
                                    .SelectList(l => l.SelectAvg(() => eAlias.HorsePowers))
                            )
                            .Select(() => pAlias.Name)
                        )
                        .List<object>();

                    return pilotsWithAvgHp;
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

        public Pilot GetOldestPilot()
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    Pilot pAlias = null;
                    Pilot allPalias = null;

                    var allsubquery = QueryOver.Of<Pilot>()
                        .SelectList(list => list
                            .Select(x => x.Age));

                    var res = _session.QueryOver(() => pAlias)
                        .WithSubquery.WhereProperty(x => x.Age)
                        .GeAll(allsubquery)
                        .SingleOrDefault();

                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public IList<object> GetMTeamDrivers()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                try
                {
                    var mTeamSelect = QueryOver.Of<Pilot>()
                        .SelectList(list => list
                            .Select(x => x.Team))
                        .Where(x => x.Team.IsLike("M%"));


                    var res = _session.QueryOver(() => pAlias)
                        .SelectList(list => list
                            .Select(x => x.Name)
                            .Select(x => x.Team)
                        ).WithSubquery.WhereProperty(x => x.Team).In(mTeamSelect)
                        .List<object>();
                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }

        public IList<Car> GetCarClassifciationByHp()
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                   
                    Engine eAlias = null;


                    var res = _session.QueryOver(() => eAlias)
    
                        .Select(
                            Projections.Conditional(
                                Restrictions.Where<GasolineEngine>(x=>x.NumberOfCylinders > 2 ),
                                Projections.Constant("Simple Car", NHibernateUtil.String),
                                Projections.Constant("Fast Car", NHibernateUtil.String))).List<object>();
                    return null;
                    // return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return null;
                }
            }
        }
    }
}