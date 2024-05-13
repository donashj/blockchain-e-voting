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
    public partial class Home : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Login"] = "Login";
            if (!IsPostBack)
            {
                DateTime d1 = DateTime.Now;
                d1 = d1.AddDays(-1);                
                string date = d1.ToString("yyyy-MM-dd");
                date = date.Replace('-', '/');

                string q = "Select EId from Election where VoteDate='" + date + "' or VoteDate>'" + date + "'";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int c = ds.Tables[0].Rows.Count;
                if (c > 0)
                {
                    for(int i = 0; i < c; i++)
                    {
                        string eid = ds.Tables[0].Rows[0][i].ToString();
                        string qu = "select CId from Vote where EId='" + eid + "' group by CId order by count(CId) desc ";
                        SqlDataAdapter sda = new SqlDataAdapter(qu,con);
                        DataSet dss = new DataSet();
                        sda.Fill(dss);
                        int cnt = dss.Tables[0].Rows.Count;
                        if (cnt > 0)
                        {
                            string cid = dss.Tables[0].Rows[0][0].ToString();
                            con.Open();
                            string que = "Update Election set Result='" + cid + "', Status='Declared' where EId='" + eid + "'";
                            SqlCommand cmd = new SqlCommand(que, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con.Open();
                            string que = "Update Election set Result='Tie', Status='Declared' where EId='" + eid + "'";
                            SqlCommand cmd = new SqlCommand(que, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                    }
                }
            }
        }
    }
}