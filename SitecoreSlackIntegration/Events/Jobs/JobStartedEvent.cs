using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Jobs;
using SitecoreSlackIntegration.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreSlackIntegration.Events.Jobs
{
    public class JobStartedEvent
    {
        public void Execute(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            JobStartedEventArgs jobEvent = (Event.ExtractParameter(args, 0) as JobStartedEventArgs);

            IntegrationManager.instance.PostPlainTextMessageIntoChannel(
                String.Format(
                "*[Job started]* \nName: {0} [{1}] \nCategory: {2} \nSite name: {3} \nPipeline name: {4} \nContext item:",
                jobEvent.Job.Name,//0
                jobEvent.Job.Options.JobName,//1
                jobEvent.Job.Category,//2
                jobEvent.Job.Options.SiteName,//3
                jobEvent.Job.Options.PipelineName,//4    
                jobEvent.Job.Options.Item.Paths.ContentPath //5
                )
                );
        }
    }
}
