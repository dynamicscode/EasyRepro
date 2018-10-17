using Newtonsoft.Json;
using System.IO;
using EasyRepro.ScenarioManager.Model;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace EasyRepro.ScenarioManager
{
    public class Reader
    {
        public Reader()
        {

        }

        public D365Config ParseD365Config(string filePath)
        {
            return ParseFromFile<D365Config>(filePath);
        }

        public Scenario ParseTestDataFromFile(string filePath)
        {
            return ParseFromFile<Scenario>(filePath);
        }

        public T ParseFromFile<T>(string filePath)
        {
            string jsonData = File.ReadAllText(filePath);

            return Parse<T>(jsonData);
        }

        public T Parse<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
