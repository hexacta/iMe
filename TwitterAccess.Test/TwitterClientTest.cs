using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TwitterAccess.Test
{
    [TestClass]
    public class TwitterClientTest
    {
        [TestMethod]
        public void GetPersonalInfoTest()
        {
            var response = (new TwitterClient()).GetPersonalInfo("Hexacta").Result;
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 1);
            Assert.AreEqual(response.First().UserName, "Hexacta");
        }
    }
}
