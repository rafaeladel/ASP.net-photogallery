using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace photography
{
    public partial class gallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataSet dst = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlDataAdapter adbtr = new SqlDataAdapter("SELECT DISTINCT img_cat FROM gallery", con);
                try
                {
                    int result = adbtr.Fill(dst);
                    if (result == 0)
                    {
                        return;
                    }
                    cat_repeater.DataSource = dst;
                    cat_repeater.DataBind();
                }
                catch(Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }

            using (SqlConnection img_con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlDataAdapter img_adbtr = new SqlDataAdapter("SELECT * from gallery WHERE img_cat=@img_cat", img_con);
                DataSet img_dst = new DataSet();
                img_adbtr.SelectCommand.Parameters.AddWithValue("@img_cat", dst.Tables[0].Rows[0][0].ToString());                
                try
                {
                    img_adbtr.Fill(img_dst);
                    slider_repeater.DataSource = img_dst;
                    slider_repeater.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }                
            }            
        }

        protected void cat_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM gallery WHERE img_cat=@img_cat", con);
                cmd.Parameters.AddWithValue("@img_cat", ((LinkButton)e.CommandSource).Text);
                con.Open();
                slider_repeater.DataSource = cmd.ExecuteReader();
                slider_repeater.DataBind();
            }
        }
    }
}