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
    public partial class Result : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string q = "Select Distinct Election,EId,VoteDate from Election where Status='Declared'";
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
            }
        }

        protected void lbtnsearch_Click(object sender, EventArgs e)
        {
            string q = "Select Candidate1,Candidate2,Candidate3,Candidate4,EId,VoteDate,Result from Election where Election='" + ddlelections.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int c = ds.Tables[0].Rows.Count;
            if (c > 0)
            {
                string cnid1 = ds.Tables[0].Rows[0][0].ToString();
                string cnid2 = ds.Tables[0].Rows[0][1].ToString();
                string cnid3 = ds.Tables[0].Rows[0][2].ToString();
                string cnid4 = ds.Tables[0].Rows[0][3].ToString();
                lbeid.Text = ds.Tables[0].Rows[0][4].ToString();
                lbedate.Text = ds.Tables[0].Rows[0][5].ToString();
                string wincan = ds.Tables[0].Rows[0][6].ToString();

                string qu = "Select  Photo, Name, Age, Qualification from Candidates where CId='" + cnid1 + "'";
                da = new SqlDataAdapter(qu, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ImgCan1.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                    lbname1.Text = ds.Tables[0].Rows[0][1].ToString(); ;
                    
                }

                qu = "Select  Photo, Name, Age, Qualification from Candidates where CId='" + cnid2 + "'";
                da = new SqlDataAdapter(qu, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ImgCan2.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                    lbname2.Text = ds.Tables[0].Rows[0][1].ToString(); ;
                    
                }

                qu = "Select  Photo, Name, Age, Qualification from Candidates where CId='" + cnid3 + "'";
                da = new SqlDataAdapter(qu, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ImgCan3.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                    lbname3.Text = ds.Tables[0].Rows[0][1].ToString(); ;
                    
                }

                qu = "Select  Photo, Name, Age, Qualification from Candidates where CId='" + cnid4 + "'";
                da = new SqlDataAdapter(qu, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ImgCan4.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                    lbname4.Text = ds.Tables[0].Rows[0][1].ToString(); ;
                   
                }

                qu = "Select  Photo, Name, Age, Qualification from Candidates where CId='" + wincan + "'";
                da = new SqlDataAdapter(qu, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ImgWinner.ImageUrl = ds.Tables[0].Rows[0][0].ToString();
                    lbwinnernm.Text = ds.Tables[0].Rows[0][1].ToString(); ;

                }

                candsDiv.Visible = true;
            }
        }
    }
}