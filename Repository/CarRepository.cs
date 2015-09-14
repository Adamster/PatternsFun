using System;
using System.Collections.Generic;
using System.Linq;
using Domain.CarTypes;
using Domain.Dto;
using Domain.EnginesTypes;
using Domain.Persons;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using NHibernate.Util;
using Repository.Interfaces;
using Utils;

namespace Repository
{
    public class CarRepository : Repository, ICarRepository
    {
        private readonly ISession _session = SessionGenerator.Instance.GetSession();

        public IList<CarDetailsDto> GetCarDetails()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => cAlias)
                        .JoinAlias(() => cAlias.Engine, () => geAlias)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => geAlias.HorsePowers).WithAlias(() => cddtoAlias.HorsePowers)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                        )
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

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


        public CarDetailsDto GetCarDetails(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => cAlias)
                        .JoinAlias(() => cAlias.Engine, () => geAlias)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => geAlias.HorsePowers).WithAlias(() => cddtoAlias.HorsePowers)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)

                        )
                        .Where(() => cAlias.Id == id)
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();

                   
                    
                    tran.Commit();
                 
                    return res.First();
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


        public IList<CarDetailsDto> GetCarDetailsWithPilot()
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => pAlias)
                        .JoinAlias(() => pAlias.CarVehicles, () => cAlias, JoinType.RightOuterJoin)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                            .Select(Projections.SqlFunction("coalesce", NHibernateUtil.String,
                                Projections.Property<Pilot>(p => p.Name),
                                Projections.Constant("No pilot in this Vehicle", NHibernateUtil.String)))
                            .WithAlias(() => cddtoAlias.PilotName)
                        )
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

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

        public CarDetailsDto GetCarDetailsWithPilotbyCarId(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                Pilot pAlias = null;
                Car cAlias = null;
                GasolineEngine geAlias = null;
                CarDetailsDto cddtoAlias = null;
                try
                {
                    var res = _session.QueryOver(() => pAlias)
                        .JoinAlias(() => pAlias.CarVehicles, () => cAlias, JoinType.RightOuterJoin)
                        .SelectList(list => list
                            .Select(() => cAlias.Name).WithAlias(() => cddtoAlias.Name)
                            .Select(() => cAlias.Weight).WithAlias(() => cddtoAlias.Weight)
                            .Select(() => cAlias.FuelTank).WithAlias(() => cddtoAlias.TankVolume)
                            .Select(Projections.SqlFunction("coalesce", NHibernateUtil.String,
                                Projections.Property<Pilot>(p => p.Name),
                                Projections.Constant("No pilot in this Vehicle", NHibernateUtil.String)))
                            .WithAlias(() => cddtoAlias.PilotName)
                            
                            
                        )
                        .Where(()=> cAlias.Id== id)
                        .TransformUsing(Transformers.AliasToBean<CarDetailsDto>())
                        .List<CarDetailsDto>();


                    tran.Commit();

                    return res.First();
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



        public IList<Car> GetAllCars()
        {
            using (var tran = _session.BeginTransaction())
            {

                try
                {
                    var res = _session.QueryOver<Car>()
                  .List();

                    tran.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return new List<Car>();
                }
              
               
            }
        }

        public IList<SportCar> GetAllSportCars()
        {
            using (var tran = _session.BeginTransaction())
            {

                try
                {
                    var res = _session.QueryOver<SportCar>()
                  .List();

                    tran.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    Logger.AddMsgToLog(ex.Message + "\n" + ex.StackTrace);
                    return new List<SportCar>();
                }


            }
        }
        
    }
}