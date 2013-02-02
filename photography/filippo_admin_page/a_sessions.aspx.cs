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
            if (Request.Cookies["username"] == null)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("login.aspx");
                }
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlCommand check_cat = new SqlCommand("SELECT COUNT(*) FROM sessions", con);
                con.Open();
                int count = (int)check_cat.ExecuteScalar();
                if (count == 0)
                {
                    DeleteBtn.Visible = false;
                }
            }
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
                    Guid session_guid = Guid.NewGuid();
                    SqlCommand insert_cmd = new SqlCommand("INSERT INTO sessions (session_name, session_desc, session_date, session_guid) VALUES (@name, @desc, @date, @guid)", con);
                    insert_cmd.Parameters.AddWithValue("@name", session_txt.Text.Trim());
                    insert_cmd.Parameters.AddWithValue("@desc", desc_txt.Text.Trim());
                    insert_cmd.Parameters.AddWithValue("@date", date_txt.Text);
                    insert_cmd.Parameters.AddWithValue("@guid", session_guid);

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
                        Directory.CreateDirectory(Server.MapPath("~/img/sessions/" + session_guid));
                        SqlCommand id_get = new SqlCommand("SELECT MAX(session_id) FROM sessions", con);
                        int current_id = Convert.ToInt32(id_get.ExecuteScalar());
                        Response.Redirect("img_session.aspx?sid=" + current_id + "&sguid=" + session_guid);
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
            string sid = GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString() ;
            string session_guid = ((Label)row.Cells[9].FindControl("Label1")).Text;
            Response.Redirect("img_session.aspx?sid=" + sid + "&sguid=" + session_guid);
        }


        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = GridView1.Rows.Count;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {                                                                           
                    SqlCommand deleteSessionCmd = new SqlCommand("DELETE FROM sessions WHERE session_id=@id", con);
                    SqlCommand deleteImgCmd = new SqlCommand("DELETE FROM img_session WHERE session_id=@id", con);
                    con.Open();
                    for (int i = 0; i < rowCount; i++)
                    {
                        CheckBox deleteChk = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("deleteCheck");
                        int rowID = Convert.ToInt32(GridView1.DataKeys[i].Value);
                        string session_title = GridView1.Rows[i].Cells[3].Text;
                        string session_guid = GridView1.Rows[i].Cells[9].Text;
                        if (deleteChk.Checked)
                        {
                            deleteSessionCmd.Parameters.AddWithValue("@id", rowID);
                            deleteSessionCmd.ExecuteNonQuery();
                            deleteSessionCmd.Parameters.Clear();

                            deleteImgCmd.Parameters.AddWithValue("@id", rowID);
                            deleteImgCmd.ExecuteNonQuery();
                            deleteImgCmd.Parameters.Clear();

                            DeleteSession(session_title, session_guid);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                msg_lbl.Visible = true;
                msg_lbl.Text = ex.Message;                      
            }
            catch (IOException ex)
            {
                msg_lbl.Visible = true;
                msg_lbl.Text = ex.Message;
            }
            GridView1.DataBind();
        }

        private void DeleteSession( string sessionName ,string sessionGUID)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/img/sessions/" + sessionGUID));
                dir.Delete(true);
                msg_lbl.Visible = true;
                msg_lbl.Text = string.Format("Session {0} Is deleted.", sessionName);
            }
            catch (IOException ex)
            {
                msg_lbl.Visible = true;
                msg_lbl.Text = ex.Message;
            }
        }

    }
}