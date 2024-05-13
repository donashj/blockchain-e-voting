using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Login"] != null)
            {
                string login = Session["Login"].ToString();
                if (login == "Admin")
                {
                    PAdmin.Visible = true;
                    PUser.Visible = false;
                    PDefault.Visible = false;
                    contentDiv.Style["background-image"] = Page.ResolveUrl("~/Images/back.jpg");
                    contentDiv.Style["background-image-size"] = "cover";
                    contentDiv.Style["background-repeat"] = "no-repeat";
                    contentDiv.Style["width"] = "100%";
                    contentDiv.Style["height"] = "780px";
                    contentDiv.Style["margin-top"] = "-7%";
                }
                else if (login == "User")
                {
                    PAdmin.Visible = false;
                    PUser.Visible = true;
                    PDefault.Visible = false;
                    contentDiv.Style["background-image"] = Page.ResolveUrl("~/Images/back.jpg");
                    contentDiv.Style["background-image-size"] = "cover";
                    contentDiv.Style["background-repeat"] = "no-repeat";
                    contentDiv.Style["width"] = "100%";
                    contentDiv.Style["height"] = "700px";
                    contentDiv.Style["margin-top"] = "-7%";

                }
                else if (login == "Demo")
                {
                    PAdmin.Visible = false;
                    PUser.Visible = false;
                    PDefault.Visible = true;
                    contentDiv.Style["background-image"] = Page.ResolveUrl("~/Images/back.jpg");
                    contentDiv.Style["background-image-size"] = "cover";
                    contentDiv.Style["background-repeat"] = "no-repeat";
                    contentDiv.Style["width"] = "100%";
                    contentDiv.Style["height"] = "700px";
                    contentDiv.Style["margin-top"] = "-7%";

                }
                else
                {
                    PAdmin.Visible = false;
                    PUser.Visible = false;
                    PDefault.Visible = true;
                    contentDiv.Style["background-image"] = Page.ResolveUrl("~/Images/loginback.jpg");
                    //contentDiv.Style["background-image-size"] = "cover";
                    contentDiv.Style["background-repeat"] = "no-repeat";
                    contentDiv.Style["width"] = "100%";
                    contentDiv.Style["height"] = "700px";
                    contentDiv.Style["margin-top"] = "-5%";
                }
            }
        }
    }
}