using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace photography.filippo_admin_page
{
    public partial class admin_master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            Session["username"] = null;
            if (Request.Cookies["username"] != null)
            {
                HttpCookie my_cookie = new HttpCookie("username");
                my_cookie.Expires = DateTime.Now.AddSeconds(-1);
                Response.Cookies.Add(my_cookie);
                Response.Redirect("login.aspx");
            }
        }
    }
}