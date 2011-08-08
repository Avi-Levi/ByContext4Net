using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Configuration;

namespace NConfig.Tests.Helpers
{
    public static class InterceptorTypeNamesParameterValues
    {
        /// <summary>
        /// At dev env we want to trace method calls, input\output data etc...
        /// </summary>
        public static ParameterValue TracingInterceptor = ParameterValue.Create("namespace.TracingInterceptor,assembly");

        /// <summary>
        /// We want to handle transactions for some methods.
        /// </summary>
        public static ParameterValue TransactionInterceptor = ParameterValue.Create("namespace.TransactionInterceptor,assembly");

        /// <summary>
        /// we want to handle errors for all service methods.
        /// </summary>
        public static ParameterValue ExceptionHandlingInterceptor = ParameterValue.Create("namespace.ExceptionHandlingInterceptor,assembly");
    }
}
