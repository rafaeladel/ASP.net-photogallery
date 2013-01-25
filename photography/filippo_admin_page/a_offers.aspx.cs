using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace photography.filippo_admin_page
{
    public partial class a_offers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["username"] == null)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        protected void submit_btn_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string offer_title = title_txt.Text,
                offer_body = body_txt.Text;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO offers (offer_title, offer_body, offer_date)
                                                 VALUES (@offer_title, @offer_body, @offer_date)", con);
                    cmd.Parameters.AddWithValue("@offer_title", offer_title);
                    cmd.Parameters.AddWithValue("@offer_body", offer_body);
                    cmd.Parameters.AddWithValue("@offer_date", now);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "New Offer added!";
                        title_txt.Text = "";
                        body_txt.Text = "";
                        GridView1.DataBind();
                    }
                    else
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Failed to add new offer.";
                    }
                }
                catch (SqlException ex)
                {
                    msg_lbl.Visible = true;
                    msg_lbl.Text = ex.Message;
                }
                catch (Exception ex)
                {
                    msg_lbl.Visible = true;
                    msg_lbl.Text = ex.Message;
                }
            }
        }

    }
}