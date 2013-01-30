using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace photography
{
    public partial class offers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT offer_title, offer_body FROM dbo.Select_offers_FN() ORDER BY offer_date DESC", con);
                con.Open();
                try
                {
                    offers_repeater.DataSource = cmd.ExecuteReader();
                    offers_repeater.DataBind();
                }
                catch { }
            }
        }
    }
}