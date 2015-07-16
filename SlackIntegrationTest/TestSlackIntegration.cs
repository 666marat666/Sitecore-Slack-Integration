using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SitecoreSlackIntegration.Core.Base;
 

namespace SlackIntegrationTest
{
    [TestClass]
    public class TestSlackIntegration
    {
        [TestMethod]
        public void TestPlainTextMessage()
        {
            Assert.IsTrue(
                IntegrationManager.instance.PostPlainTextMessageIntoChannel("Hello!")
                );
        }
    }
}
