using ByContext.Model;

namespace ByContext.Exceptions
{
    public class MemberNotFoundForParameter : ByContextException
    {
        public Parameter Parameter { get; private set; }

        public MemberNotFoundForParameter(Parameter parameter)
            : base(string.Format("A matching member was not found for parameter with name: {0}", parameter.Name))
        {
            Parameter = parameter;
        }
    }
}