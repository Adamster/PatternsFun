using System;
using System.IO;
using System.Text;
using Domain.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Utils;

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
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(builder => builder.Database("Garage")
                        .Server(@"MDDSK40101").TrustedConnection()))
                .Mappings(x => x.FluentMappings.AddFromAssembly(typeof(EntityMap<>).Assembly))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));

            


            var conf = configuration.BuildConfiguration();

            var stringBuilder = new StringBuilder();
            new SchemaExport(conf).Execute(entry => stringBuilder.Append(entry), false, false);

            using (
                var configFile = new FileStream(@"C:\Users\" + Environment.UserName + @"\Documents\config.sql",
                    FileMode.Create))
            {
                Logger.AddMsgToLog("saving new config sql");
                var buff = Encoding.Unicode.GetBytes(stringBuilder.ToString());
                configFile.Write(buff, 0, buff.Length);
            }

            return configuration.BuildSessionFactory();
        }
    }
}