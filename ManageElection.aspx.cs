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
    public partial class ManageElection : System.Web.UI.Page
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
            string q = "Select Top 1 EId from Election order by EId desc";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                vid = ds.Tables[0].Rows[0][0].ToString();
                id = Convert.ToInt32(vid);
                id++;
                txteid.Text = id.ToString();
            }
            else
            {
                txteid.Text = "1001";
            }
        }

        protected void GvBind()
        {
            string q = "Select EId, Election, Votedate, Result,Status  from Election";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                electionGrid.Visible = true;
                lbnodata.Visible = false;
            }
            else
            {
                electionGrid.Visible = false;
                lbnodata.Visible = true;
            }
            
        }

        protected void lbtnaddnew_Click(object sender, EventArgs e)
        {
            SetId();
            txtelection.Text = "";
            txtelectndate.Text = "";
            btnelection.Visible = true;
            btnUpdate.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);
        }        

        protected void lbtnedit_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            txteid.Text = row.Cells[0].Text;
            txtelection.Text = row.Cells[1].Text;            
            DateTime d = Convert.ToDateTime(row.Cells[2].Text);
            txtelectndate.Text = d.ToString("yyyy-MM-dd");
            btnelection.Visible = false;
            btnUpdate.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#mngModal').modal({backdrop: 'static', keyboard: false},'show')", true);

        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            lbeid.Text = row.Cells[0].Text;
            lbelection.Text = row.Cells[1].Text;            
            DateTime d = Convert.ToDateTime(row.Cells[2].Text);
            lbeldt.Text = d.ToString("yyyy-MM-dd");
            lbresult.Text = row.Cells[3].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#viewModal').modal({backdrop: 'static', keyboard: false},'show')", true);

        }

        protected void lbtndelete_Command(object sender, CommandEventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            string eid = row.Cells[0].Text;

            string argument = e.CommandArgument.ToString();
            if (argument.Equals("delete"))
            {
                con.Open();
                string q = "Delete from Election where EId = '" + eid + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Election Details Deleted !!!')", true);
                GvBind();

            }
        }

        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            string q = "Select EId, Election, Votedate, Result, Status  from Election where Election='" + txtsearch.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                q = "Select EId, Election, Votedate, Result, Status  from Election where EId='" + txtsearch.Text + "'";
                da = new SqlDataAdapter(q, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    electionGrid.Visible = true;
                    lbnodata.Visible = false;
                }
            }
            else
            {
                electionGrid.Visible = false;
                lbnodata.Visible = true;
            }
        }

        protected void btnelection_Click(object sender, EventArgs e)
        {
           
            DateTime date = Convert.ToDateTime(txtelectndate.Text);
            string eldt = date.ToString("yyyy/MM/dd");
            eldt = eldt.Replace('-', '/');

            string now = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime d = Convert.ToDateTime(now);

            if (date < d || date == d)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Election Date')", true);
            }            
            else
            {
                con.Open();
                string q = "Insert into Election(EId,Election, VoteDate,Result,Status) values('" + txteid.Text + "','" + txtelection.Text + "','" + eldt + "','Not Declared','Not Declared')";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Election Added Successfully!!!')", true);
                id = Convert.ToInt32(txteid.Text);
                id++;
                txteid.Text = id.ToString();
                GvBind();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(txtelectndate.Text);
            string eldt = date.ToString("yyyy/MM/dd");
            eldt = eldt.Replace('-', '/');

            string now = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime d = Convert.ToDateTime(now);

            if (date < d || date == d)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Election Date')", true);
            }
            else
            {
                con.Open();
                string q = "Update Election set Election='" + txtelection.Text + "', VoteDate='" + eldt + "' where EId = '" + txteid.Text + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Election Updated Successfully!!!')", true);
                id = Convert.ToInt32(txteid.Text);
                id++;
                txteid.Text = id.ToString();
                GvBind();
            }
        }

        protected void lbtnmngcand_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            GridViewRow row = lbtn.NamingContainer as GridViewRow;
            lbeleId.Text = row.Cells[0].Text;
            string q = "Select CId, Voterid, Name, Photo,Age, Qualification  from Candidates";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();                             
            }           

            q = "Select Candidate1,Candidate2,Candidate3,Candidate4 from Election where EId='" + lbeleId.Text + "'";
            da = new SqlDataAdapter(q, con);
            ds = new DataSet();
            da.Fill(ds);
            int c = ds.Tables[0].Rows.Count;
            if (c > 0)
            {
                for(int i = 0; i < 4; i++)
                {
                    string cnid = ds.Tables[0].Rows[0][i].ToString();
                    foreach (GridViewRow gvrow in GridView2.Rows)
                    {
                        var checkbox = gvrow.FindControl("chkbSelect") as CheckBox;
                        
                        string cid = gvrow.Cells[1].Text;
                        if (cnid == cid)
                        {
                            checkbox.Checked = true;
                            break;
                        }
                        else
                        {
                           
                        }
                        
                    }
                }
            }
                

            mngcandDiv.Visible = true;
            mngelecDiv.Visible = false;
        }

        protected void lbtnfinalise_Click(object sender, EventArgs e)
        {
            
            string cid = "";
            foreach (GridViewRow gvrow in GridView2.Rows)
            {
                var checkbox = gvrow.FindControl("chkbSelect") as CheckBox;
                if (checkbox.Checked)
                {
                    cid += gvrow.Cells[1].Text+",";                                       
                }
            }
            string[] candId = cid.Split(',');
            con.Open();
            string q = "Update Election set Candidate1='" + candId[0] + "',Candidate2='" + candId[1] + "',Candidate3='" + candId[2] + "',Candidate4='" + candId[3] + "' where EId = '" + lbeleId.Text + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            mngcandDiv.Visible = false;
            mngelecDiv.Visible = true;
        }
    }
}