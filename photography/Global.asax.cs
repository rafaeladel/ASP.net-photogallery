using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace photography
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.Equals("/"))
            {
                HttpContext.Current.RewritePath(Request.Url.AbsolutePath + "homepage.aspx");
            }
            else if (Request.Url.AbsolutePath.EndsWith("filippo_admin_page/"))
            {
                HttpContext.Current.RewritePath(Request.Url.AbsolutePath + "login.aspx");
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}