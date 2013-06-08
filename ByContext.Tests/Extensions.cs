using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ByContext.Filters.Conditions.TextMatch;
using ByContext.Model;
using ByContext.Extensions;
using ByContext.Filters;
using ByContext.Filters.Conditions;

namespace ByContext.Tests
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

        public static ParameterValue WithTextMatchReference(this ParameterValue source, string subjectName, string subjectValue)
        {
            source.FilterConditions.Add(FilterCondition.Create(TextMatchCondition.Name,new Dictionary<string, string>
                                                                                           {
                                                                                               {"Subject", subjectName},
                                                                                               {"Value", subjectValue}
                                                                                           } ));
            return source;
        }
    }
}
