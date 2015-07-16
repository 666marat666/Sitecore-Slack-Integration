using Sitecore.Data.Events;
using Sitecore.Diagnostics;
using SitecoreSlackIntegration.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreSlackIntegration.Events.Publish.Remote
{
    public class PublishEndRemote
    {
        public void Execute(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            PublishEndRemoteEventArgs pOptions = args as PublishEndRemoteEventArgs;

            string rootItemId;

            if (pOptions.RootItemId == null)
            {
                rootItemId = "null";
            }
            else
            {
                rootItemId = pOptions.RootItemId.ToString();
            }

            IntegrationManager.instance.PostPlainTextMessageIntoChannel(
                    String.Format("*[Publish end remote]* ```\nMode: {0} \nPublish targets: {1} \nRepublish All: {2} \nRoot item ID: {3} \nSource db: {4} \nTarget db: {5}```",
                    pOptions.Mode.ToString(),//0
                    pOptions.PublishingTargets.Aggregate((x, y) => x + "|" + y),//1
                    pOptions.RepublishAll,//2
                    rootItemId,//3
                    pOptions.SourceDatabaseName,//4
                    pOptions.TargetDatabaseName//5                                                          
                    )
                );
        }
    }
}
