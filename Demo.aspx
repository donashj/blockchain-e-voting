<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="Project.Demo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <div class="card" style="height:550px;width:94%; margin:5% 0% 0% 3%; background-color:white;opacity:0.9;overflow-y:scroll;overflow-x:hidden">        
        <center>
            <h1 style="font-family:'Copperplate Gothic';font-weight:bolder">Instructions & Demo </h1><br />
            <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-5">
                    <p align="left" style="font-size:large"><br />
                        Carefully Cast your vote and follow the procedure to go to Vote Page.<br />
                        <br />
                        1. Firstly, Login with your Login Credentials.<br />
                        2. Then Go to Vote Page, by Clicking on the Vote in Menu.<br />
                        3. Select the Election you are voting for.<br />
                        4. Candidates with their details will be display. Select Candidate u want to vote for.<br />
                        5. Once u finalize with candidate selection, Click on Finish Button.<br />
                        <br />
                        Message will popup that "Your Vote Cast Successfully!!!".<br />
                    </p>
                </div>
                <div class="col-lg-5">
                    <video width="550" height="500" controls autoplay style="margin-top: -18%;">
                      <source src="Images/How to vote.mp4" type="video/mp4">
                      Your browser does not support the video tag.
                    </video>
                </div>
                <div class="col-lg-1"></div>
            </div>
        </center>
    </div>
</asp:Content>
