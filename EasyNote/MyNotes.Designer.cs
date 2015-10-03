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
            this.pbCancelBttn = new System.Windows.Forms.PictureBox();
            this.pbDeleteBttn = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pbShowTags = new System.Windows.Forms.PictureBox();
            this.pbAddNote = new System.Windows.Forms.PictureBox();
            this.noteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pbSaveBttn = new System.Windows.Forms.PictureBox();
            this.myNotesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancelBttn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteBttn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSaveBttn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myNotesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AllowDrop = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTitle.Location = new System.Drawing.Point(34, 157);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(52, 35);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Title";
            // 
            // lbTags
            // 
            this.lbTags.AllowDrop = true;
            this.lbTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTags.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTags.Location = new System.Drawing.Point(34, 229);
            this.lbTags.Name = "lbTags";
            this.lbTags.Size = new System.Drawing.Size(60, 35);
            this.lbTags.TabIndex = 1;
            this.lbTags.Text = "Tags";
            // 
            // lbText
            // 
            this.lbText.AllowDrop = true;
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbText.Location = new System.Drawing.Point(34, 311);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(60, 35);
            this.lbText.TabIndex = 2;
            this.lbText.Text = "Text";
            // 
            // lbAllNotes
            // 
            this.lbAllNotes.AllowDrop = true;
            this.lbAllNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAllNotes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbAllNotes.Location = new System.Drawing.Point(14, 494);
            this.lbAllNotes.Name = "lbAllNotes";
            this.lbAllNotes.Size = new System.Drawing.Size(100, 35);
            this.lbAllNotes.TabIndex = 3;
            this.lbAllNotes.Text = "All Notes";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.Location = new System.Drawing.Point(144, 163);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(457, 28);
            this.tbTitle.TabIndex = 1;
            // 
            // tBTags
            // 
            this.tBTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBTags.Location = new System.Drawing.Point(144, 234);
            this.tBTags.Name = "tBTags";
            this.tBTags.Size = new System.Drawing.Size(457, 28);
            this.tBTags.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(144, 311);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(457, 172);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(152, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 20);
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
            this.dgvNotesList.Location = new System.Drawing.Point(18, 534);
            this.dgvNotesList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvNotesList.Name = "dgvNotesList";
            this.dgvNotesList.RowTemplate.ReadOnly = true;
            this.dgvNotesList.Size = new System.Drawing.Size(828, 228);
            this.dgvNotesList.TabIndex = 4;
            this.dgvNotesList.DoubleClick += new System.EventHandler(this.dgvNotesList_DoubleClick);
            // 
            // pbCancelBttn
            // 
            this.pbCancelBttn.Image = global::EasyNote.Properties.Resources.Dark_Cancel_Button;
            this.pbCancelBttn.Location = new System.Drawing.Point(680, 364);
            this.pbCancelBttn.Name = "pbCancelBttn";
            this.pbCancelBttn.Size = new System.Drawing.Size(129, 38);
            this.pbCancelBttn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCancelBttn.TabIndex = 17;
            this.pbCancelBttn.TabStop = false;
            this.pbCancelBttn.Visible = false;
            this.pbCancelBttn.Click += new System.EventHandler(this.pbCancelBttn_Click);
            this.pbCancelBttn.MouseEnter += new System.EventHandler(this.pbCancelBttn_MouseEnter);
            this.pbCancelBttn.MouseLeave += new System.EventHandler(this.pbCancelBttn_MouseLeave);
            // 
            // pbDeleteBttn
            // 
            this.pbDeleteBttn.ErrorImage = global::EasyNote.Properties.Resources.Dark_Delete_Button;
            this.pbDeleteBttn.Image = global::EasyNote.Properties.Resources.Dark_Delete_Button;
            this.pbDeleteBttn.Location = new System.Drawing.Point(680, 297);
            this.pbDeleteBttn.Name = "pbDeleteBttn";
            this.pbDeleteBttn.Size = new System.Drawing.Size(129, 38);
            this.pbDeleteBttn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDeleteBttn.TabIndex = 16;
            this.pbDeleteBttn.TabStop = false;
            this.pbDeleteBttn.Visible = false;
            this.pbDeleteBttn.Click += new System.EventHandler(this.pbDeleteBttn_Click);
            this.pbDeleteBttn.MouseEnter += new System.EventHandler(this.pbDeleteBttn_MouseEnter);
            this.pbDeleteBttn.MouseLeave += new System.EventHandler(this.pbDeleteBttn_MouseLeave);
            // 
            // pbExit
            // 
            this.pbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbExit.Image = global::EasyNote.Properties.Resources.Dark_Exit_Button;
            this.pbExit.Location = new System.Drawing.Point(680, 429);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(129, 38);
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
            this.pbShowTags.Location = new System.Drawing.Point(680, 234);
            this.pbShowTags.Name = "pbShowTags";
            this.pbShowTags.Size = new System.Drawing.Size(129, 38);
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
            this.pbAddNote.Location = new System.Drawing.Point(680, 173);
            this.pbAddNote.Name = "pbAddNote";
            this.pbAddNote.Size = new System.Drawing.Size(129, 38);
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
            // pbSaveBttn
            // 
            this.pbSaveBttn.Image = global::EasyNote.Properties.Resources.Dark_Save_Button;
            this.pbSaveBttn.Location = new System.Drawing.Point(680, 173);
            this.pbSaveBttn.Name = "pbSaveBttn";
            this.pbSaveBttn.Size = new System.Drawing.Size(129, 38);
            this.pbSaveBttn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSaveBttn.TabIndex = 18;
            this.pbSaveBttn.TabStop = false;
            this.pbSaveBttn.Visible = false;
            this.pbSaveBttn.Click += new System.EventHandler(this.pbSaveBttn_Click);
            this.pbSaveBttn.MouseEnter += new System.EventHandler(this.pbSaveBttn_MouseEnter);
            this.pbSaveBttn.MouseLeave += new System.EventHandler(this.pbSaveBttn_MouseLeave);
            // 
            // myNotesBindingSource
            // 
            this.myNotesBindingSource.DataSource = typeof(EasyNote.MyNotes);
            // 
            // MyNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(856, 806);
            this.Controls.Add(this.pbSaveBttn);
            this.Controls.Add(this.pbCancelBttn);
            this.Controls.Add(this.pbDeleteBttn);
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
            this.MinimumSize = new System.Drawing.Size(858, 818);
            this.Name = "MyNotes";
            this.Text = "Easy Note";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyNotes_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancelBttn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteBttn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSaveBttn)).EndInit();
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
        private System.Windows.Forms.PictureBox pbDeleteBttn;
        private System.Windows.Forms.PictureBox pbCancelBttn;
        private System.Windows.Forms.PictureBox pbSaveBttn;
    }
}

