using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NConfig.Filters;
using NConfig.Filters.Rules;
using NConfig.Model;
using NConfig.Testing;
using NConfig.Tests.Helpers;
using NUnit.Framework;

namespace NConfig.Tests.Filter
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ParameterValuesTypeAttribute : Attribute
    {
        public ParameterValuesTypeAttribute(Type containerClassType)
        {
            this.ContainerClassType = containerClassType;
        }

        public Type ContainerClassType { get; set; }
    }

    [TestFixture]
    public class FilterPolicySingleValueTest
    {
        private IEnumerable<ParameterValue> FilterItems()
        {
            MethodBase callingMethod = new StackFrame(1).GetMethod();

            IDictionary<string, string> runtimeContext = TestlHelper.ExtractRuntimeContextFromMethod(callingMethod);
            IFilterPolicy policy = Helper.ExtractFilterPolicyFromMethod(callingMethod);

            return policy.Filter(runtimeContext, Helper.ExtractValuesFromMethod(callingMethod)).OfType<ParameterValue>();
        }

        [SetUp]
        public void Init()
        {
            this.InitLoggingFolderParameterValues();
            this.InitInterceptorTypeNamesParameterValues();
        }

        private void InitInterceptorTypeNamesParameterValues()
        {
            // allways selected.
            InterceptorTypeNamesParameterValues.ExceptionHandlingInterceptor
                 .WithAllReferenceToSubject(servicesSubject.Name)
                 .WithAllReferenceToSubject(environmentSubject.Name)
                 .WithAllReferenceToSubject(appTypeSubject.Name)
                 .WithAllReferenceToSubject(methodNameSubject.Name)
                 ;

            // selected only for dev environment.
            InterceptorTypeNamesParameterValues.TracingInterceptor
                .WithAllReferenceToSubject(servicesSubject.Name)
                .WithAllReferenceToSubject(appTypeSubject.Name)
                .WithAllReferenceToSubject(methodNameSubject.Name)
                .WithReference(environmentSubject.Name, environmentSubject.dev)
                ;

            // selected only for UpdateCustomer method.
             InterceptorTypeNamesParameterValues.TransactionInterceptor
                .WithAllReferenceToSubject(environmentSubject.Name)
                .WithAllReferenceToSubject(appTypeSubject.Name)
                .WithAllReferenceToSubject(servicesSubject.Name)
                .WithReference(methodNameSubject.Name, methodNameSubject.UpdateCustomer)
                ;
        }

        private void InitLoggingFolderParameterValues()
        {
            LoggingFolderPathParameterValues.ReletiveOneLevelDownPath
                .WithAllReferenceToSubject(environmentSubject.Name)
                .WithAllReferenceToSubject(appTypeSubject.Name)
                .WithAllReferenceToSubject(servicesSubject.Name);

            LoggingFolderPathParameterValues.ApplicationServerPath
                .WithReference(environmentSubject.Name, environmentSubject.prod)
                .WithReference(appTypeSubject.Name, appTypeSubject.applicationServer)
                .WithAllReferenceToSubject(servicesSubject.Name);

            LoggingFolderPathParameterValues.ProdOnlineClient
                .WithReference(environmentSubject.Name, environmentSubject.prod)
                .WithReference(appTypeSubject.Name, appTypeSubject.onlineClient);

            LoggingFolderPathParameterValues.AuditServicePath
                .WithReference(appTypeSubject.Name, appTypeSubject.applicationServer)
                .WithReference(environmentSubject.Name, environmentSubject.prod)
                .WithReference(servicesSubject.Name, servicesSubject.AuditService);
        }

        #region LoggingFolderPath
        /// <summary>
        /// on dev environment, we use self hosting and want the log folder to be inside our bin folder.
        /// .\logs.
        /// </summary>
        [Test]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.dev)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.onlineClient)]

        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule), typeof(BestMatchRule))]
        [ParameterValuesType(typeof(LoggingFolderPathParameterValues))]
        public void Dev_online_client()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(LoggingFolderPathParameterValues.ReletiveOneLevelDownPath));
        }

        /// <summary>
        /// on dev environmentt, we use self hosting and want the log folder to be inside our bin folder.
        /// .\logs.
        /// </summary>
        [Test]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.dev)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.SomeService)]

        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule), typeof(BestMatchRule))]
        [ParameterValuesType(typeof(LoggingFolderPathParameterValues))]
        public void dev_online_server()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(LoggingFolderPathParameterValues.ReletiveOneLevelDownPath));
        }

        /// <summary>
        /// usually on production application server, we deploy our application as a web\wcf service.
        /// it's hard to get a correct path that is relative to the executable (w3wp), so we 
        /// want to specify an absolute path.
        /// </summary>
        [Test]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.SomeService)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.prod)]

        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule), typeof(BestMatchRule))]
        [ParameterValuesType(typeof(LoggingFolderPathParameterValues))]
        public void prod_online_server()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(LoggingFolderPathParameterValues.ApplicationServerPath));
        }

        /// <summary>
        /// on production client machine, 
        /// the relative path might not work for client machines with restrictive security, 
        /// as there may be a specific path with write permissions.
        /// so we need something like this "c:\userData\appName\logs"
        /// </summary>
        [Test]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.onlineClient)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.prod)]

        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule), typeof(BestMatchRule))]
        [ParameterValuesType(typeof(LoggingFolderPathParameterValues))]
        public void prod_online_client()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(LoggingFolderPathParameterValues.ProdOnlineClient));
        }

        /// <summary>
        /// sometimes we want a specific service to log to a different path, 
        /// for example: an audit service that needs to log to a certaine network path, were 
        /// a legacy application loads the logs to be analized.
        /// </summary>
        [Test]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.AuditService)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.prod)]

        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule), typeof(BestMatchRule))]
        [ParameterValuesType(typeof(LoggingFolderPathParameterValues))]
        public void prod_onlineServer_auditService()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(LoggingFolderPathParameterValues.AuditServicePath));
        }

        #endregion LoggingFolderPath

        [Test]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.AuditService)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.dev)]
        [RuntimeContextItemAttribute(methodNameSubject.Name, methodNameSubject.UpdateCustomer)]
        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule))]
        [ParameterValuesType(typeof(InterceptorTypeNamesParameterValues))]

        public void auditService_appServer_Dev_updateCustomer()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.ExceptionHandlingInterceptor));
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.TracingInterceptor));
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.TransactionInterceptor));
        }

        [Test]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.AuditService)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.dev)]
        [RuntimeContextItemAttribute(methodNameSubject.Name, methodNameSubject.GetAllCustomers)]
        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule))]
        [ParameterValuesType(typeof(InterceptorTypeNamesParameterValues))]

        public void auditService_appServer_Dev_GetAllCustomers()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.ExceptionHandlingInterceptor));
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.TracingInterceptor));
        }

        [Test]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.AuditService)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.prod)]
        [RuntimeContextItemAttribute(methodNameSubject.Name, methodNameSubject.UpdateCustomer)]
        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule))]
        [ParameterValuesType(typeof(InterceptorTypeNamesParameterValues))]

        public void auditService_appServer_prod_UpdateCustomer()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.ExceptionHandlingInterceptor));
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.TransactionInterceptor));
        }

        [Test]
        [RuntimeContextItemAttribute(servicesSubject.Name, servicesSubject.AuditService)]
        [RuntimeContextItemAttribute(appTypeSubject.Name, appTypeSubject.applicationServer)]
        [RuntimeContextItemAttribute(environmentSubject.Name, environmentSubject.prod)]
        [RuntimeContextItemAttribute(methodNameSubject.Name, methodNameSubject.GetAllCustomers)]
        [FilterPolicyAttribute(typeof(WithSpecificOrALLRerefenceToSubjectRule))]
        [ParameterValuesType(typeof(InterceptorTypeNamesParameterValues))]

        public void auditService_appServer_prod_GetAllCustomers()
        {
            IEnumerable<ParameterValue> result = this.FilterItems();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(InterceptorTypeNamesParameterValues.ExceptionHandlingInterceptor));
        }
    }
}
