using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NConfig;
using NConfig.Model;
using Common;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Server.Services;
using Server.Data;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IWindsorContainer container = BuildContainer();

            Application.Run(container.Resolve<Host>());
        }

        private static IConfigurationService BuildConfigService()
        {
            IConfigurationService configSvc = Configure.With()
                .RuntimeContext(ctx =>
                {
                    ctx.Add(ConfigConstants.Subjects.Environment.Name, ConfigConstants.Subjects.Environment.Dev);
                    ctx.Add(ConfigConstants.Subjects.AppType.Name, ConfigConstants.Subjects.AppType.ApplicationServer);
                    ctx.Add(ConfigConstants.Subjects.MachineName.Name, Environment.MachineName);
                })
                .AddFromXml("Configuration.xml")
                .Build();
            ;
            return configSvc;
        }

        private static IWindsorContainer BuildContainer()
        {
            WindsorContainer container = new WindsorContainer();

            container.Register(Component.For<Host>());

            container.Register(Component.For<LoginService>());
            container.Register(Component.For<ProductsService>());

            container.Register(Component.For<LoggerFactory>());
            container.Register(Component.For<UsersDal>());
            container.Register(Component.For<ProductsDAL>());

            IConfigurationService configSvc = BuildConfigService();
            container.Register(Component.For<IConfigurationService>().Instance(configSvc));

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            return container;
        }
    }
}
