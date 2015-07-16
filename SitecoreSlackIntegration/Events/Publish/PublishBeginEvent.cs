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
    public class PublishBeginEvent
    {
        public void Execute(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            //TODO: send event
            PublishOptions pOptions = (Event.ExtractParameter(args, 0) as Publisher).Options;
            string rootItemPath;
            string rootItemName;

            if (pOptions.RootItem == null)
            {
                rootItemPath = "null";
                rootItemName = "null";                
            }
            else
            {
                rootItemPath = rootItemPath = pOptions.RootItem.Paths.ContentPath;
                rootItemName = pOptions.RootItem.Name;
            }

            IntegrationManager.instance.PostPlainTextMessageIntoChannel(
                    String.Format("*[Publish begin]* \nMode: {0} \nPublish targets: {1} \nPublish related items: {2} \nRepublish All: {3} \nRoot item: {4} [{5}] \nSource db: {6} \nTarget db: {7}",
                    pOptions.Mode.ToString(),//0
                    pOptions.PublishingTargets.Aggregate((x, y) => x + "|" + y),//1
                    pOptions.PublishRelatedItems,//2
                    pOptions.RepublishAll,//3
                    rootItemName,//4
                    rootItemPath,//5
                    pOptions.SourceDatabase.ConnectionStringName,//6
                    pOptions.TargetDatabase.ConnectionStringName//7                                                           
                    )
                );

        }
    }
}
