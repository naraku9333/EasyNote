<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lbTitle" runat="server" Text="Title"></asp:Label>
        <asp:TextBox ID="tbTitle" runat="server" Width="349px"></asp:TextBox>
&nbsp;<div>
    
                    <asp:Label ID="lbTags" runat="server" Text="Tags"></asp:Label>
                    <asp:TextBox ID="tbTags" runat="server" Width="342px"></asp:TextBox>
    
    </div>
        <asp:Label ID="lbText" runat="server" Text="Text"></asp:Label>
        <asp:TextBox ID="tbBody" runat="server" Height="131px" Width="348px"></asp:TextBox>
        <div style="width: 340px">
            <asp:Label ID="lbSearch" runat="server" Text="Search"></asp:Label>
            <asp:TextBox ID="tbSearch" runat="server" Width="290px"></asp:TextBox>
        <asp:Button ID="pbSearch" runat="server" Text="Search" Height="20px" OnClick="pbSearch_Click" Width="51px" />
            <asp:Button ID="pbClear" runat="server" Height="19px" OnClick="pbClear_Click" Text="Clear" />
            <asp:Label ID="lbFound" runat="server"></asp:Label>
        </div>
        <asp:Button ID="pbSaveBttn" runat="server" OnClick="pbSaveBttn_Click" Text="Save" Visible="False" />
        <asp:Button ID="pbAddNote" runat="server"  Text="Add" OnClick="pbAddNote_Click" />
        <asp:Button ID="pbRetrieveBttn" runat="server" Text="Retrieve" Visible="False" OnClick="pbRetrieveBttn_Click" />
        <asp:FileUpload ID="UploadAttachment" runat="server" />
        <asp:Button ID="pbAttachBtn" runat="server" Text="Attach" OnClick="pbAttachBtn_Click" />
        <asp:Button ID="pbDeleteBttn" runat="server" OnClick="pbDeleteBttn_Click" Text="Delete" Visible="False" />
        <asp:Button ID="pbCancelBttn" runat="server" Text="Cancel" Visible="False" />
        <asp:Button ID="pbClearBtn" runat="server" Text="Clear" Visible="False" />
        <asp:Button ID="pbExit" runat="server" Text="Exit" />
                    <asp:GridView ID="dgvNotesList" runat="server" AllowSorting="True" 
                        onselectedindexchanged="dgvNotesList_SelectedIndexChanged" 
                        onsorting="dgvNotesList_Sorting" Width="389px" AutoGenerateSelectButton="True">
                    </asp:GridView>
        <asp:HiddenField ID="NoteField" runat="server" />
    </form>
</body>
</html>
