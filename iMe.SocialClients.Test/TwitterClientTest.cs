﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using iMe;
using iMe.SocialClients;

namespace iMe.SocialClients.Test
{
    [TestClass]
    public class TwitterClientTest
    {
        [TestInitialize]
        public void Setup()
        {
            AutoMapperConfig.Configure();
        }
        
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