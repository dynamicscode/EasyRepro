using NUnit.Framework;

namespace EasyRepro.ScenarioManager.UnitTest
{
    [TestFixture]
    public class UT_Reader
    {
        Reader reader;

        [SetUp]
        public void Init()
        {
            reader = new Reader();
        }

        [TestCase]
        public void Parse_An_Empty_String()
        {
            var result = reader.Parse(string.Empty);

            Assert.IsNull(result);
        }

        [TestCase]
        public void Parse_A_Valid_String()
        {
            var result = reader.Parse(@"{
  ""Name"": ""Create a new account record"",
  ""Description"": ""Create a new account record using EasyRepro"",
  ""Area"": ""Sales"",
  ""SubArea"": ""Accounts"",
  ""View"": ""Active Accounts"",
  ""Commands"": [
    {
      ""Name"": ""New"",
      ""Description"": ""Click New button from the command bar and fill the data"",
      ""Data"": ""test data""
    },
    {
      ""Name"": ""Save & Close"",
      ""Description"": ""Click Save & Close from the command bar"",
      ""Data"": """"
    }
  ]
}");

            Assert.IsNotNull(result);

            Assert.IsNotEmpty(result.Name);

            Assert.AreEqual("Create a new account record", result.Name);
        }

        [TestCase(@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\TestData\Sample.json")]
        public void Parse_From_A_File(string filePath)
        {
            var result = reader.ParseFromFile(filePath);

            Assert.IsNotNull(result);

            Assert.IsNotEmpty(result.Name);

            Assert.AreEqual("Create a new account record", result.Name);
        }
    }
}
