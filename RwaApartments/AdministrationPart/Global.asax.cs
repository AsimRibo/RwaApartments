using RwaUtilities.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace AdministrationPart
{
    public class Global : System.Web.HttpApplication
    {
        private readonly IRepository repository;

        public Global()
        {
            repository = RepositoryFactory.GetRepository();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["database"] = repository;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }
}