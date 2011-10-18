using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Windows.Forms;
using NConfig;
using NConfig.Extensions;
using NConfig.WCF;
using Server.Configuration;
using Server.WCF;

namespace Server
{
    public partial class Host : Form
    {
        public Host()
        {
            InitializeComponent();
        }

        public IConfigurationService ConfigService { get; set; }

        private void Host_Load(object sender, EventArgs e)
        {
            var configurationDataServiceHost = new ServiceHost(typeof(ConfigurationDataService));
            configurationDataServiceHost.Open();

            this.TraceHostOpen(configurationDataServiceHost);

            var servicesConfig = this.ConfigService.GetSection<ServicesConfig>();

            foreach (Type serviceType in servicesConfig.ServiceTypesToLoad)
            {
                var singleServiceConfig = this.ConfigService.WithServiceRef(serviceType).GetSection<SingleServiceConfig>();
                
                var host = new ConfigServiceHost(serviceType, this.ConfigService);
                host.Description.Behaviors.AddRange(singleServiceConfig.ServiceBehaviors);

                host.Open();

                this.TraceHostOpen(host);
            }
        }
        private void TraceHostOpen(ServiceHost host)
        {
            this.listBox1.Items.Add(host.Description.Endpoints.First().Address + "  " +
                    host.Description.Endpoints.First().Binding.GetType().FullName
                    );
        }
    }
}
