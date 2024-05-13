using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using RestSharp;

namespace Project
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Login"] = "Login";
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string q = "Select * from Admin where Username='" + txtUsername.Text + "' and Password = '" + txtpass.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["Login"] = "Admin";
                Response.Redirect("ManageVoters.aspx");
            }
            else
            {
                q = "Select Id, Concat(FName,' ',MName,' ',LName) as Name, MobileNo from Voters where Username='" + txtUsername.Text + "' and Password = '" + txtpass.Text + "'";
                da = new SqlDataAdapter(q, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {                                       
                    string mobileno = ds.Tables[0].Rows[0][2].ToString();
                    Random r = new Random();
                    int un = r.Next(1111, 9999);
                    string otp = un.ToString();
                    hdfotp.Value = otp;

                    string message = "OTP for Verification is." + otp;
                    var client = new RestClient("https://www.fast2sms.com/dev/bulkV2");
                    var request = new RestRequest();
                    request.AddHeader("authorization", "a6J3VZCHxfbN7QXcoejlBdpYW1TzAkFDyuq0vSgnGKR58rshEtAnmZWF35r92fK6sNQpvMiPSVkjCXTY");
                    request.AddHeader("Content-Type", "application/json");
                    var body = @"{
                    " + "\n" +
                    @"""route"" : ""q""," + "\n" +
                    @"""message"":""" + message + "\"" + "," + "\n" +
                    @"""flash"" : 0," + "\n" +
                    @"""numbers"":"" " + mobileno + " \"" + "\n" +

                    @"}";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    request.AddJsonBody(body);
                    RestResponse response = client.Post(request);
                    string res = response.StatusCode.ToString();

                    if (res == "OK")
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "msg", "alert('OTP send Successfully!!! Check SMS')", true);

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "msg", "alert('OTP Send Successfully but Some Error occured while sending SMS!!!')", true);

                    }

                    Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('OTP Send On Your Registered Mobile No. for Verification')", true);
                    otpDiv.Visible = true;
                    tbllogin.Visible = false;
                    Session["Login"] = "User";
                    Session["Voter"] = ds.Tables[0].Rows[0][0].ToString() + "," + ds.Tables[0].Rows[0][1].ToString();                    
                
                }
                else
                {
                    Session["Login"] = "User";
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid Login Credentials')", true);
                }
            }
        }

        protected void btnverify_Click(object sender, EventArgs e)
        {
            if (txtotp.Text == hdfotp.Value)
            {
                Response.Redirect("Demo.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgtype", "alert('Invalid OTP')", true);
            }
        }

    }
}