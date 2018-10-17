using EasyRepro.ScenarioManager.Model;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Configuration;

namespace EasyRepro.ScenarioManager
{
    public class TestPlanner
    {
        D365Config config;
        string configPath;

        static BrowserOptions Options = new BrowserOptions
        {
            BrowserType = BrowserType.Chrome,
            PrivateMode = true,
            FireEvents = true,
            Headless = false,
            UserAgent = false
        };

        public TestPlanner()
        {
            configPath = ConfigurationManager.AppSettings["D365ConfigFilePath"].ToString();
            //@"C:\Repository\Git\EasyRepro\EasyRepro.ScenarioManager\Config\d365.json";
        }

        public TestPlanner(string configPath)
        {
            this.configPath = configPath;
        }

        public void Init()
        {
            config = new Reader().ParseD365Config(configPath);
        }

        public void Execute(string filePath)
        {
            Init();
            var scenario = new Reader().ParseTestDataFromFile(filePath);

            using (var xrmBrowser = new Browser(Options))
            {
                xrmBrowser.LoginPage.Login(new Uri(config.Url), config.User.ToSecureString(), config.Password.ToSecureString());
                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                xrmBrowser.Dialogs.CloseWarningDialog();

                xrmBrowser.ThinkTime(500);

                if (!string.IsNullOrEmpty(scenario.Area) && !string.IsNullOrEmpty(scenario.SubArea))
                    xrmBrowser.Navigation.OpenSubArea(scenario.Area, scenario.SubArea);

                xrmBrowser.ThinkTime(500);

                if (!string.IsNullOrEmpty(scenario.View))
                    xrmBrowser.Grid.SwitchView(scenario.View);

                xrmBrowser.ThinkTime(500);
                foreach(var cmd in scenario.Commands)
                {
                    switch(cmd.Name.ToUpper())
                    {
                        case "SEARCH":
                            xrmBrowser.Grid.Search(cmd.Data[0].Value);

                            var results = xrmBrowser.Grid.GetGridItems();

                            if (results.Value.Count == 0)
                            {
                                throw new InvalidOperationException($"{scenario.Entity} not found or was not created.");
                            }
                            break;
                        case "DELETE":
                            xrmBrowser.Grid.Search(cmd.Data[0].Value);

                            xrmBrowser.Grid.OpenRecord(0);

                            xrmBrowser.CommandBar.ClickCommand("Delete");

                            xrmBrowser.Dialogs.Delete();
                            break;
                        default:
                            xrmBrowser.CommandBar.ClickCommand(cmd.Name);

                            xrmBrowser.ThinkTime(500);

                            foreach (var d in cmd.Data)
                            {
                                switch(d.Type)
                                {
                                    case AttributeType.TwoOption:
                                        xrmBrowser.Entity.SetValue(d.Name, bool.Parse(d.Value));
                                        break;
                                    case AttributeType.Composite:
                                        xrmBrowser.Entity.SetValue(new CompositeControl() { Id = d.Name, Fields = d.Fields });
                                        break;
                                    case AttributeType.OptionSet:
                                        xrmBrowser.Entity.SetValue(new OptionSet { Name = d.Name, Value = d.Value });
                                        break;
                                    case AttributeType.Date:
                                        xrmBrowser.Entity.SetValue(d.Name, DateTime.Parse(d.Value));
                                        break;
                                    default:
                                        xrmBrowser.Entity.SetValue(d.Name, d.Value);
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
