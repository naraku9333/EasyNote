namespace EasyNote
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginPanel = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.pword = new System.Windows.Forms.Label();
            this.uname = new System.Windows.Forms.Label();
            this.registerPanel = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tbInPassword2 = new System.Windows.Forms.TextBox();
            this.tbInPassword1 = new System.Windows.Forms.TextBox();
            this.tbCC = new System.Windows.Forms.TextBox();
            this.tbUsername2 = new System.Windows.Forms.TextBox();
            this.tbLastname = new System.Windows.Forms.TextBox();
            this.tbFirstname = new System.Windows.Forms.TextBox();
            this.fname = new System.Windows.Forms.Label();
            this.lname = new System.Windows.Forms.Label();
            this.uname2 = new System.Windows.Forms.Label();
            this.cc = new System.Windows.Forms.Label();
            this.pword1 = new System.Windows.Forms.Label();
            this.pword2 = new System.Windows.Forms.Label();
            this.loginPanel.SuspendLayout();
            this.registerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.btnRegister);
            this.loginPanel.Controls.Add(this.btnLogin);
            this.loginPanel.Controls.Add(this.tbPassword);
            this.loginPanel.Controls.Add(this.tbUsername);
            this.loginPanel.Controls.Add(this.pword);
            this.loginPanel.Controls.Add(this.uname);
            this.loginPanel.Location = new System.Drawing.Point(12, 47);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(529, 152);
            this.loginPanel.TabIndex = 0;
            // 
            // btnRegister
            // 
            this.btnRegister.BackgroundImage = global::EasyNote.Properties.Resources.Dark_Register_Button;
            this.btnRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRegister.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Location = new System.Drawing.Point(297, 107);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(86, 34);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.btnRegister.MouseEnter += new System.EventHandler(this.btnRegister_MouseEnter);
            this.btnRegister.MouseLeave += new System.EventHandler(this.btnRegister_MouseLeave);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.BackgroundImage = global::EasyNote.Properties.Resources.Dark_Login_Button;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Location = new System.Drawing.Point(164, 107);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(86, 34);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += new System.EventHandler(this.btnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new System.EventHandler(this.btnLogin_MouseLeave);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(204, 64);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(224, 20);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(204, 32);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(224, 20);
            this.tbUsername.TabIndex = 2;
            // 
            // pword
            // 
            this.pword.AutoSize = true;
            this.pword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pword.ForeColor = System.Drawing.Color.White;
            this.pword.Location = new System.Drawing.Point(87, 67);
            this.pword.Name = "pword";
            this.pword.Size = new System.Drawing.Size(83, 18);
            this.pword.TabIndex = 1;
            this.pword.Text = "Password: ";
            // 
            // uname
            // 
            this.uname.AutoSize = true;
            this.uname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uname.ForeColor = System.Drawing.Color.White;
            this.uname.Location = new System.Drawing.Point(87, 35);
            this.uname.Name = "uname";
            this.uname.Size = new System.Drawing.Size(85, 18);
            this.uname.TabIndex = 0;
            this.uname.Text = "Username: ";
            // 
            // registerPanel
            // 
            this.registerPanel.Controls.Add(this.btnSubmit);
            this.registerPanel.Controls.Add(this.tbInPassword2);
            this.registerPanel.Controls.Add(this.tbInPassword1);
            this.registerPanel.Controls.Add(this.tbCC);
            this.registerPanel.Controls.Add(this.tbUsername2);
            this.registerPanel.Controls.Add(this.tbLastname);
            this.registerPanel.Controls.Add(this.tbFirstname);
            this.registerPanel.Controls.Add(this.fname);
            this.registerPanel.Controls.Add(this.lname);
            this.registerPanel.Controls.Add(this.uname2);
            this.registerPanel.Controls.Add(this.cc);
            this.registerPanel.Controls.Add(this.pword1);
            this.registerPanel.Controls.Add(this.pword2);
            this.registerPanel.Location = new System.Drawing.Point(12, 1);
            this.registerPanel.Name = "registerPanel";
            this.registerPanel.Size = new System.Drawing.Size(529, 247);
            this.registerPanel.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::EasyNote.Properties.Resources.Dark_Submit_Button;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(222, 205);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(86, 25);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            this.btnSubmit.MouseEnter += new System.EventHandler(this.btnSubmit_MouseEnter);
            this.btnSubmit.MouseLeave += new System.EventHandler(this.btnSubmit_MouseLeave);
            // 
            // tbInPassword2
            // 
            this.tbInPassword2.Location = new System.Drawing.Point(204, 174);
            this.tbInPassword2.Name = "tbInPassword2";
            this.tbInPassword2.Size = new System.Drawing.Size(273, 20);
            this.tbInPassword2.TabIndex = 11;
            this.tbInPassword2.UseSystemPasswordChar = true;
            // 
            // tbInPassword1
            // 
            this.tbInPassword1.Location = new System.Drawing.Point(204, 141);
            this.tbInPassword1.Name = "tbInPassword1";
            this.tbInPassword1.Size = new System.Drawing.Size(273, 20);
            this.tbInPassword1.TabIndex = 10;
            this.tbInPassword1.UseSystemPasswordChar = true;
            // 
            // tbCC
            // 
            this.tbCC.Location = new System.Drawing.Point(204, 112);
            this.tbCC.Name = "tbCC";
            this.tbCC.Size = new System.Drawing.Size(273, 20);
            this.tbCC.TabIndex = 9;
            // 
            // tbUsername2
            // 
            this.tbUsername2.Location = new System.Drawing.Point(204, 80);
            this.tbUsername2.Name = "tbUsername2";
            this.tbUsername2.Size = new System.Drawing.Size(273, 20);
            this.tbUsername2.TabIndex = 8;
            // 
            // tbLastname
            // 
            this.tbLastname.Location = new System.Drawing.Point(204, 48);
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.Size = new System.Drawing.Size(273, 20);
            this.tbLastname.TabIndex = 7;
            // 
            // tbFirstname
            // 
            this.tbFirstname.Location = new System.Drawing.Point(204, 16);
            this.tbFirstname.Name = "tbFirstname";
            this.tbFirstname.Size = new System.Drawing.Size(273, 20);
            this.tbFirstname.TabIndex = 6;
            // 
            // fname
            // 
            this.fname.AutoSize = true;
            this.fname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fname.ForeColor = System.Drawing.Color.White;
            this.fname.Location = new System.Drawing.Point(26, 19);
            this.fname.Name = "fname";
            this.fname.Size = new System.Drawing.Size(89, 18);
            this.fname.TabIndex = 0;
            this.fname.Text = "First Name: ";
            // 
            // lname
            // 
            this.lname.AutoSize = true;
            this.lname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lname.ForeColor = System.Drawing.Color.White;
            this.lname.Location = new System.Drawing.Point(26, 50);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(88, 18);
            this.lname.TabIndex = 5;
            this.lname.Text = "Last Name: ";
            // 
            // uname2
            // 
            this.uname2.AutoSize = true;
            this.uname2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uname2.ForeColor = System.Drawing.Color.White;
            this.uname2.Location = new System.Drawing.Point(26, 82);
            this.uname2.Name = "uname2";
            this.uname2.Size = new System.Drawing.Size(85, 18);
            this.uname2.TabIndex = 4;
            this.uname2.Text = "Username: ";
            // 
            // cc
            // 
            this.cc.AutoSize = true;
            this.cc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cc.ForeColor = System.Drawing.Color.White;
            this.cc.Location = new System.Drawing.Point(26, 113);
            this.cc.Name = "cc";
            this.cc.Size = new System.Drawing.Size(91, 18);
            this.cc.TabIndex = 3;
            this.cc.Text = "Credit Card: ";
            // 
            // pword1
            // 
            this.pword1.AutoSize = true;
            this.pword1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pword1.ForeColor = System.Drawing.Color.White;
            this.pword1.Location = new System.Drawing.Point(26, 143);
            this.pword1.Name = "pword1";
            this.pword1.Size = new System.Drawing.Size(83, 18);
            this.pword1.TabIndex = 2;
            this.pword1.Text = "Password: ";
            // 
            // pword2
            // 
            this.pword2.AutoSize = true;
            this.pword2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pword2.ForeColor = System.Drawing.Color.White;
            this.pword2.Location = new System.Drawing.Point(26, 176);
            this.pword2.Name = "pword2";
            this.pword2.Size = new System.Drawing.Size(138, 18);
            this.pword2.TabIndex = 1;
            this.pword2.Text = "Confirm password: ";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(553, 249);
            this.Controls.Add(this.registerPanel);
            this.Controls.Add(this.loginPanel);
            this.Name = "Login";
            this.Text = "Login";
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.registerPanel.ResumeLayout(false);
            this.registerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label uname;
        private System.Windows.Forms.Label pword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Panel registerPanel;
        private System.Windows.Forms.Label lname;
        private System.Windows.Forms.Label uname2;
        private System.Windows.Forms.Label cc;
        private System.Windows.Forms.Label pword1;
        private System.Windows.Forms.Label pword2;
        private System.Windows.Forms.Label fname;
        private System.Windows.Forms.TextBox tbInPassword2;
        private System.Windows.Forms.TextBox tbInPassword1;
        private System.Windows.Forms.TextBox tbCC;
        private System.Windows.Forms.TextBox tbUsername2;
        private System.Windows.Forms.TextBox tbLastname;
        private System.Windows.Forms.TextBox tbFirstname;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnSubmit;
    }
}