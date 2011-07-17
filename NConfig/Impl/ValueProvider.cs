using System;
using System.Collections.Generic;
using System.Linq;
using NConfig.Abstractions;
using NConfig.Model;
using NConfig.Filter;
using System.Collections;
using System.Reflection;

namespace NConfig.Impl
{
    public class ValueProvider : IValueProvider
    {
        public ValueProvider(Parameter target, IValueTranslator translator, IFilterPolicy policy)
        {
            this.Target = target;
            this.Translator = translator;
            this.Policy = policy;
        }

        private Parameter Target { get; set; }
        private IValueTranslator Translator { get; set; }
        private IFilterPolicy Policy { get; set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            var valuesByPolicy = this.Policy.Filter(runtimeContext, this.Target.Values).OfType<ParameterValue>().Select(v => v.Value);

            return this.TranslateRawValue(valuesByPolicy);
        }

        private object TranslateRawValue(IEnumerable<string> valuesByPolicy)
        {
            Type parameterType = Type.GetType(this.Target.TypeName, true);

            if (parameterType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(parameterType))
            {
                MethodInfo translateMethodInfo = this.Translator.GetType().GetMethod("Translate", new Type[1] { typeof(IEnumerable<string>) });

                return translateMethodInfo.Invoke(this.Translator, new object[1] { valuesByPolicy });
            }
            else
            {
                MethodInfo translateMethodInfo = this.Translator.GetType().GetMethod("Translate", new Type[1] { typeof(string) });

                return translateMethodInfo.Invoke(this.Translator, new object[1] { valuesByPolicy.Single() });
            }
        }
    }
}
