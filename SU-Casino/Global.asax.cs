using SU_Casino.service;
using System;

namespace SU_Casino
{
    public class Global : System.Web.HttpApplication
    {
        DBDataService _database = new DBDataService();

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs  
            Exception Ex = Server.GetLastError();
            var log = new EventLog("Unhandled error", null, Ex);

            _database.Log(log);

            // Server.ClearError();
            // Server.Transfer("ErrorPage.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}