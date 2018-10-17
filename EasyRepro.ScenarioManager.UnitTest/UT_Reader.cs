using EasyRepro.ScenarioManager.Model;
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
            var result = reader.Parse<Scenario>(string.Empty);

            Assert.IsNull(result);
        }

        [TestCase]
        public void Parse_A_Valid_String()
        {
            var result = reader.Parse<Scenario>(@"{
  ""Name"": ""Create a new account record"",
  ""Description"": ""Create a new account record using EasyRepro"",
  ""Entity"": ""Account"",
  ""Area"": ""Sales"",
  ""SubArea"": ""Accounts"",
  ""View"": ""Active Accounts"",
  ""Commands"": [
    {
      ""Name"": ""New"",
      ""Description"": ""Click New button from the command bar and fill the data"",
      ""Data"": [
        {
          ""Attribute"": ""name"",
          ""Value"":  ""Test Account""
        }
      ]
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
            Assert.AreEqual("Test Account", result.Commands[0].Data[0].Value);
        }

        [TestCase(@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\TestData\SampleAccount.json")]
        public void Parse_From_A_File(string filePath)
        {
            var result = reader.ParseTestDataFromFile(filePath);

            Assert.IsNotNull(result);

            Assert.IsNotEmpty(result.Name);

            Assert.AreEqual("Create a new account record", result.Name);
            Assert.AreEqual("Test Account 12345", result.Commands[0].Data[0].Value);
            Assert.AreEqual(AttributeType.Text, result.Commands[0].Data[0].Type);
        }

        [TestCase(@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\TestData\SampleContact.json")]
        public void Parse_From_A_File_With_Composite_Key(string filePath)
        {
            var result = reader.ParseTestDataFromFile(filePath);

            Assert.IsNotNull(result);

            Assert.IsNotEmpty(result.Commands[0].Data[0].Fields[0].Value);

            Assert.AreEqual("Create a new contact record", result.Name);
            Assert.AreEqual("Test Demo Persona", result.Commands[0].Data[0].Fields[0].Value + " " + 
                result.Commands[0].Data[0].Fields[1].Value);
        }
    }
}
