using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Publishing;
using SitecoreSlackIntegration.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreSlackIntegration.Events.Publish
{
    public class PublishFailEvent
    {
        public void Execute(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            Exception ex = (Event.ExtractParameter(args, 1) as Exception);
            PublishOptions pOptions = (Event.ExtractParameter(args, 0) as Publisher).Options;

            string rootItemName = "null";
            string rootItemPath = "null";

            if (pOptions.RootItem != null)
            {
                rootItemName = pOptions.RootItem.Name;
                rootItemPath = pOptions.RootItem.Paths.ContentPath;
            }

            IntegrationManager.instance.PostPlainTextMessageIntoChannel(
                String.Format(
                "*[Publish fail]* \nMessage: {0} \nException stack: {1} \nRoot item: {2} [{3}]",
                ex.Message,
                ex.StackTrace,
                rootItemName,
                rootItemPath
                )
                );

        }
    }
}
