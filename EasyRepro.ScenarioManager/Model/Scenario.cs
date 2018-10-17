using Microsoft.Dynamics365.UIAutomation.Api;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyRepro.ScenarioManager.Model
{
    public class Scenario
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Entity { get; set; }
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
        public AttributeType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public List<Field> Fields { get; set; }
    }

    public enum AttributeType
    {
        [JsonProperty("text")]
        Text,
        [JsonProperty("date")]
        Date,
        [JsonProperty("datetime")]
        DateTime,
        [JsonProperty("optionset")]
        OptionSet,
        [JsonProperty("lookup")]
        Lookup,
        [JsonProperty("composite")]
        Composite,
        [JsonProperty("twooption")]
        TwoOption
    }
}
