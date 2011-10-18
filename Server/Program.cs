using System;
using System.Windows.Forms;
using NConfig;
using Common;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using NConfig.XML;
using Server.Services;
using Server.Data;
using System.ServiceModel.Description;
using Server.WCF;

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

        private static IConfigurationService BuildConfigService(WindsorContainer container)
        {
            IConfigurationService configSvc = Configure.With(cfg=>
                cfg.RuntimeContext(ctx =>
                {
                    ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                    ctx.Add(Subjects.AppType.Name, Subjects.AppType.ApplicationServer);
                    ctx.Add(Subjects.MachineName.Name, Environment.MachineName);
                })
                .AddWindsorTranslatorProvider(container)
                .AddFromXmlFile("Configuration.xml")
                );
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
            container.Register(Component.For<IServiceBehavior>().ImplementedBy<DI_InstanceProviderExtension>().Named("DI"));

            IConfigurationService configSvc = BuildConfigService(container);
            container.Register(Component.For<IConfigurationService>().Instance(configSvc));

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            return container;
        }
    }
}
