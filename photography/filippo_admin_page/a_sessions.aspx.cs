using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace photography.filippo_admin_page
{
    public partial class a_sessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (session_txt.Text.Trim() == "")
            {
                msg_lbl.Visible = true;
                msg_lbl.Text = "Must specify a session name!";
                return;
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                try
                {
                    SqlCommand insert_cmd = new SqlCommand("INSERT INTO sessions (session_name, session_desc, session_date) VALUES (@name, @desc, @date)", con);
                    insert_cmd.Parameters.AddWithValue("@name", session_txt.Text.Trim());
                    insert_cmd.Parameters.AddWithValue("@desc", desc_txt.Text.Trim());
                    insert_cmd.Parameters.AddWithValue("@date", date_txt.Text);

                    con.Open();
                    int result = insert_cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Error occured while saving into DB!";
                        return;
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("~/img/sessions/" + session_txt.Text));
                        SqlCommand id_get = new SqlCommand("SELECT MAX(session_id) FROM sessions", con);
                        int current_id = Convert.ToInt32(id_get.ExecuteScalar());
                        Response.Redirect("img_session.aspx?sid=" + current_id + "&sname=" + session_txt.Text);
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            //Response.Write(row.Cells[1].Text);
            Response.Redirect("img_session.aspx?sid=" + GridView1.DataKeys[GridView1.SelectedIndex].Value + "&sname=" + row.Cells[2].Text);
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
           
        }
    }
}