﻿using System.Web.Http;

namespace iMe
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.Configure();
            UnityConfig.GetConfiguredContainer();
        }
    }
}
