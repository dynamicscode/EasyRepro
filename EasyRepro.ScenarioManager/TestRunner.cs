using NUnit.Framework;
using System.Configuration;

namespace EasyRepro.ScenarioManager
{
    [TestFixture]
    public class TestRunner
    {
        string configPath;

        [SetUp]
        public void Init()
        {
            configPath = ConfigurationManager.AppSettings["D365ConfigFilePath"].ToString();
            //@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\Config\d365.json";
        }

        [TestCase(@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\TestData\SampleAccount.json")]
        [TestCase(@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\TestData\SampleContact.json")]
        public void Run_Test_Scenario(string filePath)
        {
            var planner = new TestPlanner(configPath);
            planner.Execute(filePath);
        }
    }
}
