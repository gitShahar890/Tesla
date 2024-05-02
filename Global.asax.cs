using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Tesla
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["counter"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["firstName"] = "guest";
            Session["Admin"] = "No";
            Application["counter"] = (int)Application["counter"] + 1;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["Admin"] = "No";
            Session["Table"] = "false";
            Session["firstName"] = "guest";
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}