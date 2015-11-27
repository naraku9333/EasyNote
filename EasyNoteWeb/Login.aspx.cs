using System;
using System.Web;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void pbLoginBtn_Click(object sender, EventArgs e)
    {
        pnlLogin.Visible = true;
        pnlRegister.Visible = false;
        HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('This the Login page')</SCRIPT>");
    }
    public void pbRegisterBtn_Click(object sender, EventArgs e)
    {
        pnlLogin.Visible = false;
        pnlRegister.Visible = true;

    }
}