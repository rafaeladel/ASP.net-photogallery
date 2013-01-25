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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.FindControl("nav_container").Visible = false;
            Master.FindControl("logout").Visible = false;
        }
    
        protected void register_btn_Click(object sender, EventArgs e)
        {
            if (un_text.Text.Trim().Length == 0 || pw_text.Text.Trim().Length == 0)
            {
                msg_lbl.Text = "Both Username and password are required.";
            }
            else
            {
                string username = un_text.Text;
                string hashed_password = Encrypt(pw_text.Text);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                {
                    SqlCommand user_check = new SqlCommand("SELECT COUNT(*) FROM admins WHERE username=@username AND hashed_password=@hashed_password", con);
                    user_check.Parameters.AddWithValue("@username", username);
                    user_check.Parameters.AddWithValue("@hashed_password", hashed_password);
                    con.Open();
                    int count = (int)user_check.ExecuteScalar();
                    if (count > 0)
                    {
                        msg_lbl.Text = "User already exists.";
                    }
                    else
                    {
                        SqlCommand register_user = new SqlCommand("INSERT INTO admins (username, hashed_password) VALUES (@username, @hashed_password)", con);
                        register_user.Parameters.AddWithValue("@username", username);
                        register_user.Parameters.AddWithValue("@hashed_password", hashed_password);
                        int result = register_user.ExecuteNonQuery();
                        if (result == 1)
                        {
                            Response.Redirect("login.aspx");
                        }
                        else
                        {
                            msg_lbl.Text = "Error while registering a new admin.";
                        }
                    }
                }
            }
         }

        protected string Encrypt(string input)
        {
            byte[] tempSrc = ASCIIEncoding.ASCII.GetBytes(input);
            SHA256CryptoServiceProvider hashTool = new SHA256CryptoServiceProvider();
            byte[] tempHash = hashTool.ComputeHash(tempSrc);
            return string.Join("", tempHash);
        }
    }
}