using Domain.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Repository
{
    public class SessionGenerator
    {
        private static readonly ISessionFactory SessionFactory = CreateSessionFactory();
        private static readonly SessionGenerator _sessionGenerator = new SessionGenerator();

        private SessionGenerator()
        {
        }

        public static SessionGenerator Instance
        {
            get { return _sessionGenerator; }
        }

        public ISession GetSession()
        {
            NHibernateProfiler.Initialize();
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(builder => builder.Database("Garage")
                        .Server(@"MDDSK40101").TrustedConnection()))
                .Mappings(x => x.FluentMappings.AddFromAssembly(typeof (EntityMap<>).Assembly))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));


            return configuration.BuildSessionFactory();
        }
    }
}