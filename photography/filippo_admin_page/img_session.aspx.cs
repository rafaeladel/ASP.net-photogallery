using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace photography.filippo_admin_page
{
    public partial class img_session : System.Web.UI.Page
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

            int sid = 0;
            bool sid_valid = int.TryParse(Request.QueryString["sid"], out sid);
            if (sid_valid && Request.QueryString["sname"] != null)
            {
                if (Request.UrlReferrer == null)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                    {
                        SqlCommand check_cmd = new SqlCommand("SELECT COUNT(*) FROM sessions WHERE session_id=@session_id AND session_name=@session_name", con);
                        check_cmd.Parameters.AddWithValue("@session_id", sid);
                        check_cmd.Parameters.AddWithValue("@session_name", Request.QueryString["sname"]);
                        con.Open();
                        int result = Convert.ToInt32(check_cmd.ExecuteScalar());
                        if (result == 0)
                        {
                            Response.Redirect("a_sessions.aspx");
                        }                       
                    }
                }

                if (!IsPostBack)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                    {
                        SqlCommand get_cmd = new SqlCommand("SELECT session_name, session_desc, session_date, session_cover FROM sessions WHERE session_id=@session_id", con);
                        get_cmd.Parameters.AddWithValue("@session_id", Request.QueryString["sid"]);
                        con.Open();
                        SqlDataReader rdr = get_cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            try
                            {
                                session_txt.Text = rdr["session_name"].ToString();
                                desc_txt.Text = rdr["session_desc"].ToString();
                                date_txt.Text = rdr["session_date"].ToString();
                                cover_img.ImageUrl = rdr["session_cover"].ToString();
                            }
                            catch
                            {
                                Response.Redirect("a_sessions.aspx");
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("a_sessions.aspx");
            }
        }

        protected void Insert_Click(object sender, EventArgs e)
        {
            bool ok = true;
            string folder_dir = null;

            bool have_file = true;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile my_file = Request.Files[i];
                if (my_file.ContentLength > 0)
                {
                    have_file = true;
                    break;
                }
                have_file = false;
            }

            if (have_file == false)
            {
                upload_msg.Visible = true;
                upload_msg.Text = "You have to select an image.";
                return;
            }

            //Checks if selected files are images or not
            bool reg_match = true;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile my_file = Request.Files[i];
                if (Regex.IsMatch(my_file.FileName, @"^.+(.jpg|.JPG|.ttf|.TTF|.png|.PNG|.gif|.GIF)$"))
                {
                    reg_match = true;
                }
                else
                {
                    //IE: if user entered file in one input and left the other.
                    //checks if it's the file in a first place or empty input 
                    if (my_file.ContentLength > 0)
                    {
                        reg_match = false;
                        break;
                    }
                }
            }

            if (reg_match == false)
            {
                upload_msg.Visible = true;
                upload_msg.Text = "Selected file(s) must be an image.";
                return;
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                //Inserting Images into DB
                SqlCommand insert_cmd = new SqlCommand("INSERT INTO img_session (session_img_name,session_id,session_img_path) VALUES (@name,@session_id,@path)", con);
                insert_cmd.Parameters.AddWithValue("@session_id", Request.QueryString["sid"]);
                con.Open();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile my_file = Request.Files[i];
                    if (my_file.ContentLength > 0)
                    {
                        string path = "~/img/sessions/" + Request.QueryString["sname"] + "/" + my_file.FileName;
                        insert_cmd.Parameters.AddWithValue("@name", my_file.FileName);
                        insert_cmd.Parameters.AddWithValue("@path", path);
                        int result = insert_cmd.ExecuteNonQuery();
                        if (result != 1)
                        {
                            upload_msg.Visible = true;
                            upload_msg.Text = "Error Occured! while uploading image : " + my_file.FileName;
                            ok = false;
                            break;
                        }
                        //uploading files
                        folder_dir = Path.Combine(Server.MapPath("~/img/sessions/"), Request.QueryString["sname"]);
                        string file_dir = Path.Combine(folder_dir, my_file.FileName);
                        my_file.SaveAs(file_dir);
                        insert_cmd.Parameters.RemoveAt("@name");
                        insert_cmd.Parameters.RemoveAt("@path");
                    }
                }

                //get images count
                DirectoryInfo dir = new DirectoryInfo(folder_dir);
                int img_count = dir.GetFiles().Length;
                
                //Inserting image count into sessions table
                SqlCommand insert_count = new SqlCommand("UPDATE sessions SET session_photos=@count WHERE session_id=@session_id", con);
                insert_count.Parameters.AddWithValue("@count", img_count);
                insert_count.Parameters.AddWithValue("@session_id", Request.QueryString["sid"]);
                int insert_count_result = insert_count.ExecuteNonQuery();
                if (insert_count_result != 1)
                {
                    upload_msg.Visible = true;
                    upload_msg.Text = "Error Occured! while Updating images count!";
                    ok = false;
                }

                if (ok)
                {
                    img_session_grid.DataBind();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                try
                {
                    SqlCommand insert_cmd = new SqlCommand("UPDATE sessions SET session_name=@name, session_desc=@desc, session_date=@date WHERE session_id=@id", con);
                    insert_cmd.Parameters.AddWithValue("@id", Request.QueryString["sid"]);
                    insert_cmd.Parameters.AddWithValue("@name", session_txt.Text.Trim());
                    insert_cmd.Parameters.AddWithValue("@desc", desc_txt.Text.Trim());
                    insert_cmd.Parameters.AddWithValue("@date", date_txt.Text);
                    con.Open();

                    int result = insert_cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Error Occured! while Updating Session info";
                    }
                    else
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Session Updated.";
                    }
                }
                catch (SqlException ex)
                {
                    msg_lbl.Visible = true;
                    msg_lbl.Text = ex.Message;
                }
            }
        }

        protected void img_session_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string img_path = img_session_grid.DataKeys[img_session_grid.SelectedIndex].Value.ToString();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                try
                {
                    SqlCommand insert_path = new SqlCommand("UPDATE sessions SET session_cover=@cover WHERE session_id=@id", con);
                    insert_path.Parameters.AddWithValue("@cover", img_path);
                    insert_path.Parameters.AddWithValue("@id", Request.QueryString["sid"]);
                    con.Open();
                    int result = insert_path.ExecuteNonQuery();
                    if (result != 1)
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Failed setting cover photo.";
                    }
                    else
                    {
                        cover_img.ImageUrl = img_path;
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Cover photo updated!";
                    }
                }
                catch (SqlException ex)
                {
                    msg_lbl.Visible = true;
                    msg_lbl.Text = ex.Message;
                }
            }
        }
    }
}