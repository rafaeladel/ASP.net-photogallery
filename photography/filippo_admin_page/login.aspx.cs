using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace photography.filippo_admin_page
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.FindControl("nav_container").Visible = false;
            Master.FindControl("logout").Visible = false;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlCommand check_user = new SqlCommand("SELECT COUNT(*) FROM admins", con);
                con.Open();
                int count = (int)check_user.ExecuteScalar();
                if (count == 0)
                {
                    Response.Redirect("register.aspx");
                }
            }

            if (Request.Cookies["username"] == null)
            {
                if (Session["username"] != null)
                {
                    Response.Redirect("a_gallery.aspx");
                }
            }
            else
            {
                Response.Redirect("a_gallery.aspx");
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string username = un_text.Text;
            string password = pw_text.Text;

            if (username.Length == 0 || password.Length == 0)
            {
                msg_lbl.Text = "Username and Password are required";
            }
            else
            {
                string hashed_password = Encrypt(password);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    try
                    {
                        SqlCommand check_user = new SqlCommand("SELECT username, hashed_password FROM admins WHERE username=@username AND hashed_password=@hashed_password", con);
                        check_user.Parameters.AddWithValue("@username", username);
                        check_user.Parameters.AddWithValue("@hashed_password", hashed_password);
                        con.Open();
                        SqlDataReader rdr = check_user.ExecuteReader();
                        bool has_result = rdr.Read();
                        if(has_result)
                        {
                            if (username == rdr["username"].ToString() && hashed_password == rdr["hashed_password"].ToString())
                            {
                                Session["username"] = rdr["username"].ToString();
                                if (remembercheck.Checked)
                                {
                                    HttpCookie my_cookie = new HttpCookie("username");
                                    my_cookie["username"] = rdr["username"].ToString();
                                    my_cookie.Expires = DateTime.Now.AddDays(30);
                                    Response.Cookies.Add(my_cookie);
                                }
                                Response.Redirect("a_gallery.aspx");                                
                            }
                            else
                            {
                                msg_lbl.Text = "Invalid username/password!";
                            }
                        }
                        else
                        {
                            msg_lbl.Text = "Invalid username/password!";
                        }
                    }
                    catch (SqlException ex)
                    {
                        msg_lbl.Text = ex.Message;
                    }
                }
            }
        }

        protected string Encrypt(string input)
        {
            byte[] tempSrc = ASCIIEncoding.ASCII.GetBytes(input);
            SHA256CryptoServiceProvider hashTool = new SHA256CryptoServiceProvider();
            byte[] tempHash = hashTool.ComputeHash(tempSrc);
            return string.Join("",tempHash);
        }
    }
}