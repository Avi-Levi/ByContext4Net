using NUnit.Framework;

namespace NConfig.JsonTranslators.Tests
{
    [TestFixture]
    public class JsonTranslatorTests
    {
        [Test]
        public void ConvertJson()
        {
            // Arrange
            const string json = "{ \"Name\": \"Test\", \"Valid\": true }";

            // Act
            var translator = new JsonTranslator(typeof (ComplexType));
            var result = translator.Translate(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ComplexType>(result);
        }

        [Test]
        public void ConvertJsonWithValidProperties()
        {
            // Arrange
            const string json = "{ \"Name\": \"Test\", \"Valid\": true }";

            // Act
            var translator = new JsonTranslator(typeof(ComplexType));
            dynamic result = translator.Translate(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Name);
            Assert.AreEqual(true, result.Valid);
        }
    }

    public class ComplexType
    {
        public string Name { get; set; }
        public bool Valid { get; set; }
    }
}
