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
    public partial class sessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int session_id = 0;
            bool show_imgs = int.TryParse(Request.QueryString["sid"], out session_id);
            if (show_imgs)
            {
                sessions_panel.Visible = false;
                images_panel.Visible = true;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    SqlCommand info_cmd = new SqlCommand("SELECT session_name, session_desc, session_date FROM Select_session_By_ID_FN(@id)", con);
                    info_cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = session_id;
                    con.Open();
                    SqlDataReader rdr = info_cmd.ExecuteReader();
                    bool has_record = rdr.Read();
                    if (has_record == false)
                    {
                        Response.Redirect("sessions.aspx");
                    }
                    while (has_record)
                    {
                        session_title.InnerText = rdr["session_name"].ToString();
                        if (rdr["session_desc"].ToString().Trim().Length == 0)
                        {
                            session_desc.Visible = false;
                        }
                        else
                        {
                            session_desc.InnerText = rdr["session_desc"].ToString();
                        }

                        if (rdr["session_date"].ToString().Trim().Length == 0)
                        {
                            session_date.Visible = false;
                        }
                        else
                        {
                            session_date.InnerText = rdr["session_date"].ToString();
                        }
                        has_record = rdr.Read();
                    }
                }
            }
            else
            {
                sessions_panel.Visible = true;
                images_panel.Visible = false;
            }
        }

        protected void sessions_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string sid = ((Button)e.CommandSource).Attributes["data-session-id"];
            Response.Redirect("sessions.aspx?sid=" + sid);
            
        }

    }
}