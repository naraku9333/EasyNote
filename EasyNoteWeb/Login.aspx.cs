using System;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    private string connString;

    /******************************Added for Assignment 5********************************
    * FUNCTION:  private void Page_Load(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   This function has no return value
    *
    * NOTES:     This function gets called when the page is loaded and sets the 
    *            connection string on first load
    **************************************************************************************/
    protected void Page_Load(object sender, EventArgs e)
    {
        connString = ConfigurationManager.ConnectionStrings["sqlserver"].ConnectionString;
    }

    /******************************Added for Assignment 5********************************
    * FUNCTION:  private void pbLoginBtn_Click(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   This function has no return value
    *
    * NOTES:     This function handles the Login button click.
    *            It checks the DB for the specified username, if found it verifies the 
    *            passwords match and alerts user if they don't. If user is not found
    *            user is shown the register interface.
    **************************************************************************************/
    public void pbLoginBtn_Click(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(connString))
        {
            con.Open();
            using (var com = new SqlCommand("select hashedpassword, salt from Customers where username=@user", con))
            {
                com.Parameters.AddWithValue("@user", tbUserID.Text);
                var reader = com.ExecuteReader();
                if(!reader.Read())
                {
                    HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('No user with ID, please register')</SCRIPT>");
                    pnlLogin.Visible = false;
                    pnlRegister.Visible = true;
                }
                else
                {
                    string salt = reader["salt"] as string;
                    string storedPW = reader["hashedpassword"] as string;

                    string providedPW = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword.Text + salt, "SHA1");
                    if(storedPW == providedPW)
                    {
                        Session["user"] = tbUserID.Text;
                        FormsAuthentication.RedirectFromLoginPage(tbUserID.Text, false);
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Invalid password!')</SCRIPT>");
                    }
                }
            }
        }                    
    }

    /******************************Added for Assignment 5********************************
    * FUNCTION:  private void pbRegisterBtn_Click(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   This function has no return value
    *
    * NOTES:     This function handles register button click.
    *            It gets the required information and stores it in the DB and redirects 
    *            user to the app.
    **************************************************************************************/
    public void pbRegisterBtn_Click(object sender, EventArgs e)
    {
        if(pnlLogin.Visible == true)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
        }        
        else
        {
            using (var con = new SqlConnection(connString))
            {
                con.Open();
                using (var com = new SqlCommand("insert into Customers values(@fn, @ln, @un, @hp, @salt, @enccc, @key, @iv)", con))
                {
                    string salt = SaltGeneratorService.GenerateSaltString();
                    string hashedpw = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword1.Text + salt, "SHA1");
                    byte[] enccc, key, iv;
                     
                    using (var aes = Rijndael.Create())
                    {
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                byte[] b = System.Text.Encoding.UTF8.GetBytes(tbCreditCard.Text);
                                cs.Write(b, 0, b.Length);
                                key = aes.Key;
                                iv = aes.IV;
                            }
                            enccc = ms.ToArray();
                        }
                    }
                    com.Parameters.AddWithValue("@fn", tbFirstName.Text);
                    com.Parameters.AddWithValue("@ln", tbLastName.Text);
                    com.Parameters.AddWithValue("@un", tbUserID0.Text);
                    com.Parameters.AddWithValue("@hp", hashedpw);
                    com.Parameters.AddWithValue("@salt", salt);
                    com.Parameters.AddWithValue("@enccc", Convert.ToBase64String(enccc));
                    com.Parameters.AddWithValue("@key", Convert.ToBase64String(key));
                    com.Parameters.AddWithValue("@iv", Convert.ToBase64String(iv));

                    com.ExecuteNonQuery();

                    Session["user"] = tbUserID.Text;
                    FormsAuthentication.RedirectFromLoginPage(tbUserID0.Text, false);
                }
            }
        }
    }
}