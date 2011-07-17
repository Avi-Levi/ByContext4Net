using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Tests
{
    public class TestSection
    {
        public int Num { get; set; }
        public IList<int> Numbers { get; set; }
        public IEnumerable<int> EnumerableNumbers { get; set; }
        public IDictionary<int, string> Dictionary { get; set; }
        public string Name { get; set; }
        public TestEnum EnumValue { get; set; }
        public IService SVC { get; set; }
        public IEnumerable<IService> SVCs { get; set; }
    }

    public interface IService
    {
        int Get();
    }
    class ServiceImpl : IService
    {
        public ServiceImpl(int valueToReturn)
        {
            this._valueToReturn = valueToReturn;
        }
        int _valueToReturn;
        public int Get()
        {
            return this._valueToReturn;
        }
    }
}
