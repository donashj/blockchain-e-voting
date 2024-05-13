using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace Project
{
    public partial class ManageVoters : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);
        string vid;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetId();
                GvBind();
            }
        }

        protected void SetId()
        {
            string q = "Select Top 1 Id from Voters order by Id desc";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                vid = ds.Tables[0].Rows[0][0].ToString();
                id = Convert.ToInt32(vid);
                id++;
                txtid.Text = id.ToString();
            }
            else
            {
                txtid.Text = "1001";
            }
        }

        protected void GvBind()
        {
            string q = "Select Id, Concat(FName,' ',MName,' ',LName) as Name, DOB, Age, photo  from Voters";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                voterGrid.Visible = true;
                lbnodata.Visible = false;
            }
            else
            {
                voterGrid.Visible = false;
                lbnodata.Visible = true;
            }
        }

        protected void lbtnaddnew_Click(object sender, EventArgs e)
        {
            SetId();
            txtfname.Text = "";
            txtmname.Text = "";
            txtlname.Text = "";
            txtdob.Text = "";
            txtage.Text = "";
            txtmobno.Text = "";
            txtemail.Text = "";
            txtrmbldg.Text = "";
            txtstrarea.Text = "";
            txtcity.Text = "";
            txtstate.Text = "";
            txtcntry.Text = "";
            txtpincode.Text = "";
            txtusernm.Text = "";
            txtpass.Text = "";
            trlogindet.Visible = true;
            btnregister.Visible = true;
            btnUpdate.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);
        }

        protected void lbtnedit_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            string voterid = row.Cells[1].Text;

            string q = "Select Id, FName, MName, LName, DOB, Age, MobileNo, EmailId, RmNoBldg, StreetArea, City, State, Country, Pincode, photo  from Voters where id='" + voterid + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtid.Text = ds.Tables[0].Rows[0][0].ToString();
                txtfname.Text = ds.Tables[0].Rows[0][1].ToString();
                txtmname.Text = ds.Tables[0].Rows[0][2].ToString();
                txtlname.Text = ds.Tables[0].Rows[0][3].ToString();
                string d= ds.Tables[0].Rows[0][4].ToString();
                DateTime date = Convert.ToDateTime(d);
                txtdob.Text = date.ToString("yyyy-MM-dd");
                txtage.Text = ds.Tables[0].Rows[0][5].ToString();
                txtmobno.Text = ds.Tables[0].Rows[0][6].ToString();
                txtemail.Text = ds.Tables[0].Rows[0][7].ToString();
                txtrmbldg.Text = ds.Tables[0].Rows[0][8].ToString();
                txtstrarea.Text = ds.Tables[0].Rows[0][9].ToString();
                txtcity.Text = ds.Tables[0].Rows[0][10].ToString();
                txtstate.Text = ds.Tables[0].Rows[0][11].ToString();
                txtcntry.Text = ds.Tables[0].Rows[0][12].ToString();
                txtpincode.Text = ds.Tables[0].Rows[0][13].ToString();
                Image1.ImageUrl = ds.Tables[0].Rows[0][14].ToString();
                trlogindet.Visible = false;
                btnregister.Visible = false;
                btnUpdate.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);
        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            string voterid = row.Cells[1].Text;

            string q = "Select Id, FName, MName, LName, DOB, Age, MobileNo, EmailId, RmNoBldg, StreetArea, City, State, Country, Pincode, photo  from Voters where id='" + voterid + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbid.Text = ds.Tables[0].Rows[0][0].ToString();
                lbname.Text = ds.Tables[0].Rows[0][1].ToString()+" "+ ds.Tables[0].Rows[0][2].ToString() +" "+ ds.Tables[0].Rows[0][3].ToString();
                string d = ds.Tables[0].Rows[0][4].ToString();
                DateTime date = Convert.ToDateTime(d);
                lbdob.Text = date.ToString("yyyy-MM-dd");
                lbage.Text = ds.Tables[0].Rows[0][5].ToString();
                lbmobile.Text = ds.Tables[0].Rows[0][6].ToString();
                lbemail.Text = ds.Tables[0].Rows[0][7].ToString();
                lbrmbldg.Text = ds.Tables[0].Rows[0][8].ToString();
                lbstrarea.Text = ds.Tables[0].Rows[0][9].ToString();
                lbcity.Text = ds.Tables[0].Rows[0][10].ToString();
                lbstate.Text = ds.Tables[0].Rows[0][11].ToString();
                lbcntry.Text = ds.Tables[0].Rows[0][12].ToString();
                lbpincode.Text = ds.Tables[0].Rows[0][13].ToString();
                Image2.ImageUrl = ds.Tables[0].Rows[0][14].ToString();
               
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#viewModal').modal({backdrop: 'static', keyboard: false},'show')", true);
        }

        protected void lbtndelete_Command(object sender, CommandEventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            string voterid = row.Cells[1].Text;

            string argument = e.CommandArgument.ToString();
            if (argument.Equals("delete"))
            {
                con.Open();
                string q = "Delete from Voters where Id = '" + voterid + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Voter Details Deleted !!!')", true);
                GvBind();

            }
        }


        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            string q = "Select Id, Concat(FName,' ',MName,' ',LName) as Name, DOB, Age, photo  from Voters where FName like '%" + txtsearch.Text + "%' and MName like '%" + txtsearch.Text + "%' and LName like '%" + txtsearch.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                voterGrid.Visible = true;
                lbnodata.Visible = false;
            }
            else
            {
                q = "Select Id, Concat(FName,' ',MName,' ',LName) as Name, DOB, Age, photo  from Voters where Id='" + txtsearch.Text + "'";
                da = new SqlDataAdapter(q, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    voterGrid.Visible = true;
                    lbnodata.Visible = false;
                }
                else
                {
                    voterGrid.Visible = false;
                    lbnodata.Visible = true;
                }
            }
        }

        protected void btnregister_Click(object sender, EventArgs e)
        {
            int age = Convert.ToInt32(txtage.Text);

            DateTime date = Convert.ToDateTime(txtdob.Text);
            string dob = date.ToString("yyyy/MM/dd");
            dob = dob.Replace('-', '/');

            string now = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime d = Convert.ToDateTime(now);
           
            if (date > d || date == d)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Date of Birth')", true);
            }
            else if (txtage.Text.Contains('-') || age < 18)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Voter age')", true);
            }
            else
            {
                con.Open();
                string q = "Insert into Voters(Id, FName, MName, LName, DOB, Age, RmNoBldg, StreetArea, City, State, Country, Pincode, MobileNo, EmailId, Username, Password,photo) values('" + txtid.Text + "','" + txtfname.Text + "','" + txtmname.Text + "','" + txtlname.Text + "','" + dob + "','" + txtage.Text + "','" + txtrmbldg.Text + "','" + txtstrarea.Text + "','" + txtcity.Text + "','" + txtstate.Text + "','" + txtcntry.Text + "','" + txtpincode.Text + "','" + txtmobno.Text + "','" + txtemail.Text + "','" + txtusernm.Text + "','" + txtpass.Text + "','" + Image1.ImageUrl + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Voter Registered Successfully!!!')", true);
                id = Convert.ToInt32(txtid.Text);
                id++;
                txtid.Text = id.ToString();
                GvBind();

            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int age = Convert.ToInt32(txtage.Text);

            DateTime date = Convert.ToDateTime(txtdob.Text);
            string dob = date.ToString("yyyy/MM/dd");
            dob = dob.Replace('-', '/');

            string now = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime d = Convert.ToDateTime(now);

            if (date > d || date == d)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Date of Birth')", true);
            }
            else if (txtage.Text.Contains('-') || age < 18)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Voter age')", true);
            }
            else
            {
                con.Open();
                string q = "Update Voters set FName='" + txtfname.Text + "', MName='" + txtmname.Text + "', LName='" + txtlname.Text + "', DOB='" + dob + "', Age='" + txtage.Text + "', RmNoBldg='" + txtrmbldg.Text + "', StreetArea='" + txtstrarea.Text + "', City='" + txtcity.Text + "', State='" + txtstate.Text + "', Country='" + txtcntry.Text + "', Pincode='" + txtpincode.Text + "', MobileNo='" + txtmobno.Text + "', EmailId='" + txtemail.Text + "',photo='" + Image1.ImageUrl + "' Where Id='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Voter Details Updated Successfully!!!')", true);

                GvBind();

            }
        }

        protected void txtusernm_TextChanged(object sender, EventArgs e)
        {
            string q = "Select * from Voters where Username='" + txtusernm.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Username already exis. Please enter new one.')", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);
        }

        protected void lbtnimgupload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string file = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (file.Contains(".jpg") || file.Contains(".jpeg") || file.Contains(".png"))
                {
                    //Save images into Images folder
                    FileUpload1.SaveAs(Server.MapPath("~\\Images\\" + file));
                    string path = Server.MapPath("~\\Images\\" + file);
                    FileUpload1.SaveAs(path + file);
                    Image1.ImageUrl = "~/Images/" + file;
                    lbimgname.Text = Image1.ImageUrl;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Please upload Image in JPEG or PNG format')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Please select Image File')", true);
            }
        }


    }
}