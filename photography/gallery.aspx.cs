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
                SqlDataAdapter adbtr = new SqlDataAdapter();                
                adbtr.SelectCommand = new SqlCommand("SELECT * FROM dbo.Select_gallery_names_FN()", con);
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
                SqlDataAdapter img_adbtr = new SqlDataAdapter();
                img_adbtr.SelectCommand = new SqlCommand("select * from dbo.Select_gallery_cat_FN(@img_cat)", img_con);
                img_adbtr.SelectCommand.Parameters.Add("@img_cat",SqlDbType.NVarChar,8000).Value = dst.Tables[0].Rows[0][0].ToString();
                DataSet img_dst = new DataSet();                
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
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from dbo.Select_gallery_cat_FN(@img_cat)";
                cmd.Parameters.Add("@img_cat", SqlDbType.NVarChar,8000).Value = ((LinkButton)e.CommandSource).Text;
                con.Open();
                slider_repeater.DataSource = cmd.ExecuteReader();
                slider_repeater.DataBind();
            }
        }
    }
}