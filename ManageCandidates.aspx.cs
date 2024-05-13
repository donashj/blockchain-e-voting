using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Project
{
    public partial class ManageCandidates : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);
        string cid;
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
            string q = "Select Top 1 CId from Candidates order by CId desc";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cid = ds.Tables[0].Rows[0][0].ToString();
                id = Convert.ToInt32(cid);
                id++;
                txtcid.Text = id.ToString();
            }
            else
            {
                txtcid.Text = "5001";
            }

        }

        protected void GvBind()
        {
            string q = "Select CId, Voterid, Name, Photo,Age, Qualification  from Candidates";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                candidateGrid.Visible = true;
                lbnodata.Visible = false;
            }
            else
            {
                candidateGrid.Visible = false;
                lbnodata.Visible = true;
            }
        }

        protected void lbtnaddnew_Click(object sender, EventArgs e)
        {
            SetId();
            txtvoterid.Text = "";
            txtname.Text = "";
            txtage.Text = "";
            txtqualification.Text = "";
            Imgphoto.ImageUrl = "";
            btnaddcand.Visible = true;
            btnUpdate.Visible = false;
            tblvoter.Visible = false;
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);
        }

        protected void txtvoterid_TextChanged(object sender, EventArgs e)
        {
            string q = "Select Concat(FName,' ',MName,' ',LName) as Name, Age, photo  from Voters where Id like '%" + txtvoterid.Text + "%'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtname.Text = ds.Tables[0].Rows[0][0].ToString();
                txtage.Text = ds.Tables[0].Rows[0][1].ToString();
                Imgphoto.ImageUrl = ds.Tables[0].Rows[0][2].ToString();
                tblvoter.Visible = true;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('This Voter-id is not registered')", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);

        }

        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            string q = "Select CId, Voterid, Name, Photo,Age, Qualification  from Candidates where Name like '%" + txtsearch.Text + "%' or Voterid = '" + txtsearch.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                candidateGrid.Visible = true;
                lbnodata.Visible = false;
            }
            else
            {
                q = "Select CId, Voterid, Name, Photo,Age, Qualification  from Candidates where CId = '" + txtsearch.Text + "'";
                da = new SqlDataAdapter(q, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    candidateGrid.Visible = true;
                    lbnodata.Visible = false;
                }
                else
                {
                    candidateGrid.Visible = false;
                    lbnodata.Visible = true;
                }
                
            }
        }

        protected void lbtnedit_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;     
            
            txtcid.Text = row.Cells[1].Text;
            txtvoterid.Text = row.Cells[2].Text;
            txtname.Text = row.Cells[3].Text;
            txtage.Text = row.Cells[4].Text;
            txtqualification.Text = row.Cells[5].Text;
            Imgphoto.ImageUrl = row.Cells[6].Text;
            tblvoter.Visible = true;
            btnUpdate.Visible = true;
            btnaddcand.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);

        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;

            lbcid.Text = row.Cells[1].Text;
            lbvoterid.Text = row.Cells[2].Text;
            lbname.Text = row.Cells[3].Text;
            lbage.Text = row.Cells[4].Text;
            lbqualification.Text = row.Cells[5].Text;
            Image1.ImageUrl = row.Cells[6].Text;           

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#viewModal').modal({backdrop: 'static', keyboard: false},'show')", true);

        }

        protected void lbtndelete_Command(object sender, CommandEventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            string cid = row.Cells[1].Text;

            string argument = e.CommandArgument.ToString();
            if (argument.Equals("delete"))
            {
                con.Open();
                string q = "Delete from Candidates where CId = '" + cid + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Candidates Details Deleted !!!')", true);
                GvBind();

            }
        }

        protected void btnaddcand_Click(object sender, EventArgs e)
        {
            con.Open();
            string q = "Insert into Candidates(CId, Voterid, Name, Age, Photo, Qualification) values('" + txtcid.Text + "','" + txtvoterid.Text + "','" + txtname.Text + "','" + txtage.Text + "','" + Imgphoto.ImageUrl + "','" + txtqualification.Text + "')";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();

            Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Candidate Details added Successfully!!!')", true);
            GvBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            string q = "Update  Candidates set  Qualification='" + txtqualification.Text + "' where CId='" + txtcid.Text + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();

            Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Candidate Details added Successfully!!!')", true);
            
            GvBind();
        }

        
    }
}