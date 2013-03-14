using NConfig.Model;

namespace NConfig.Exceptions
{
    public class MemberNotFouldForParameter : NConfigException
    {
        public Parameter Parameter { get; private set; }

        public MemberNotFouldForParameter(Parameter parameter)
            : base(string.Format("A matching member was not found for parameter with name: {0}", parameter.Name))
        {
            Parameter = parameter;
        }
    }
}