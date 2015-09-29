namespace EasyNote
{
    partial class MyNotes
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
            this.components = new System.ComponentModel.Container();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbTags = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.lbAllNotes = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tBTags = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataSet1 = new System.Data.DataSet();
            this.dgvNotesList = new System.Windows.Forms.DataGridView();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbShowTags = new System.Windows.Forms.PictureBox();
            this.pbAddNote = new System.Windows.Forms.PictureBox();
            this.noteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.myNotesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myNotesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AllowDrop = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTitle.Location = new System.Drawing.Point(23, 102);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(35, 23);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Title";
            // 
            // lbTags
            // 
            this.lbTags.AllowDrop = true;
            this.lbTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTags.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTags.Location = new System.Drawing.Point(23, 149);
            this.lbTags.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTags.Name = "lbTags";
            this.lbTags.Size = new System.Drawing.Size(40, 23);
            this.lbTags.TabIndex = 1;
            this.lbTags.Text = "Tags";
            // 
            // lbText
            // 
            this.lbText.AllowDrop = true;
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbText.Location = new System.Drawing.Point(23, 202);
            this.lbText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(40, 23);
            this.lbText.TabIndex = 2;
            this.lbText.Text = "Text";
            // 
            // lbAllNotes
            // 
            this.lbAllNotes.AllowDrop = true;
            this.lbAllNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAllNotes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbAllNotes.Location = new System.Drawing.Point(9, 321);
            this.lbAllNotes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAllNotes.Name = "lbAllNotes";
            this.lbAllNotes.Size = new System.Drawing.Size(67, 23);
            this.lbAllNotes.TabIndex = 3;
            this.lbAllNotes.Text = "All Notes";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.Location = new System.Drawing.Point(96, 106);
            this.tbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(306, 21);
            this.tbTitle.TabIndex = 1;
            // 
            // tBTags
            // 
            this.tBTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBTags.Location = new System.Drawing.Point(96, 152);
            this.tBTags.Margin = new System.Windows.Forms.Padding(2);
            this.tBTags.Name = "tBTags";
            this.tBTags.Size = new System.Drawing.Size(306, 21);
            this.tBTags.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(96, 202);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(306, 113);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(101, 174);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "(input example: tag1:tag2:tag3)";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dgvNotesList
            // 
            this.dgvNotesList.AllowUserToAddRows = false;
            this.dgvNotesList.AllowUserToDeleteRows = false;
            this.dgvNotesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNotesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNotesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotesList.Location = new System.Drawing.Point(12, 347);
            this.dgvNotesList.Name = "dgvNotesList";
            this.dgvNotesList.RowTemplate.ReadOnly = true;
            this.dgvNotesList.Size = new System.Drawing.Size(552, 148);
            this.dgvNotesList.TabIndex = 4;
            // 
            // pbExit
            // 
            this.pbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbExit.Image = global::EasyNote.Properties.Resources.Dark_Exit_Button;
            this.pbExit.Location = new System.Drawing.Point(453, 212);
            this.pbExit.Margin = new System.Windows.Forms.Padding(2);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(86, 25);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExit.TabIndex = 15;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseEnter += new System.EventHandler(this.pbExit_MouseEnter);
            this.pbExit.MouseLeave += new System.EventHandler(this.pbExit_MouseLeave);
            // 
            // pbShowTags
            // 
            this.pbShowTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbShowTags.Image = global::EasyNote.Properties.Resources.Dark_Show_Button;
            this.pbShowTags.Location = new System.Drawing.Point(453, 162);
            this.pbShowTags.Margin = new System.Windows.Forms.Padding(2);
            this.pbShowTags.Name = "pbShowTags";
            this.pbShowTags.Size = new System.Drawing.Size(86, 25);
            this.pbShowTags.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbShowTags.TabIndex = 14;
            this.pbShowTags.TabStop = false;
            this.pbShowTags.Click += new System.EventHandler(this.pbShowTags_Click);
            this.pbShowTags.MouseEnter += new System.EventHandler(this.pbShowTags_MouseEnter);
            this.pbShowTags.MouseLeave += new System.EventHandler(this.pbShowTags_MouseLeave);
            // 
            // pbAddNote
            // 
            this.pbAddNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAddNote.Image = global::EasyNote.Properties.Resources.Dark_Add_Button;
            this.pbAddNote.Location = new System.Drawing.Point(453, 112);
            this.pbAddNote.Margin = new System.Windows.Forms.Padding(2);
            this.pbAddNote.Name = "pbAddNote";
            this.pbAddNote.Size = new System.Drawing.Size(86, 25);
            this.pbAddNote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAddNote.TabIndex = 13;
            this.pbAddNote.TabStop = false;
            this.pbAddNote.Click += new System.EventHandler(this.pbAddNote_Click_1);
            this.pbAddNote.MouseEnter += new System.EventHandler(this.pbAddNote_MouseEnter);
            this.pbAddNote.MouseLeave += new System.EventHandler(this.pbAddNote_MouseLeave);
            // 
            // noteBindingSource
            // 
            this.noteBindingSource.DataSource = typeof(NoteLibrary.Note);
            // 
            // myNotesBindingSource
            // 
            this.myNotesBindingSource.DataSource = typeof(EasyNote.MyNotes);
            // 
            // MyNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(571, 524);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbShowTags);
            this.Controls.Add(this.pbAddNote);
            this.Controls.Add(this.dgvNotesList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tBTags);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.lbAllNotes);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.lbTags);
            this.Controls.Add(this.lbTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(579, 551);
            this.Name = "MyNotes";
            this.Text = "Easy Note";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyNotes_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myNotesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbTags;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Label lbAllNotes;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tBTags;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource myNotesBindingSource;
        private System.Windows.Forms.BindingSource noteBindingSource;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.DataGridView dgvNotesList;
        private System.Windows.Forms.PictureBox pbAddNote;
        private System.Windows.Forms.PictureBox pbShowTags;
        private System.Windows.Forms.PictureBox pbExit;
    }
}

