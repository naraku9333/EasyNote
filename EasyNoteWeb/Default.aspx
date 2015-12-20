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
        input.file:hover {
            background: url("images/Light Select Button.png") no-repeat;
        }
        .uploadButton input[type="file"] {
            cursor:pointer;
            position:absolute;
            top:3px;
            opacity:0;
            left: 2px;
            width: 235px;
            height: 24px;
            font-size: 0;
        }

        .uploadButton {
            position:relative;
            overflow:hidden;
            height:24px;
            cursor:pointer;
            width:80px;
            background:url('images/Dark Select Button.png');
            background-size: 100%;
            top: -100px;
            left: 38px;
        }
        .uploadButton:hover {
            /*opacity:1;*/   
            background:url('images/Light Select Button.png');
            background-size: 100%;
        }
        </style>
</head>

<body bgcolor="#0066CC" text="white">
    <center>
    <form id="form1" runat="server">
        <h1 style = "font-family: 'Vladimir Script'">Easy Note Web
        </h1>
        <div>
            <asp:Label style="padding-left:70%" ID="Label1" runat="server" Text="Signed in as  " Width="80px"></asp:Label>
            <asp:LinkButton ID="lbUser" ForeColor="White" OnClick="LinkButton_Click" runat="server">LinkButton</asp:LinkButton>
        </div>
        <asp:Label style="margin-right:.5em" ID="lbTitle" runat="server" Text="Title  " Font-Bold="True"></asp:Label>
        <asp:TextBox ID="tbTitle" runat="server" Width="349px"></asp:TextBox><br />
&nbsp;
        <div>
    
            <asp:Label style="margin-right:.5em" ID="lbTags" runat="server" Text="Tags" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="tbTags" runat="server" Width="347px"></asp:TextBox><br /><br />
        </div>
        <div>
            <asp:Label style="margin-right:.5em" ID="lbText" runat="server" Text="Text  " Font-Bold="True"></asp:Label>
            <asp:TextBox ID="tbBody" runat="server" Height="131px" Width="348px" TextMode="MultiLine"></asp:TextBox>
        </div>
        <br />

        <div style="width: 549px; height: 31px; margin-left: 0px;"><span>
            <asp:Label style="margin-right:.5em" ID="lbSearch" runat="server" Text="Search  " Font-Bold="True"></asp:Label>
            <asp:TextBox style="margin-right:.5em" ID="tbSearch" runat="server" Width="170px"></asp:TextBox>
            <asp:ImageButton style="margin-right:.5em" ID="pbSearch" runat="server" Height="24px" ImageUrl="~/images/Dark Search Button.png" Width="80px" 
                OnClick="pbSearch_Click" OnMouseOver="src='images/Light Search Button.png';" OnMouseOut="src='images/Dark Search Button.png';"/>
            <asp:ImageButton style="margin-right:.5em" ID="pbClearBtn" runat="server" Height="24px" ImageUrl="~/images/Dark Clear Button.png" 
                OnClick="pbClear_Click" Width="80px" OnMouseOver="src='images/Light Clear Button.png';" OnMouseOut="src='images/Dark Clear Button.png';"/>
            <asp:Label ID="lbFound" runat="server"></asp:Label></span>
        </div>
        <br />

        <div style="width: 354px; margin-left: 0px">
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbSaveBttn" runat="server" Height="24px" ImageUrl="~/images/Dark Save Button.png" Width="80px" 
                OnClick="pbSaveBttn_Click" Visible="False" OnMouseOver="src='images/Light Save Button.png';" OnMouseOut="src='images/Dark Save Button.png';"/>
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbAddNote" runat="server" Height="24px" ImageUrl="~/images/Dark Add Button.png" 
                OnClick="pbAddNote_Click" Width="80px" OnMouseOver="src='images/Light Add Button.png';" OnMouseOut="src='images/Dark Add Button.png';"/>
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbDeleteBttn" runat="server" Height="24px" ImageUrl="~/images/Dark Delete Button.png" 
                OnClick="pbDeleteBttn_Click" Visible="False" Width="80px" OnMouseOver="src='images/Light Delete Button.png';" OnMouseOut="src='images/Dark Delete Button.png';"/>
            <asp:ImageButton style="margin-right:.5em; " ID="pbCancelBttn" runat="server" Height="24px" ImageUrl="~/images/Dark Cancel Button.png" Width="80px" 
                OnClick="pbCancelBttn_Click" OnMouseOver="src='images/Light Cancel Button.png';" OnMouseOut="src='images/Dark Cancel Button.png';"/>
            <!--<div class="fileinputs">
	            <input ID="file" type="file" class="file" />
	            <div class="fakefile">
		           
		        </div>
            </div>-->
        </div>
     
        &nbsp;
        <div style="width: 357px; margin-left: 0px; height: 37px;">           
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbAttachBtn" runat="server" Height="24px" ImageUrl="~/images/Dark Attach Button.png" 
                OnClick="pbAttachBtn_Click" Width="80px" OnMouseOver="src='images/Light Attach Button.png';" OnMouseOut="src='images/Dark Attach Button.png';"/>                       
            <asp:ImageButton style="margin-right:.5em; float:left" ID="pbRetrieveBttn" runat="server" Height="24px" ImageUrl="~/images/Dark Retrieve Button.png" 
                OnClick="pbRetrieveBttn_Click" OnMouseOver="src='images/Light Retrieve Button.png';" OnMouseOut="src='images/Dark Retrieve Button.png';" Visible="False" Width="80px" />
           <!-- <span style="position:absolute">-->
                
                <!---->
            <div style="padding:100px;">
                <div id="UB" class="uploadButton">
                    <input name="UploadAttachment" id="UploadAttachment" type="file"/>   
                    <!--<asp:FileUpload class="file" Style="margin-right: .5em; width: 80px; height: 26px; left: -80px; cursor:pointer" ID="UploadAttachment" runat="server" 
                    OnMouseOver="src='images/Light Select Button.png';" OnMouseOut="src='images/Dark Select Button.png';"/>                     -->
                </div>
            </div>
            &nbsp;
            <asp:HiddenField ID="NoteField" runat="server" />
        </div>
        &nbsp;<!--</span>-->        

        <br />
        <br />
        <div style="width: 705px; margin-left: 0px">
            <asp:GridView bgcolor="#C0C0C0" text="black" ID="dgvNotesList" runat="server" AllowSorting="True" 
                onselectedindexchanged="dgvNotesList_SelectedIndexChanged" 
                onsorting="dgvNotesList_Sorting" Width="704px" AutoGenerateSelectButton="True" style="margin-left: 0px">
                <HeaderStyle BackColor="#565656" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="white" ForeColor="black" />
            </asp:GridView>
        </div>
        <br /><br />
    </form>
    </center>
</body>
<script type="text/javascript">
    (function () {
        (".uploadButton").mousemove(function (e) {
            var offL, offR, inpStart
            offL = (this).offset().left;
            offT = (this).offset().top;
            aaa = (this).find("input").width();
            (this).find("input").css({
                left: e.pageX - aaa - 30,
                top: e.pageY - offT - 10
            })
        });
    });
</script>
</html>
