using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreSlackIntegration.Core.Base
{
    public class SettingsProvider
    {
        static SettingsProvider _instance = null;
        static object lockObject = new object();

        public static SettingsProvider instance
        {
            get
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new SettingsProvider();
                        return _instance;
                    }
                    else return _instance;
                }
            }
        }

        public List<string> GetFilteredJobs()
        { 
            List<string> result = new List<string>();
            string filteredJobs = Sitecore.Configuration.Settings.GetSetting("Slack.FilteredJobs", "UnlockContactListsAgent|Index_Update_IndexName");
            result = filteredJobs.Split('|').ToList();
            return result;
        }
    }
}
