using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
    
namespace photography.filippo_admin_page
{
    public partial class a_gallery : System.Web.UI.Page
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
        }    

        protected void submit_Click(object sender, EventArgs e)
        {
            //setting up variables
            string img_title = title_txt.Text,
                img_desc = desc_txt.Text,
                img_cat = cat_txt.Text.Trim().Length > 0 ? cat_txt.Text.Trim() : cat_ddl.SelectedValue;

            //check if user selected files or not yet
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
                msg_lbl.Visible = true;
                msg_lbl.Text = "You have to select an image.";
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
                msg_lbl.Visible = true;
                msg_lbl.Text = "Selected file(s) must be an image.";
                return;
            }


            //Everything is ok... Start uploading!
            string img_folder = Server.MapPath("~/img/gallery/" + img_cat);
            if (!Directory.Exists(img_folder))
            {
                Directory.CreateDirectory(img_folder);
            }
                
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile my_file = Request.Files[i];

                //alt_file : for record saving on DB
                //img_file : for file saving on the server
                if (my_file.ContentLength > 0)
                {
                    string alt_file = "~/img/gallery/" + img_cat + "/" + my_file.FileName,
                        img_file = Server.MapPath(alt_file);
                    my_file.SaveAs(img_file);
                    InsertDB(img_title, img_desc, img_cat, alt_file);
                    GridView1.DataBind();
                    cat_ddl.DataBind();
                }
            }
          
        }

        protected void InsertDB(string title, string desc, string cat, string path)
        {
            DateTime now = DateTime.Now;
            title = title.Length == 0 ? "Untitled" : title;
            cat = cat.Length == 0 ? "Uncategorized" : cat;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {                    
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO gallery (img_title, img_desc, img_cat, img_date, img_path)
                                                    VALUES (@title, @desc, @cat, @date, @path)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@title", title.Trim());
                    cmd.Parameters.AddWithValue("@desc", desc.Trim());
                    cmd.Parameters.AddWithValue("@cat", cat.Trim());
                    cmd.Parameters.AddWithValue("@date", now);
                    cmd.Parameters.AddWithValue("@path", path.Trim());
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "New Image is uploaded.";
                        title_txt.Text = "";
                        desc_txt.Text = "";
                        cat_txt.Text = "";                        
                    }
                    else
                    {
                        msg_lbl.Visible = true;
                        msg_lbl.Text = "Error occured.";
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

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            DirectoryInfo my_dir = new DirectoryInfo(Server.MapPath("~/img/gallery/" + e.Values["img_cat"]));
            try
            {                
                foreach (FileInfo my_file in my_dir.GetFiles())
                {
                    //getting path of the deleted file
                    string path = e.Values["img_path"].ToString();
                    //delete file
                    if (my_file.Name == path.Substring(path.LastIndexOf("/") + 1))
                    {
                        my_file.Delete();
                    }
                }
                if (my_dir.GetFiles().Length == 0)
                {                    
                    my_dir.Delete();
                    msg_lbl.Visible = true;
                    cat_ddl.DataBind();
                    msg_lbl.Text = "Category: " + e.Values["img_cat"] + " is deleted completely.";
                }
            }
            catch (IOException ex)
            {
                msg_lbl.Visible = true;
                msg_lbl.Text = ex.Message;
            }
        }
    }
}