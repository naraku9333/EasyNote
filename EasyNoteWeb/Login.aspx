<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login to Easy Note</title>
 <style type="text/css">
        </style>
</head>
<body bgcolor="#0066CC" text="white">
    <center>
        <form id="form2" runat="server">
            <h1 style = "font-family: 'Vladimir Script'">Easy Note Web</h1>
            <h2>Please Login</h2>
            <div>
                <asp:Label style="margin-right:.4em" ID="lbTitle" runat="server" Text="User ID  " Font-Bold="True" Width="100px"></asp:Label>
                <asp:TextBox ID="tbUserID" runat="server" Width="349px"></asp:TextBox><br /><br />
            </div>
            <div>
                <asp:Label style="margin-right:.4em" ID="Label1" runat="server" Text="Password" Font-Bold="True" Width="100px"></asp:Label>
                <asp:TextBox ID="tbPassword" runat="server" Width="349px" TextMode="Password"></asp:TextBox><br />
            </div>
            <br />
            <asp:ImageButton style="margin-right:.5em" ID="pbLoginBtn" runat="server" Height="24px" ImageUrl="~/images/Dark Login Button.png" Width="80px" 
                OnClick="pbLoginBtn_Click" OnMouseOver="src='images/Light Login Button.png';" OnMouseOut="src='images/Dark Login Button.png';"/>
            <asp:Label style="margin-right:.5em" ID="Label2" runat="server" Text=" or " Font-Bold="True"></asp:Label>
             <asp:ImageButton style="margin-right:.5em" ID="pbRegisterBtn" runat="server" Height="24px" ImageUrl="~/images/Dark Register Button.png" 
                OnClick="pbRegisterBtn_Click" Width="80px" OnMouseOver="src='images/Light Register Button.png';" OnMouseOut="src='images/Dark Register Button.png';"/>
        </form>
    </center>
</body>
</html>
