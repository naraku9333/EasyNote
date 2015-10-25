﻿/**************************************************************************************
 * CLASS:     MyNotes
 * 
 * AUTHORS:   Cierria McPerryman
 *            Sean Vogel
 *            Robert Kahren
 *            Anthony Malmgren
 * 
 * ASSIGNMENT 3 Due 10/27/2015
 * 
 * NOTES:     This is the main application form.  It contains the GUI to display and search
 *            a note database as well as event handlers that modify the database by adding
 *            notes, deleting them, or updating them.  
 **************************************************************************************/
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Data.SqlClient;
using System.Text;
using EasyNote.Properties;

namespace EasyNote
{
    public partial class MyNotes : Form
    {
        private SqlConnection connection = null;    //Holds the connection to the database, using conString.

        private SqlCommand command = null;          //Holds a command to be executed on the database.  

        private SqlDataReader reader = null;        //Holds the result of a query from the database.  

        //The connection string to use for connecting to the notebase2 database.  
        private const string conString = "server=10.158.56.48;uid=net2;pwd=dtbz2;database=notebase2;";

        private int selectedNote;               //The current note_id selected in the dgv
        private int selectedRow;                //The current row selected in the dgv
        private DataTable notesTable = null;

        /**************************************************************************************
         * FUNCTION:  MyNotes()
         *
         * ARGUMENTS: None
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     Default constructor
         *            Initializes components, then creates the notes table.  
         **************************************************************************************/
        public MyNotes()
        {
            InitializeComponent();
            ResizeRedraw = true;//force redraw on window resize
            createNoteTable();
        }

        /**************************************************************************************
         * FUNCTION:  private void pbExit_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the exit button is clicked closing the window
         **************************************************************************************/
        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**************************************************************************************
         * FUNCTION:  private void pbShowTgs_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the show tags button is clicked
         *            creating a messagebox showing the tags in current note
         **************************************************************************************/
        private void pbShowTags_Click(object sender, EventArgs e)
        {
            char[] delimiterChars = { ':' };

            //assign text data in text box Tags
            string text = tbTags.Text;

            //parse text in text box using delimiter characters
            string[] words = text.Split(delimiterChars);

            //add title and new line
            string wordsShow = " Tags: " + Environment.NewLine;


            //for each string in words add to wordsShow with a newline after
            foreach (string s in words)
            {
                wordsShow += s + Environment.NewLine;
            }

            //message box showing the strings
            MessageBox.Show(wordsShow, "Tags");
        }

        /**************************************************************************************
         * FUNCTION:  private void createNoteTable()
         *
         * ARGUMENTS: none
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function gets the note and tag data from the Notes and Tags tables in
         *            the database and places them into a dataGridView.  
         **************************************************************************************/
        private void createNoteTable()
        {
            //Try to execute the following Sql statements
            try
            {
                //Connect to the notebase2 database.  
                using (connection = new SqlConnection(conString))
                {
                    connection.Open();

                    //Grab all of the notes (not including their tags) from the notes table
                    using (SqlDataAdapter data = new SqlDataAdapter("select * from Notes", connection))
                    {
                        //Create a datatable and fill it with the table from our query.   
                        notesTable = new DataTable();
                        data.Fill(notesTable);

                        //The tags for each note are added in an additional column and the NotesTable is bound to the datagrid.  
                        notesTable.Columns.Add("Tags");
                        dgvNotesList.DataSource = notesTable;
                        
                        //Hide the note id, but keep it in the table to make future Sql commands easier.  
                        dgvNotesList.Rows[0].Visible = false;

                        //Add the notes for each row based on the note id for that row.  
                        foreach (DataGridViewRow row in dgvNotesList.Rows)
                        {
                            //The primary key of the note, used to determine what tags are associated with this row.  
                            int noteID = (int)row.Cells[0].Value;

                            //Use a StringBuilder to hold a combination of all of the tags.  
                            StringBuilder tagString = new StringBuilder();

                            //Grab all of the tags that are associated with the note.
                            command = new SqlCommand("select Tag.text from Tag,NoteTags,Notes where Tag.tag_id = NoteTags.tag_id and NoteTags.note_id = Notes.note_id and Notes.note_id = " + noteID, connection);

                            //Read the results of the SqlCommand and create a tagString from the individual tags associated with the note, 
                            //then place it in the table.
                            using (reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    tagString.Append(reader.GetString(0) + ":");
                                }

                                //Remove the last ':' from the tag string.
                                if (tagString.Length != 0)
                                    tagString.Remove(tagString.Length - 1, 1);

                                row.Cells["Tags"].Value = tagString.ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show("Creating the DataGridView of the notes list failed:" + e.Message + "\n" + e.ToString());
            }
        }

        /**************************************************************************************
         * FUNCTION:  private void MyNotes_Paint(object sender, PaintEventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the window is painted/repainted
         *            adding the application name in custom font centered in window
         **************************************************************************************/
        private void MyNotes_Paint(object sender, PaintEventArgs e)
        {
            string s = "Easy Note";   //The title of the program that will be painted.

            //load custom font
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("../../VLADIMIR.TTF");
            Font f = new Font(pfc.Families[0], 30, FontStyle.Bold);

            PointF p = new PointF(ClientSize.Width / 2 - e.Graphics.MeasureString(s, f).Width / 2, 20.0f);
            SolidBrush b = new SolidBrush(Color.White);

            e.Graphics.DrawString(s, f, b, p);
        }

        /**************************************************************************************
         * FUNCTION:  private void pbAddNote_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbAddNote and changes
         *            the displayed image
         **************************************************************************************/
        private void pbAddNote_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image addButton =  EasyNote.Properties.Resources.Light_Add_Button;
            pbAddNote.Image = addButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbAddNote_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbAddNote and changes
         *            the displayed image
         **************************************************************************************/
        private void pbAddNote_MouseLeave(object sender, EventArgs e)
        {
            //create image from resource and display
            Image addButton = EasyNote.Properties.Resources.Dark_Add_Button;
            pbAddNote.Image = addButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbExit_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbExit and changes
         *            the displayed image
         **************************************************************************************/
        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image exitButton = EasyNote.Properties.Resources.Light_Exit_Button;
            pbExit.Image = exitButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbExit_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbExit and changes
         *            the displayed image
         **************************************************************************************/
        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            //create image from resource and display
            Image exitButton = EasyNote.Properties.Resources.Dark_Exit_Button;
            pbExit.Image = exitButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbShowTags_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbShowTags and changes
         *            the displayed image
         **************************************************************************************/
        private void pbShowTags_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image showButton = EasyNote.Properties.Resources.Light_Show_Button;
            pbShowTags.Image = showButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbShowTgs_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbShowTags and changes
         *            the displayed image
         **************************************************************************************/
        private void pbShowTags_MouseLeave(object sender, EventArgs e)
        {
            //create image from resource and display
            Image showButton = EasyNote.Properties.Resources.Dark_Show_Button;
            pbShowTags.Image = showButton;
        }

        /***************************Updated for Assignment 3************************************
         * FUNCTION:  private void pbAddNote_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the pbAddNote button is clicked It calls the 
         *            CustomerMessageBox Class and displays a confirmation box to the user to 
         *            cofirm they wish to add the note. If they do, it adds the new note to 
         *            note list and notfile
         **************************************************************************************/
        private void pbAddNote_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text != "" && tbBody.Text != "")
            {                
                DialogResult result =  CustomMessageBox.Show("Are you sure you wish to add this note?", "Add Note",
                    Resources.Light_Cancel_Button, Resources.Dark_Cancel_Button, Resources.Light_Ok_Button, Resources.Dark_Ok_Button);
               
                //if the user confirms the adding of the note
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (connection = new SqlConnection(conString))
                        {
                            using (var com = new SqlCommand("addnote", connection) { CommandType = CommandType.StoredProcedure })
                            {
                                com.Connection = connection;
                                com.Parameters.AddWithValue("@title", tbTitle.Text);
                                com.Parameters.AddWithValue("@body", tbBody.Text);
                                com.Parameters.AddWithValue("@tags", tbTags.Text);

                                using (var adapter = new SqlDataAdapter(com))
                                {
                                    adapter.Fill(notesTable);
                                    createNoteTable();
                                }
                            }
                        }
                        clearText();
                    }
                    catch (SqlException sqle)
                    {
                        MessageBox.Show("There was an issue saving the update to the database: " + sqle.Message);
                    }
                }                
            }
        }

        /**************************************************************************************
         * FUNCTION:  private dgvNotesList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   No return value, but textFields in the program will be modified.
         *
         * NOTES:     This function takes the values for the note selected in the dataGridView 
         *            and copies them into textFields for the user to modify.  
         **************************************************************************************/
        private void dgvNotesList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //If the column or line headers were double clicked, do nothing
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            selectedRow = e.RowIndex;

            //Show the save,delete, and cancel buttons.  
            changeButtonView();

            //Grab the title, body, and tags associated with the selected note and put them in
            //textfields for the user to see.  
            tbTitle.Text = dgvNotesList.Rows[selectedRow].Cells["title"].Value as string;
            tbBody.Text = dgvNotesList.Rows[selectedRow].Cells["body"].Value as string;
            tbTags.Text = dgvNotesList.Rows[selectedRow].Cells["Tags"].Value as string;

            //Grab the id of the note the user selected from the table for later queries.  
            selectedNote = (int)dgvNotesList.Rows[selectedRow].Cells["note_id"].Value;
            
        }

        /**************************************************************************************
         * FUNCTION:  private void pbCancelBttn_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbCancelBtnn and
         *            changes the displayed image
         **************************************************************************************/
        private void pbCancelBttn_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image cancelButton = Resources.Light_Cancel_Button;
            pbCancelBttn.Image = cancelButton;

        }

        /**************************************************************************************
         * FUNCTION:  private void pbCancelBttn_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbCancelBttn and changes
         *            the displayed image
         **************************************************************************************/
        private void pbCancelBttn_MouseLeave(object sender, EventArgs e)
        {
            //create image from resource and display
            Image cancelButton = Resources.Dark_Cancel_Button;
            pbCancelBttn.Image = cancelButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbDeleteBttn_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbDeleteBttn and 
         *            changes the displayed image
         **************************************************************************************/
        private void pbDeleteBttn_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image deleteButton = Resources.Light_Delete_Button;
            pbDeleteBttn.Image = deleteButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbDeleteBttn_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbDeleteBttn and changes
         *            the displayed image
         **************************************************************************************/
        private void pbDeleteBttn_MouseLeave(object sender, EventArgs e)
        {
            //create image from resource and display
            Image deleteButton = Resources.Dark_Delete_Button;
            pbDeleteBttn.Image = deleteButton;
        }

        /*******************************Updated for Assignment 3*******************************
         * FUNCTION:  private void pbSaveBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value, but the changes will appear in the 
         *            datagridview for the selected note and the database will be updated.  
         *
         * NOTES:     This function is called when the pbSaveBttn is clicked
         *            It saves the changes to the note in the datagridview and to the database.  
         *            
         **************************************************************************************/
        private void pbSaveBttn_Click(object sender, EventArgs e)
        {
            DialogResult result = CustomMessageBox.Show("Are you sure you wish to save this note?", "Save Note",
                Resources.Light_Cancel_Button, Resources.Dark_Cancel_Button, Resources.Light_Ok_Button, Resources.Dark_Ok_Button);

            //If the user wants to save the note, then start modifying the tables.   
            if (result == DialogResult.Yes)
            {
                //Remove the save / cancel button.  
                changeButtonView();
                try
                {
                    using (connection = new SqlConnection(conString))
                    {
                        using (var com = new SqlCommand("updatenote", connection) { CommandType = CommandType.StoredProcedure })
                        {
                            com.Connection = connection;
                            com.Parameters.AddWithValue("@noteid", selectedNote);
                            com.Parameters.AddWithValue("@title", tbTitle.Text);
                            com.Parameters.AddWithValue("@body", tbBody.Text);
                            com.Parameters.AddWithValue("@tags", tbTags.Text);

                            using (var adapter = new SqlDataAdapter(com))
                            {
                                adapter.Fill(notesTable);
                                createNoteTable();
                            }
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    MessageBox.Show("There was an issue saving the update to the database: " + sqle.Message);
                }                
            } //end if dialogresult == yes        
        }

        /**************************************************************************************
         * FUNCTION:  private void pbSaveBttn_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbSaveBttn and changes
         *            the displayed image
         **************************************************************************************/
        private void pbSaveBttn_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image SaveButton = Resources.Light_Save_Button;
            pbSaveBttn.Image = SaveButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbSaveBttn_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbSaveBttn and changes
         *            the displayed image
         **************************************************************************************/
        private void pbSaveBttn_MouseLeave(object sender, EventArgs e)
        {
            //create image from resource and display
            Image SaveButton = Resources.Dark_Save_Button;
            pbSaveBttn.Image = SaveButton;
        }

        /**************************************************************************************
         * FUNCTION:  private void pbCancelBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the pbCancelBttn is clicked
         *            It discards any changes to the selected note
         **************************************************************************************/
        private void pbCancelBttn_Click(object sender, EventArgs e)
        {
            changeButtonView();
            clearText();
        }

        /**********************Updated for Assignment 3***************************************
         * FUNCTION:  private void pbDeleteBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value, but the dataGridView may have a row be
         *            removed.  The database will also have this value deleted.  
         *
         * NOTES:     This function is called when the pbSaveBttn is clicked
         *            It deletes the currently selected row from the datagridview and the 
         *            currently selected note from that row from the database.  
         *
         **************************************************************************************/
        private void pbDeleteBttn_Click(object sender, EventArgs e)
        {           
            DialogResult result = CustomMessageBox.Show("Are you sure you wish to delete this note?", "Add Note",
                Resources.Light_Cancel_Button, Resources.Dark_Cancel_Button, Resources.Light_Ok_Button, Resources.Dark_Ok_Button);

            //If the user does want to delete the note, remove it from the notes table as well as well as any references in the NoteTags table.  
            if (result == DialogResult.Yes)
            {
                try
                {
                    //Open a connection
                    using (connection = new SqlConnection(conString))
                    {
                        connection.Open();

                        //Remove references to the note in note_tags.  
                        using (command = new SqlCommand("delete from Notes where note_id = @id", connection))
                        {
                            command.Parameters.AddWithValue("id", selectedNote);
                            command.ExecuteNonQuery();                            
                        }

                        clearText();

                        //Hide the save/cancel/delete button.  
                        changeButtonView();

                        //Remove the row for the dgv to keep the database and local table in sync.  
                        dgvNotesList.Rows.RemoveAt(selectedRow);
                    }
                }
                catch (SqlException sqle)
                {
                    MessageBox.Show("There was an issue with deleting from the database: " + sqle.Message);
                }
            }
        }
        
        /**************************************************************************************
         * FUNCTION:  private void clearText()
         *
         * ARGUMENTS: This function takes no args
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     Helper function to clear UI text feilds
         **************************************************************************************/
        private void clearText()
        {
            tbTags.Clear();
            tbTitle.Clear();
            tbBody.Clear();
            dgvNotesList.ClearSelection();
        }        

        /**************************************************************************************
         * FUNCTION:  private void changeButtonView()
         *
         * ARGUMENTS: This function takes no args
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     Helper function to swap button images for UI views
         **************************************************************************************/
        private void changeButtonView()
        {
            pbAddNote.Visible = !pbAddNote.Visible;
            pbSaveBttn.Visible = !pbSaveBttn.Visible;
            pbDeleteBttn.Visible = !pbDeleteBttn.Visible;
            pbCancelBttn.Visible = !pbCancelBttn.Visible;
        }

        /****************************New for Assignment 3**************************************
         * FUNCTION:   private void tbSearch_TextChanged(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the text of tbSearch changes.  It sets the 
         *            the clear button visiblity appropriately, filters the results displayed 
         *            in the DataGridView, and isplays the number of numbers that have tags 
         *            containing the search text entered.   
         **************************************************************************************/
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            int count = 0;                              //tracks matching number of notes
            //if the user has text entered into the search box
            if (tbSearch.Text != "")
            {
                pbClearBtn.Visible = true;
                //check each row in the DataGridView
                foreach (System.Windows.Forms.DataGridViewRow row in dgvNotesList.Rows)
                {
                    //if a tag contains the search text, display the row and increament count
                    if ((row.Cells[5]).Value.ToString().ToLower().Contains(tbSearch.Text.ToLower()))
                    {
                        dgvNotesList.Rows[row.Index].Visible = true;
                        count++;
                    }
                    //otherwise hide the row
                    else
                    {
                        CurrencyManager curr = (CurrencyManager)BindingContext[dgvNotesList.DataSource];
                        curr.SuspendBinding();
                        dgvNotesList.Rows[row.Index].Visible = false;
                        curr.ResumeBinding();
                    }
                }
                //assign string with number of mathcing notes to lbTagsFound and display it
                lbMatching.Text = "Number of matches: " + count.ToString();
                lbMatching.Visible = true;

            }
            //otherwise hide pbClearButton button and lbTagsFound
            else
            {
                pbClearBtn.Visible = false;
                lbMatching.Visible = false;
                //set each row in the DataGridView to visible
                foreach (System.Windows.Forms.DataGridViewRow row in dgvNotesList.Rows)
                {
                    dgvNotesList.Rows[row.Index].Visible = true;
                }

            }

        }

        /************************New for Assignment 3*******************************************
         * FUNCTION:  private void pbClearButton_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the pbClearButton is clicked
         *            It clears the tbSearch.
         **************************************************************************************/
        private void pbClearBtn_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
        }

        /**************************New for Assignment 3****************************************
         * FUNCTION:  private void pbClearButton_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbClearButton and 
         *            changes the displayed image
         **************************************************************************************/
        private void pbClearBtn_MouseEnter(object sender, EventArgs e)
        {
            Image ClearButton = Resources.Light_Clear_Button;
            pbClearBtn.Image = ClearButton;
        }

        /************************************New for Assignment 3******************************
         * FUNCTION:  private void pbClearButton_MouseLeave(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is off of pbClearButton and changes
         *            the displayed image
         **************************************************************************************/
        private void pbClearBtn_MouseLeave(object sender, EventArgs e)
        {
            Image ClearButton = Resources.Dark_Clear_Button;
            pbClearBtn.Image = ClearButton;
        }
    }
}
