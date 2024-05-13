using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Project
{
    public partial class Vote : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);
        string prevHash, voterid,Id,voternm;
        int voteid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Voter"] != null)
            {
                voterid = Session["Voter"].ToString();
                lbvid.Text = voterid.Split(',')[0];
                lbvname.Text = voterid.Split(',')[1];
            }
            else
            {
                voterid = lbvid.Text;
                voternm = lbvname.Text;
            }
            if (!IsPostBack)
            {               
                string q = "Select Distinct Election,EId,VoteDate from Election";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int c = ds.Tables[0].Rows.Count;
                if (c > 0)
                {
                    ddlelections.Items.Clear();
                    ddlelections.Items.Add("Select Election");
                    for (int i = 0; i < c; i++)
                    {
                        string ename = ds.Tables[0].Rows[i][0].ToString();
                        ddlelections.Items.Add(ename);
                    }
                }               

                SetId();

            }
        }

        protected void SetId()
        {
            string q = "Select Top 1 Id from Vote order by Id desc";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Id = ds.Tables[0].Rows[0][0].ToString();
                voteid = Convert.ToInt32(Id);
                voteid++;
                lbvoteid.Text = voteid.ToString();
            }
            else
            {
                lbvoteid.Text = "8001";
            }

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox4.Checked = false;
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
        }

        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            string q = "Select * from Vote where VoterId = '" + lbvid.Text + "' and EID = (Select EId from Election where Election='" + ddlelections.SelectedValue + "')";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                candsDiv.Visible = false;
                voteSuccDiv.Visible = true;
                electndateDiv.Visible = false;
            }
            else
            {
                q = "Select Candidate1,Candidate2,Candidate3,Candidate4,EId,VoteDate from Election where EID = (Select EId from Election where Election='" + ddlelections.SelectedValue + "')";
                da = new SqlDataAdapter(q, con);
                ds = new DataSet();
                da.Fill(ds);

                string edate = ds.Tables[0].Rows[0][5].ToString();
                DateTime ed = Convert.ToDateTime(edate);
                lbedate.Text = edate;
                DateTime d = DateTime.Now;
                string dnow = d.ToString("yyyy-MM-dd");
                dnow = dnow.Replace('-', '/');
                if (dnow == edate)
                {
                    string cnid1 = ds.Tables[0].Rows[0][0].ToString();
                    string cnid2 = ds.Tables[0].Rows[0][1].ToString();
                    string cnid3 = ds.Tables[0].Rows[0][2].ToString();
                    string cnid4 = ds.Tables[0].Rows[0][3].ToString();
                    lbeid.Text = ds.Tables[0].Rows[0][4].ToString();


                    string qu = "Select  Photo, Name, CId from Candidates where CId='" + cnid1 + "'";
                    da = new SqlDataAdapter(qu, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ImgCan1.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                        lbname1.Text = ds.Tables[0].Rows[0][1].ToString();
                        lbcid1.Text = ds.Tables[0].Rows[0][2].ToString();
                    }

                    qu = "Select  Photo, Name, CId from Candidates where CId='" + cnid2 + "'";
                    da = new SqlDataAdapter(qu, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ImgCan2.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                        lbname2.Text = ds.Tables[0].Rows[0][1].ToString();
                        lbcid2.Text = ds.Tables[0].Rows[0][2].ToString();

                    }

                    qu = "Select  Photo, Name, CId from Candidates where CId='" + cnid3 + "'";
                    da = new SqlDataAdapter(qu, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ImgCan3.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                        lbname3.Text = ds.Tables[0].Rows[0][1].ToString();
                        lbcid3.Text = ds.Tables[0].Rows[0][2].ToString();

                    }

                    qu = "Select  Photo, Name, CId from Candidates where CId='" + cnid4 + "'";
                    da = new SqlDataAdapter(qu, con);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ImgCan4.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                        lbname4.Text = ds.Tables[0].Rows[0][1].ToString();
                        lbcid4.Text = ds.Tables[0].Rows[0][2].ToString();

                    }
                    candsDiv.Visible = true;
                }
                else if (ed > d)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Check the date for Election ')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Election Voting is done. ')", true);
                }
            }
        }
    

        protected void btnfinish_Click(object sender, EventArgs e)
        {
            if (CheckBox1.Checked || CheckBox2.Checked || CheckBox3.Checked || CheckBox4.Checked)
            {
                string cndid="";

                if (CheckBox1.Checked)
                {
                    cndid = lbcid1.Text;
                }
                else if (CheckBox2.Checked)
                {
                    cndid = lbcid2.Text;
                }
                else if (CheckBox3.Checked)
                {
                    cndid = lbcid3.Text;
                }
                else if (CheckBox4.Checked)
                {
                    cndid = lbcid4.Text;
                }
                DateTime d = DateTime.Now;
                string date = d.ToString("yyyy/MM/dd");
                date = date.Replace('-', '/');

                string qu = "select Id, PrevHash,CurrHash from Vote where Election = '" + ddlelections.SelectedValue + "' order by Id desc";
                SqlDataAdapter sda = new SqlDataAdapter(qu, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int c = ds.Tables[0].Rows.Count;
                if (c > 0)
                {
                    prevHash = ds.Tables[0].Rows[0][2].ToString();
                }
                else
                {
                    prevHash = "Genesis";
                }

                VoteList bl = new VoteList();
                bl.id = lbvoteid.Text;
                bl.voterid = lbvid.Text;
                bl.cid = cndid;
                bl.eid = lbeid.Text;
                bl.election = ddlelections.SelectedValue;
                bl.vname = lbvname.Text;
                bl.datetime = date;
                bl.previoushash = prevHash;

                Block block = new Block();
                block.gethash(new string[] { bl.id, bl.voterid, bl.cid , bl.eid, bl.election, bl.vname }, bl.datetime, bl.previoushash);
                string Currentblock = block.getFinalBlock();
                bl.block = Currentblock;

                con.Open();
                qu = "Insert into Vote(Id,VoterId,CId,EId,Election,Vname,Datetime,PrevHash,CurrHash) values('" + lbvoteid.Text + "','" + lbvid.Text + "','" + cndid + "','" + lbeid.Text + "','" + ddlelections.SelectedValue + "','" + lbvname.Text + "','" + date + "','" + prevHash + "','" + Currentblock + "')";
                SqlCommand cmd = new SqlCommand(qu, con);
                cmd.ExecuteNonQuery();
                
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Your vote cast succesfully. Thank You!!!')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Cast your vote to any one of the candidate ')", true);
            }
        }


        public class Block
        {
            byte[] Entities;
            byte[] prevhash;
            byte[] Datetime;           
            byte[] Finalhashblock = null;

            public byte[] gethash(string[] entities,  string datetime, string prevhashblock)
            {
                string finaldata = "";
                foreach (string s in entities)
                {
                    finaldata += s;
                }

                Entities = Encoding.Default.GetBytes(finaldata);
                Datetime = Encoding.Default.GetBytes(datetime);
                prevhash = Encoding.Default.GetBytes(prevhashblock);

                using (SHA512 sha = new SHA512Managed())
                using (MemoryStream st = new MemoryStream())
                using (BinaryWriter bw = new BinaryWriter(st))
                {
                    bw.Write(Entities);
                    bw.Write(prevhash);
                    bw.Write(Datetime);
                    var finalblock = st.ToArray();
                    Finalhashblock = sha.ComputeHash(finalblock);
                    return Finalhashblock;
                }
            }

            public string getFinalBlock()
            {
                if (Finalhashblock != null)
                {
                    return BitConverter.ToString(Finalhashblock).Replace("-", "");
                }
                else
                {
                    return "Not Defined";
                }
            }


        }
    }
}