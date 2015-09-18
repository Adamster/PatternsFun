using System;
using Domain;
using NHibernate;
using Repository.Interfaces;
using Utils;

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
                    Logger.AddMsgToLog("save by general repository commited succesfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " \n" + e.StackTrace);
                    tran.Rollback();
                }
            }
        }

        public void SaveUpdate<TEntity>(TEntity entity) where TEntity : Entity
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);

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

        public void Delete(long id)
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var entity = _session.Get<Entity>(id);
                    Console.WriteLine("trying to delete Entity in Database...");
                    _session.Delete(entity);
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

        public TEntity GetEntityById<TEntity>(long id) where TEntity : Entity
        {
            using (var tran = _session.BeginTransaction())
            {
                try
                {
                    var res = _session.Get<TEntity>(id);
                    tran.Commit();
                    return res;
                }
                catch (Exception e)
                {
                    Logger.AddMsgToLog(e.Message + " \n" + e.StackTrace);
                    Console.WriteLine(e.Message + " \n" + e.StackTrace);
                    tran.Rollback();
                    return null;
                }
            }
        }
    }
}