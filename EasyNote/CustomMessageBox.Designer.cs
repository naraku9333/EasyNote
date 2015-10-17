namespace EasyNote
{
    partial class CustomMessageBox
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
            this.lbMessage = new System.Windows.Forms.Label();
            this.btBack = new System.Windows.Forms.Button();
            this.btForward = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbMessage.Location = new System.Drawing.Point(145, 32);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(196, 13);
            this.lbMessage.TabIndex = 2;
            this.lbMessage.Text = "Are you sure you wish to save the note?";
            // 
            // btBack
            // 
            this.btBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btBack.FlatAppearance.BorderSize = 0;
            this.btBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBack.Location = new System.Drawing.Point(64, 80);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(86, 25);
            this.btBack.TabIndex = 4;
            this.btBack.TabStop = false;
            this.btBack.UseVisualStyleBackColor = false;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            this.btBack.MouseEnter += new System.EventHandler(this.btBack_MouseEnter);
            this.btBack.MouseLeave += new System.EventHandler(this.btBack_MouseLeave);
            // 
            // btForward
            // 
            this.btForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btForward.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btForward.FlatAppearance.BorderSize = 0;
            this.btForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btForward.Location = new System.Drawing.Point(350, 80);
            this.btForward.Name = "btForward";
            this.btForward.Size = new System.Drawing.Size(86, 25);
            this.btForward.TabIndex = 5;
            this.btForward.TabStop = false;
            this.btForward.UseVisualStyleBackColor = false;
            this.btForward.Click += new System.EventHandler(this.btForward_Click);
            this.btForward.MouseEnter += new System.EventHandler(this.btForward_MouseEnter);
            this.btForward.MouseLeave += new System.EventHandler(this.btForward_MouseLeave);
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btBack;
            this.ClientSize = new System.Drawing.Size(484, 125);
            this.Controls.Add(this.btForward);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.lbMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomMessageBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Button btForward;
    }
}