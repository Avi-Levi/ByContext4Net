using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using NConfig;
using NConfig.Testing;

namespace NConfig
{
    public static class TestHelperExtensions
    {
        public static Configure ContextFromCallingMethod(this Configure source)
        {
            MethodBase callingMethod = new StackFrame(1).GetMethod();

            source.RuntimeContext(()=>TestlHelper.ExtractRuntimeContextFromMethod(callingMethod));
            
            return source;
        }
    }
}
