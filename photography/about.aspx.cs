using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
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
                    SqlCommand Sel_info = new SqlCommand();
                    Sel_info.Connection = con;
                    Sel_info.CommandType = CommandType.StoredProcedure;
                    Sel_info.CommandText = "Select_info_SP";
                    con.Open();
                    SqlDataReader rdr = Sel_info.ExecuteReader();
                    rdr.Read();
                    about_txt.InnerText = rdr["about"].ToString();
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