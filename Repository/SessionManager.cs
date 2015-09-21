using NHibernate;

namespace Repository
{
    public class SessionManager : ISessionManager
    {
        private readonly ISessionFactory _sessionFactory = SessionGenerator.Instance.GetSessionFactory();
        private readonly ISession _session;

        public SessionManager()
        {
            _session = _sessionFactory.OpenSession();
        }

        public ISession GetSession()
        {
            return _session;
        }
    }
}