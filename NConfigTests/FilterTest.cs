using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Diagnostics;
using NConfig;
using NConfig.Model;
using NConfig.Policy;
using NConfig.Rules;

namespace TestProject1
{
    [TestClass]
    public class FilterTest
    {
        private IEnumerable<ParameterValue> FilterItems()
        {
            Dictionary<string, string> runtimeContext = new Dictionary<string, string>();
            var attributes = new StackFrame(1).GetMethod().GetCustomAttributes(typeof(RuntimeContextitemAttribute), false).OfType<RuntimeContextitemAttribute>();
            foreach (var item in attributes)
            {
                runtimeContext.Add(item.Name, item.Value);
            }

            var policyAttributeAttribute = new StackFrame(1).
                GetMethod().GetCustomAttributes(typeof(FilterPolicyAttribute), false).OfType<FilterPolicyAttribute>().Single();

            var policy = (IFilterPolicy)Activator.CreateInstance(policyAttributeAttribute.PolicyType);

            
            IEnumerable<ParameterValue> result = policy.Apply(Items.All, runtimeContext);
            return result.OfType<ParameterValue>();
        }

        [TestInitialize]
        public void Init()
        {
            Items.ReletiveOneLevelDownPath.
                References.Add(new ContextSubjectReference { SubjectName = env.Name });
            Items.ReletiveOneLevelDownPath.
                References.Add(new ContextSubjectReference { SubjectName = appType.Name });
            Items.ReletiveOneLevelDownPath.
                References.Add(new ContextSubjectReference { SubjectName = services.name });

            Items.applicationServerPath.
                References.Add(new ContextSubjectReference { SubjectName = env.Name, SubjectValue = env.prod });
            Items.applicationServerPath.
                References.Add(new ContextSubjectReference { SubjectName = appType.Name, SubjectValue = appType.applicationServer });
            Items.applicationServerPath.
                References.Add(new ContextSubjectReference { SubjectName = services.name });

            Items.prodOnlineClient.
                References.Add(new ContextSubjectReference { SubjectName = env.Name, SubjectValue = env.prod });
            Items.prodOnlineClient.
                References.Add(new ContextSubjectReference { SubjectName = appType.Name, SubjectValue = appType.onlineClient});

            Items.AuditServicePath.
                References.Add(new ContextSubjectReference { SubjectName = appType.Name, SubjectValue=appType.applicationServer});
            Items.AuditServicePath.
                References.Add(new ContextSubjectReference { SubjectName = env.Name, SubjectValue = env.prod });
            Items.AuditServicePath.
                References.Add(new ContextSubjectReference { SubjectName = services.name, SubjectValue = services.AuditService });
        }

        /// <summary>
        /// on dev environment, we use self hosting and want the log folder to be inside our bin folder.
        /// .\logs.
        /// </summary>
        [TestMethod]
        [RuntimeContextitemAttribute(env.Name, env.dev)]
        [RuntimeContextitemAttribute(appType.Name, appType.onlineClient)]

        [FilterPolicyAttribute(typeof(OnlyBestMatchPolicy))]
        public void Dev_online_client()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(x => x.Value == Items.ReletiveOneLevelDownPath.Value));
        }

        /// <summary>
        /// on dev environmentt, we use self hosting and want the log folder to be inside our bin folder.
        /// .\logs.
        /// </summary>
        [TestMethod]
        [RuntimeContextitemAttribute(env.Name, env.dev)]
        [RuntimeContextitemAttribute(appType.Name, appType.applicationServer)]
        [RuntimeContextitemAttribute(services.name, services.SomeService)]

        [FilterPolicyAttribute(typeof(OnlyBestMatchPolicy))]
        public void dev_online_server()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(x => x.Value == Items.ReletiveOneLevelDownPath.Value));
        }

        /// <summary>
        /// usually on production application server, we deploy our application as a web\wcf service.
        /// it's hard to get a correct path that is relative to the executable (w3wp), so we 
        /// want to specify an absolute path.
        /// </summary>
        [TestMethod]
        [RuntimeContextitemAttribute(services.name, services.SomeService)]
        [RuntimeContextitemAttribute(appType.Name, appType.applicationServer)]
        [RuntimeContextitemAttribute(env.Name, env.prod)]

        [FilterPolicyAttribute(typeof(OnlyBestMatchPolicy))]
        public void prod_online_server()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(x => x.Value == Items.applicationServerPath.Value));
        }

        /// <summary>
        /// on production client machine, 
        /// the relative path might not work for client machines with restrictive security, 
        /// as there may be a specific path with write permissions.
        /// so we need something like this "c:\userData\appName\logs"
        /// </summary>
        [TestMethod]
        [RuntimeContextitemAttribute(appType.Name, appType.onlineClient)]
        [RuntimeContextitemAttribute(env.Name, env.prod)]

        [FilterPolicyAttribute(typeof(OnlyBestMatchPolicy))]
        public void prod_online_client()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(x => x.Value == Items.prodOnlineClient.Value));
        }

        /// <summary>
        /// sometimes we want a specific service to log to a different path, 
        /// for example: an audit service that needs to log to a certaine network path, were 
        /// a legacy application loads the logs to be analized.
        /// </summary>
        [TestMethod]
        [RuntimeContextitemAttribute(services.name, services.AuditService)]
        [RuntimeContextitemAttribute(appType.Name, appType.applicationServer)]
        [RuntimeContextitemAttribute(env.Name, env.prod)]

        [FilterPolicyAttribute(typeof(OnlyBestMatchPolicy))]
        public void prod_onlineServer_auditService()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(x => x.Value == Items.AuditServicePath.Value));
        }
    }
}
