using EasyNote.Properties;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Web.Security;
using System.Drawing;

namespace EasyNote
{
    public partial class Login : Form
    {
        public string user;
        public long id;
        private const string conString = "server=vps1.svogel.me;user=easynote;database=notebase;port=3306;password=CSCI_473;";

        public Login()
        {
            InitializeComponent();
            registerPanel.Visible = false;
            loginPanel.Visible = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (tbFirstname.Text != "" && tbLastname.Text != "" && tbUsername2.Text != ""
                 && tbInPassword1.Text != "" && tbInPassword2.Text != "")
            {
                if (tbInPassword1.Text == tbInPassword2.Text)
                {
                    using (var con = new MySqlConnection(conString))
                    {
                        con.Open();
                        using (var com = new MySqlCommand("select count(*) from Customers where username='" + tbUsername2.Text + "'", con))
                        {
                            var r = com.ExecuteScalar();
                            if ((long)r > 0)
                            {
                                MessageBox.Show(tbUsername2.Text + " already in use. Pick another username");
                                con.Close();
                                return;
                            }
                        }

                        using (var com = new MySqlCommand(
                            "insert into Customers(firstname, lastname, username, hashedpassword, salt, enccardnum, enckey, iv)"
                            + " values(@fn, @ln, @un, @hp, @salt, @enccc, @key, @iv)", con))
                        {
                            string salt = GenerateSaltString();//A series of random bytes to make the password longer. 
                                                               // byte[] hashedpw = new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(tbInPassword1.Text + salt));
                            string hashedpw = FormsAuthentication.HashPasswordForStoringInConfigFile(tbInPassword1.Text + salt, "SHA1");
                            byte[] enccc, key, iv; //The encrypted credit card, key for the aes algorithm, and iv from the algorithm.  

                            //Create an algorithm to use for symmettric encryption. 
                            using (var aes = Rijndael.Create())
                            {
                                //Create the memory stream to hold the output of encrpytion. 
                                using (var ms = new MemoryStream())
                                {
                                    //Use a cryptostream with an encryptor to perofrm the encryption.  
                                    //Then save these values to be placed in the database. 
                                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                                    {
                                        byte[] b = System.Text.Encoding.UTF8.GetBytes(tbCC.Text);
                                        cs.Write(b, 0, b.Length);
                                        key = aes.Key;
                                        iv = aes.IV;
                                    }
                                    enccc = ms.ToArray();
                                }
                            }

                            //Grab the parameters from the textboxes.  The hashedpassword, encrypted credit card, key, and IV
                            //have been calculated above and will be placed into the database as well. 
                            com.Parameters.AddWithValue("@fn", tbFirstname.Text);
                            com.Parameters.AddWithValue("@ln", tbLastname.Text);
                            com.Parameters.AddWithValue("@un", tbUsername2.Text);
                            com.Parameters.AddWithValue("@hp", hashedpw);
                            com.Parameters.AddWithValue("@salt", salt);
                            com.Parameters.AddWithValue("@enccc", Convert.ToBase64String(enccc));
                            com.Parameters.AddWithValue("@key", Convert.ToBase64String(key));
                            com.Parameters.AddWithValue("@iv", Convert.ToBase64String(iv));

                            com.ExecuteNonQuery();
                            id = com.LastInsertedId;

                            user = tbUsername2.Text;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                else MessageBox.Show("Passwords do not match!");
            }
            else
            {
                MessageBox.Show("All feilds are required!");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                //Try to get the hashed password and salt value for the input user.  
                using (var com = new MySqlCommand("select cust_id, hashedpassword, salt from Customers where username=@user", con))
                {
                    com.Parameters.AddWithValue("@user", tbUsername.Text);

                    //If there wasn't a value (couldn't read), let the user know that the username was invalid and show the registration panel.  
                    var reader = com.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("No user with ID, please register");
                        loginPanel.Visible = false;
                        registerPanel.Visible = true;
                    }
                    //Otherwise compare the hashed password vs the hash of the provided password.  
                    else
                    {
                        string salt = reader["salt"] as string;
                        string storedPW = reader["hashedpassword"] as string;

                        string providedPW = FormsAuthentication.HashPasswordForStoringInConfigFile(tbPassword.Text + salt, "SHA1");

                        //If the passwords are equal, log the user in.  Otherwise, notify them there was an error.  
                        if (storedPW == providedPW)
                        {
                           user = tbUsername.Text;
                            id = (int)reader["cust_id"];
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("'Invalid password!");
                        }
                    }
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            registerPanel.Visible = true;
            loginPanel.Visible = false;
            Text = "Register";
        }

        private void btnSubmit_MouseEnter(object sender, EventArgs e)
        {
            btnSubmit.BackgroundImage = Resources.Light_Submit_Button;
            btnSubmit.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void btnSubmit_MouseLeave(object sender, EventArgs e)
        {
            btnSubmit.BackgroundImage = Resources.Dark_Submit_Button;
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackgroundImage = Resources.Light_Login_Button;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackgroundImage = Resources.Dark_Login_Button;
        }

        private void btnRegister_MouseEnter(object sender, EventArgs e)
        {
            btnRegister.BackgroundImage = Resources.Light_Register_Button;
            btnRegister.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void btnRegister_MouseLeave(object sender, EventArgs e)
        {
            btnRegister.BackgroundImage = Resources.Dark_Register_Button;
        }

        private static string GenerateSaltString()
        {
            RNGCryptoServiceProvider prng = new RNGCryptoServiceProvider();         //The Pseudo Random Number Generator for the salt.  
            byte[] saltArray = new byte[50];

            prng.GetBytes(saltArray);
            return Encoding.Default.GetString(saltArray);
        }
    }
}
