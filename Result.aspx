<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Project.Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <div class="card" style="height:600px;width:94%; margin:5% 0% 0% 3%; background-color:white;opacity:0.9;overflow-y:scroll;overflow-x:hidden">
        <center>
            <h1 style="font-family:'Copperplate Gothic';font-weight:bolder">Result</h1><br /><br />
           
            <div class="row" style="margin: 0%;width: 85%;margin-left: 40%;">                
                <div class="col-lg-2">
                    <asp:DropDownList ID="ddlelections" runat="server" CssClass="form-control" Width="250px"></asp:DropDownList>                    
                </div>
                <div class="col-lg-2" align="center">
                    <asp:LinkButton ID="lbtnsearch" runat="server" CssClass="btn btn-primary" OnClick="lbtnsearch_Click"><span class="glyphicon glyphicon-search" style="color:white; font-size:medium; font-weight:bold"></span></asp:LinkButton>
                </div>
                <div class="col-lg-2" align="center"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ControlToValidate="ddlelections" Display="Dynamic" ValidationGroup="check" InitialValue="Select Election" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
            </div>
            <br /><br />
            <div class="row" id="candsDiv" runat="server" visible="false">
                <div>
                    <table style="width:20%;margin-left: 5%;">
                        <tr>
                            <td align="left"><asp:Label ID="Label8" runat="server" Text="Election-Id :" Font-Bold="true" Font-Size="Large"></asp:Label></td>
                            <td align="left"><asp:Label ID="lbeid" runat="server" Text="" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr><td colspan="2">&nbsp;</td></tr>                  
                        <tr>
                            <td align="left"><asp:Label ID="Label2" runat="server" Text="Election-Date :" Font-Bold="true" Font-Size="Large"></asp:Label></td>
                            <td align="left"><asp:Label ID="lbedate" runat="server" Text="" Font-Size="Medium"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <hr style="border: 1px ridge black;"/>
                <div class="col-lg-9" style="border-right: 1px ridge black;margin-top:-1%">                                    
                       <h3 style="font-family:'Copperplate Gothic';font-weight:bolder">Candidates </h3><br />

                        <div align="center" class="row" style="width:88%;margin: 2%;">                
                            <div class="col-lg-2">
                                <asp:Image ID="ImgCan1" runat="server" style="width:100px;height:100px" />
                            </div>
                            <div class="col-lg-4">
                       
                                <table>
                                    <tr>
                                        <td align="left"><asp:Label ID="label21" runat="server" Text="Name :" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                        <td align="left"><asp:Label ID="lbname1" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                    </tr>
                           
                                </table>                                                 
                            </div>

                            <div class="col-lg-2">
                                <asp:Image ID="ImgCan2" runat="server" style="width:100px;height:100px" />
                            </div>
                            <div class="col-lg-4">
                       
                                <table>
                                    <tr>
                                        <td align="left"><asp:Label ID="label5" runat="server" Text="Name :" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                        <td align="left"><asp:Label ID="lbname2" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                    </tr>
                           
                                </table>                      
                            </div>                                  
                        </div>
                        <br />
                        <div class="row" style="width:90%;margin-left: 0%;">                
                            <div class="col-lg-2">
                                <asp:Image ID="ImgCan3" runat="server" style="width:100px;height:100px" />
                            </div>
                            <div class="col-lg-4">
                        
                                <table>
                                    <tr>
                                        <td align="left"><asp:Label ID="label9" runat="server" Text="Name :" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                        <td align="left"><asp:Label ID="lbname3" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                    </tr>
                            
                                </table>                        
                            </div>
                            <div class="col-lg-2">
                                <asp:Image ID="ImgCan4" runat="server" style="width:100px;height:100px" />
                            </div>
                            <div class="col-lg-4">                       
                                <table>
                                    <tr>
                                        <td align="left"><asp:Label ID="label12" runat="server" Text="Name :" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                        <td align="left"><asp:Label ID="lbname4" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                                    </tr>
                            
                                </table>                     
                            </div>                                  
                        </div>                    
                </div>
            
                <div class="col-lg-3" style="margin-top: -1%;">
                    <h3 style="font-family:'Copperplate Gothic';font-weight:bolder">Winner Candidate </h3><br />
                    <table>
                        <tr>
                            <td align="center"><asp:Image ID="ImgWinner" runat="server" style="width:100px;height:100px" /></td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td><asp:Label ID="lbwinnernm" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>
        </center>
    </div>
</asp:Content>
