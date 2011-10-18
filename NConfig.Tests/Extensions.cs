using System;
using System.Linq.Expressions;
using NConfig.Extensions;
using NConfig.Model;

namespace NConfig.Tests
{
    public static class Extensions
    {
        public static Section FromType<TSection>(this Section source) where TSection : class
        {
            source.TypeName = typeof (TSection).AssemblyQualifiedName;
            return source;
        }

        public static Section AddParameter(this Section source, Parameter parameter)
        {
            source.Parameters.Add(parameter.Name,parameter);
            return source;
        }

        public static Parameter WithTranslator(this Parameter source, string translatorName)
        {
            source.Translator = translatorName;
            return source;
        }

        public static Parameter FromExpression<TSection, TProperty>(this Parameter source, Expression<Func<TSection, TProperty>> selector)
            where TSection : class
        {
            var pi = selector.ToPropertyInfo<TSection, TProperty>();
            source.Name = pi.Name;
            source.TypeName = pi.PropertyType.AssemblyQualifiedName;

            return source;
        }

        public static Parameter AddValue(this Parameter source, ParameterValue value)
        {
            source.Values.Add(value);
            return source;
        }

        public static ParameterValue WithReference(this ParameterValue source, string subjectName, string subjectValue)
        {
            source.References.Add(ContextSubjectReference.Create(subjectName, subjectValue));
            return source;
        }
    }
}
