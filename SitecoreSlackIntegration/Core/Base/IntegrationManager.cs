using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitecoreSlackIntegration.Core.Base
{
    public class IntegrationManager
    {
        static IntegrationManager integrationManager = null;
        static object lockObject = new object();

        SlackClient client = null;
        string instanceName = null;
        string channelName = null;        
        
        public static IntegrationManager instance
        {
            get
            {
                lock (lockObject)//because may be reuqested from different publishing threads
                {
                    if (integrationManager == null)
                    {
                        integrationManager = new IntegrationManager();
                        return integrationManager;
                    }
                    else
                    {                        
                        return integrationManager;
                    }
                }
            }
        }

        public IntegrationManager()
        {
            client = new SlackClient(
                    Sitecore.Configuration.Settings.GetSetting("Slack.WebHook.URL", "https://hooks.slack.com/services/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") //TODO: hardcoded url
                );
            instanceName = Sitecore.Configuration.Settings.GetSetting("Slack.WebHook.InstanceName", "Sitecore");
            channelName =  Sitecore.Configuration.Settings.GetSetting("Slack.WebHook.ChannelName", "#general");
        }

        public bool PostPlainTextMessageIntoChannel(string messageText)
        {
            var message = new SlackMessage
            {
                Channel = channelName,
                Text = messageText,
                IconEmoji = Emoji.Ghost, //TODO: set as setting
                Username = instanceName
            };

            return client.Post(message);
        }
    }
}
