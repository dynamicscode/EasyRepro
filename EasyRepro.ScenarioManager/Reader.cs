using Newtonsoft.Json;
using System.IO;
using static EasyRepro.ScenarioManager.Model;

namespace EasyRepro.ScenarioManager
{
    public class Reader
    {
        public Reader()
        {

        }

        public Scenario ParseFromFile(string jsonFile)
        {
            string jsonData = File.ReadAllText(jsonFile);

            return Parse(jsonData);
        }

        public Scenario Parse(string jsonData)
        {
            return JsonConvert.DeserializeObject<Scenario>(jsonData);
        }
    }
}
