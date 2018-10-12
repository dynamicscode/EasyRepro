namespace EasyRepro.ScenarioManager
{
    public class Model
    {
        public class Scenario
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Area { get; set; }
            public string SubArea { get; set; }
            public string View { get; set; }
            public Command[] Commands { get; set; }
        }

        public class Command
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Attribute[] Data { get; set; }
        }

        public class Attribute
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
