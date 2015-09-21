using NHibernate;

namespace Repository
{
    public interface ISessionManager
    {
        ISession GetSession();
    }
}