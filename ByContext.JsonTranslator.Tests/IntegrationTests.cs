using System.Collections.Generic;
using ByContext.Model;
using NUnit.Framework;

namespace ByContext.JsonTranslator.Tests
{
    [TestFixture]
     public class IntegrationTests
     {
         [Test]
         public void TestJsonProperty()
         {
             // Arrange
             var section = new Section
             {
                 TypeName = typeof (SimpleSection).AssemblyQualifiedName
             };
             section.Parameters.Add("Complex", new Parameter
             {
                 Name = "Complex",
                 Translator = "Json",
                 TypeName = typeof(ComplexProperty).AssemblyQualifiedName,
                 Values = new List<ParameterValue>
                 {
                     new ParameterValue
                     {
                         Value = "{ \"Name\": \"Test\", \"Valid\": true }"
                     }
                 }
             });
 
             // Act
             var service = Configure.With(settings => settings.AddJsonTranslator().AddSection(section));
             var result = service.GetSection<SimpleSection>();
 
             // Assert
             Assert.IsNotNull(result);
             Assert.IsNotNull(result.Complex);
             Assert.AreEqual("Test", result.Complex.Name);
         }
     }
 
     public class SimpleSection
     {
         public ComplexProperty Complex { get; set; }
     }
 
     public class ComplexProperty
     {
         public string Name { get; set; }
         public bool Valid { get; set; }
     }
}