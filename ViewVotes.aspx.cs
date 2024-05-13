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
using System.Security.Cryptography;
using System.IO;

namespace Project
{
    public partial class ViewVotes : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GvBind();
            }
        }

        protected void GvBind()
        {
            string q = "Select Distinct EId from Vote ";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int c = ds.Tables[0].Rows.Count;
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    string eid = ds.Tables[0].Rows[i][0].ToString();
                    Session["EId"] = eid;
                    string qu = "Select * from Vote where EId='" + eid + "'";
                    SqlDataAdapter da1 = new SqlDataAdapter(qu, con);
                    DataTable dt = new DataTable();
                    da1.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("isManupilated");
                        List<VoteList> blocklist = getBlockList();
                        for (int z = 0; z < blocklist.Count; z++)
                        {
                            VoteList block1 = blocklist[z];
                            DataRow row1 = dt.Rows[z];

                            Block block = new Block();
                            block.gethash(new string[] { block1.id, block1.voterid, block1.cid, block1.eid, block1.election, block1.vname }, block1.datetime, block1.previoushash);
                            string block1hash = block.getFinalBlock();

                            if (block1hash == block1.block)
                            {
                                row1["isManupilated"] = "No";
                                
                            }
                            else
                            {
                                row1["isManupilated"] = "Yes";
                            }

                        }
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        voteGrid.Visible = true;
                        lbnodata.Visible = false;
                    }
                    else
                    {
                        voteGrid.Visible = false;
                        lbnodata.Visible = true;
                    }
                }
            }
        }


        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            string qu = "Select * from Vote where EId='" + txtsearch.Text + "' or Election = '" + txtsearch.Text + "'";
            SqlDataAdapter da1 = new SqlDataAdapter(qu, con);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("isManupilated");
                List<VoteList> blocklist = getSearchBlockList();
                for (int z = 0; z < blocklist.Count; z++)
                {
                    VoteList block1 = blocklist[z];
                    DataRow row1 = dt.Rows[z];

                    Block block = new Block();
                    block.gethash(new string[] { block1.id, block1.voterid, block1.cid, block1.eid, block1.election, block1.vname }, block1.datetime, block1.previoushash);
                    string block1hash = block.getFinalBlock();

                    if (block1hash == block1.block)
                    {
                        row1["isManupilated"] = "No";

                    }
                    else
                    {
                        row1["isManupilated"] = "Yes";
                    }

                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                voteGrid.Visible = true;
                lbnodata.Visible = false;
            }
            else
            {
                voteGrid.Visible = false;
                lbnodata.Visible = true;
            }
        }


        public List<VoteList> getBlockList()
        {
            string eleid = Session["EId"].ToString();
            List<VoteList> blocklist = new List<VoteList>();
            DataTable ds = new DataTable();

            string q = "select * from Vote Where EId='" + eleid + "' ";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            ds = new DataTable();
            da.Fill(ds);
            int count = ds.Rows.Count;
            for (int i = 0; i < count; i++)
            {

                VoteList bl = new VoteList();
                bl.id = ds.Rows[i][0].ToString();
                bl.voterid = ds.Rows[i][1].ToString();
                bl.cid = ds.Rows[i][2].ToString();
                bl.eid = ds.Rows[i][3].ToString();
                bl.election = ds.Rows[i][4].ToString();
                bl.vname = ds.Rows[i][5].ToString();
                bl.datetime = ds.Rows[i][6].ToString();
                bl.previoushash = ds.Rows[i][7].ToString();
                bl.block = ds.Rows[i][8].ToString();
                blocklist.Add(bl);
            }

            return blocklist;
        }

        public List<VoteList> getSearchBlockList()
        {
            string eleid = Session["EId"].ToString();
            List<VoteList> blocklist = new List<VoteList>();
            DataTable ds = new DataTable();

            string q = "select * from Vote Where EId='" + txtsearch.Text + "' or Election = '" + txtsearch.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            ds = new DataTable();
            da.Fill(ds);
            int count = ds.Rows.Count;
            for (int i = 0; i < count; i++)
            {

                VoteList bl = new VoteList();
                bl.id = ds.Rows[i][0].ToString();
                bl.voterid = ds.Rows[i][1].ToString();
                bl.cid = ds.Rows[i][2].ToString();
                bl.eid = ds.Rows[i][3].ToString();
                bl.election = ds.Rows[i][4].ToString();
                bl.vname = ds.Rows[i][5].ToString();
                bl.datetime = ds.Rows[i][6].ToString();
                bl.previoushash = ds.Rows[i][7].ToString();
                bl.block = ds.Rows[i][8].ToString();
                blocklist.Add(bl);
            }

            return blocklist;
        }

        public class Block
        {
            byte[] Entities;
            byte[] prevhash;
            byte[] Datetime;
            byte[] Finalhashblock = null;

            public byte[] gethash(string[] entities, string datetime, string prevhashblock)
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