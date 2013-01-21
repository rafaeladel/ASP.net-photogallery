using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace photography
{
    public partial class about : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand check_cmd = new SqlCommand("SELECT COUNT(*) FROM info", con);
                    con.Open();
                    int count = (int)check_cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        SqlCommand create_cmd = new SqlCommand("INSERT INTO info VALUES(NULL, NULL, NULL, NULL, NULL)", con);
                        create_cmd.ExecuteNonQuery();
                    }
                    else if (count == 1)
                    {
                        SqlCommand read_cmd = new SqlCommand("SELECT about FROM info", con);
                        SqlDataReader rdr = read_cmd.ExecuteReader();
                        rdr.Read();
                        about_txt.InnerText = rdr["about"].ToString();
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