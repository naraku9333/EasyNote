<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Easy Note Web</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
        .style2
        {
            width: 851px;
        }
        .style3
        {
            height: 23px;
            width: 851px;
        }
        span{
			display: inline-block;
            margin-left: 0px;
            height: 29px;
        }
        ImageButton{
            margin-right:.5em;
        }
        div.fileinputs {
	        position: relative;
        }

        div.fakefile {
	        position: absolute;
	        top: 0px;
	        left: 0px;
	        z-index: 1;
        }

        input.file {
	        position: relative;
	        text-align: right;
	        -moz-opacity:0;
	        filter:alpha(opacity: 0);
	        opacity: 0;
	        z-index: 1;
            top: 1px;
            /*left: -77px;*/
            margin-top: 0px;
        }
        
        </style>
</head>

<body bgcolor="#0066CC">
    <center>
    <form id="form1" runat="server">
        <h1 style = "font-family: 'Vladimir Script'">Easy Note Web</h1>
        <asp:Label style="margin-right:.5em" ID="lbTitle" runat="server" Text="Title  " Font-Bold="True"></asp:Label>
        <asp:TextBox ID="tbTitle" runat="server" Width="349px"></asp:TextBox><br />
&nbsp;
        <div>
    
            <asp:Label style="margin-right:.5em" ID="lbTags" runat="server" Text="Tags" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="tbTags" runat="server" Width="347px"></asp:TextBox><br /><br />
        </div>
        <div>
            <asp:Label style="margin-right:.5em" ID="lbText" runat="server" Text="Text  " Font-Bold="True"></asp:Label>
            <asp:TextBox ID="tbBody" runat="server" Height="131px" Width="348px"></asp:TextBox>
        </div>
        <br />

        <div style="width: 549px; height: 31px; margin-left: 0px;"><span>
            <asp:Label style="margin-right:.5em" ID="lbSearch" runat="server" Text="Search  " Font-Bold="True"></asp:Label>
            <asp:TextBox style="margin-right:.5em" ID="tbSearch" runat="server" Width="290px"></asp:TextBox>
            <asp:ImageButton style="margin-right:.5em" ID="pbSearch" runat="server" Height="25px" ImageUrl="~/images/Light Search Button.png" Width="80px" OnClick="pbSearch_Click" />
            <asp:ImageButton style="margin-right:.5em" ID="pbClearBtn" runat="server" Height="25px" ImageUrl="~/images/Light Clear Button.png" OnClick="pbClear_Click" Width="80px" />
            <asp:Label ID="lbFound" runat="server"></asp:Label></span>
        </div>
        <br /><br />

        <div style="width: 358px; margin-left: 0px">
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbSaveBttn" runat="server" Height="25px" ImageUrl="~/images/Light Save Button.png" Width="80px" OnClick="pbSaveBttn_Click" Visible="False" />
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbAddNote" runat="server" Height="25px" ImageUrl="~/images/Light Add Button.png" OnClick="pbAddNote_Click" Width="80px" />
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbDeleteBttn" runat="server" Height="25px" ImageUrl="~/images/Light Delete Button.png" OnClick="pbDeleteBttn_Click" Visible="False" Width="80px" />
            <asp:ImageButton style="margin-right:.5em; " ID="pbCancelBttn" runat="server" Height="25px" ImageUrl="~/images/Light Cancel Button.png" Width="80px" OnClick="pbCancelBttn_Click" />
            <!--<div class="fileinputs">
	            <input ID="file" type="file" class="file" />
	            <div class="fakefile">
		           
		        </div>
            </div>-->
        </div>
        <br />
        <div style="width: 358px; margin-left: 0px">           
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbAttachBtn" runat="server" Height="25px" ImageUrl="~/images/Light Attach Button.png" OnClick="pbAttachBtn_Click" Width="80px" />                       
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbRetrieveBttn" runat="server" Height="25px" ImageUrl="~/images/Light Retrieve Button.png" OnClick="pbRetrieveBttn_Click" Visible="False" Width="80px" />
            <span style="position:absolute">
                <asp:ImageButton style="margin-right:.5em; float:left" ID="pbSelectBttn" runat="server" Height="25px" ImageUrl="~/images/Light Select Button.png" Width="80px" />
                <asp:FileUpload class="file" Style="margin-right: .5em; width: 80px; height: 26px; left: -80px;" ID="UploadAttachment" runat="server" />
            </span>
        </div>        

        <br /><br />
        <div style="width: 705px; margin-left: 0px">
            <asp:GridView bgcolor="#C0C0C0" ID="dgvNotesList" runat="server" AllowSorting="True" 
                onselectedindexchanged="dgvNotesList_SelectedIndexChanged" 
                onsorting="dgvNotesList_Sorting" Width="350px" AutoGenerateSelectButton="True">
            </asp:GridView>
        </div>
        <asp:HiddenField ID="NoteField" runat="server" />
        <br /><br />
    </form>
    </center>
</body>
</html>
