using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NConfig;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Castle.Windsor;
using NConfig.WCF;

namespace Server
{
    public partial class Host : Form
    {
        public Host()
        {
            InitializeComponent();
        }

        public IWindsorContainer Windsor { get; set; }
        private void Host_Load(object sender, EventArgs e)
        {
            ServiceHost configurationDataServiceHost = new ServiceHost(typeof(ConfigurationDataService));
            configurationDataServiceHost.Open();

            this.TraceHostOpen(configurationDataServiceHost);

            IInstanceProvider instanceProvider = new DI_InstanceProvider(this.Windsor);

            var configService = this.Windsor.Resolve<IConfigurationService>();
            ServicesConfig config = configService.GetSection<ServicesConfig>();

            foreach (Type serviceType in config.ServiceTypesToLoad)
            {
                ServiceHostFactory factory = new ServiceHostFactory();
                ServiceHost host = new ConfigServiceHost(serviceType, configService);
                host.Description.Behaviors.Add(new InstanceProviderServiceBehavior(instanceProvider));

                host.Open();

                this.TraceHostOpen(host);
            }
        }
        private void TraceHostOpen(ServiceHost host)
        {
            this.listBox1.Items.Add(host.Description.Endpoints.First().Address + "  " +
                    host.Description.Endpoints.First().Binding.GetType().FullName);
        }
    }
}
