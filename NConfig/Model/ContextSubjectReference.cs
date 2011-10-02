using System.Runtime.Serialization;

namespace NConfig.Model
{
    [DataContract]
    public class ContextSubjectReference
    {
        public const string ALL = "ALL";
        private ContextSubjectReference(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public static ContextSubjectReference Create(string name, string value)
        {
            return new ContextSubjectReference(name, value);
        }

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Value { get; private set; }

        public override string ToString()
        {
            return this.Name + ":" + this.Value;
        }
    }
}
