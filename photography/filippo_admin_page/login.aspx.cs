using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace photography.filippo_admin_page
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.FindControl("nav_container").Visible = false;
        }

        protected void submit_Click(object sender, EventArgs e)
        {

        }
    }
}