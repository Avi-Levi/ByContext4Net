using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Client.Views.Login;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Client.Views.Shell;
using ByContext;
using Common;
using Client.Views.Product;
using ByContext.WCF;

namespace Client
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

            Application.Run((Form)BuildContainer().Resolve<IShellView>());
        }

        private static IWindsorContainer BuildContainer()
        {
            WindsorContainer container = new WindsorContainer();
            container.Register(Component.For<ILoginView>().ImplementedBy<LoginView>());

            container.Register(Component.For<LoginServiceAdapter>());
            container.Register(Component.For<ShellController>().LifeStyle.Singleton);

            container.Register(Component.For<IShellView>().ImplementedBy<Shell>());
            container.Register(Component.For<IProductView>().ImplementedBy<ProductView>());

            container.Register(Component.For<ProductsServiceAdapter>());

            container.Register(Component.For<ProxyFactory>());
            container.Register(Component.For<LoggerFactory>());

            IByContext svc = BuildByContext();
            container.Register(Component.For<IByContext>().Instance(svc));

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            return container;
        }
        private static IByContext BuildByContext()
        {
            IByContext configSvc = Configure.With(cfg=>
                cfg.RuntimeContext(ctx =>
                {
                    ctx.Add(Subjects.Environment.Name, Subjects.Environment.Dev);
                    ctx.Add(Subjects.AppType.Name, Subjects.AppType.OnlineClient);
                    ctx.Add(Subjects.MachineName.Name, Subjects.MachineName.ClientMachine1);
                })
                .AddFromRemoteWCFService()
                );

            return configSvc;
        }
    }
}
