using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;

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
                    else
                    {
                        if (Request.QueryString["cat"] == null || Request.QueryString.Count > 1)
                        {
                            string firstCat = dst.Tables[0].Rows[0][0].ToString();
                            Response.Redirect("gallery.aspx?cat=" + firstCat);
                        }
                        else
                        {
                            bool catExists = dst.Tables[0].AsEnumerable()
                                                .Any(row => Request.QueryString["cat"].ToString() ==  row.Field<string>("img_cat"));
                            if (catExists == false)
                            {
                                string firstCat = dst.Tables[0].Rows[0][0].ToString();
                                Response.Redirect("gallery.aspx?cat=" + firstCat);
                            }
                        }
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
                img_adbtr.SelectCommand.Parameters.Add("@img_cat", SqlDbType.NVarChar, 8000).Value = Request.QueryString["cat"].ToString();
                DataSet img_dst = new DataSet();
                try
                {
                    img_adbtr.Fill(img_dst);
                    photosCount.InnerText = img_dst.Tables[0].Rows.Count + " Photo(s)";
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
            string catName = ((LinkButton)e.CommandSource).Text;
            Response.Redirect("gallery.aspx?cat=" + catName);
        }
    }
}