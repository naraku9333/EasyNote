/**************************************************************************************
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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using NoteLibrary;
using System.IO;
using System.Drawing.Text;
using System.Data.SqlClient;
using System.Text;

namespace EasyNote
{
    public partial class MyNotes : Form
    {
        private const String FILE_LOCATION = "notefile.txt";

        private List<Note> notes = new List<Note>();

        private Note currentNote = null;            //currently selected note iff its a stored note

        private SqlConnection connection = null;    //Holds the connection to the database, using conString.

        private SqlCommand command = null;          //Holds a command to be executed on the database.  

        private SqlDataReader reader = null;        //Holds the result of a query from the database.  

        //The connection string to use for connecting to the notebase2 database.  
        private string conString = "server=10.158.56.48;uid=net2;pwd=dtbz2;database=notebase2;";

        private int selectedNote;               //The current note_id selected in the dgv
        private int selectedRow;                //The current row selected in the dgv

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
//           readNotesFile();
//            getDllNotes();
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
         * FUNCTION:  private void readNotesFile()
         *
         * ARGUMENTS: none
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function reads lines from a note file and constructs a Note from the
         *            data in the line. The Note is then added to a list of Notes
         **************************************************************************************/
        private void readNotesFile()
        {
            String[] line;  //A line from the file that has been split up into its individual parts (based on *)
            String name;    //The name of the note, appears as the first argument on a line.
            String body;    //The body of the note, appears as the second argument on the line.
            String tags;    //The tags of the note, appears as the third argument on the line.

           try
           {
                using(StreamReader reader = new StreamReader(FILE_LOCATION) )
                {

                    //Read the notes file, splitting the line into its parts.  Then, use these parts to construct a note.
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine().Split('*');
                        if (line.Length == 3)
                        {
                            name = line[0];
                            body = line[1];
                            tags = line[2];

                            this.addNewNote(line[0], line[1], line[2]);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("There was an error processing the file: " + e.Message);
                Environment.Exit(-1);
            }
        }

        /**************************************************************************************
         * FUNCTION:  private void writeNotesFile()
         *
         * ARGUMENTS: none
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function overwrites the notefile with the current data
         *            in the notes list
         **************************************************************************************/
        private void writeNotesFile()
        {
            using (StreamWriter sw = new StreamWriter(FILE_LOCATION))
            {
                foreach(Note n in notes)
                {
                    if(n.Modifiable)
                        sw.WriteLine(n.Title + "*" + n.Body + "*" + n.getTagString());
                }
            }
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
                        DataTable notesTable = new DataTable();
                        data.Fill(notesTable);

                        //The tags for each note are added in an additional column and the NotesTable is bound to the datagrid.  
                        notesTable.Columns.Add("Tags");
                        dgvNotesList.DataSource = notesTable;
                        
                        //Hide the note id, but keep it in the table to make future Sql commands easier.  
            //            dgvNotesList.Rows[0].Visible = false;

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

        /**************************************************************************************
         * FUNCTION:  private addNewNote(string title, string text, string tagString)
         *
         * ARGUMENTS: title - the title of the note
         *            body - the body text of the note
         *            tagString - all of the tags for the note, in one combined string.
         *
         * RETURNS:   This function returns true if note is sucsessfully created, and the notes
         *            list will be modified to have a new note with the entered values.
         *
         * NOTES:    AddNewNote not only adds the note, but it also does the exception checking for
         *           it as well.
         **************************************************************************************/
        private bool addNewNote(string title, string text, string tagString, bool mod = true)
        {
            try
            {
                String[] tags = Note.splitTags(tagString);
                notes.Add(new Note(title, text, tags, mod));  //CEdge add modifiable arg
                return true;
            }
            catch (NoteException ne)
            {
                //If there was an innerException contained with the note exception, but its message in the caption.
                if (ne.InnerException != null)
                {
                    MessageBox.Show(ne.Message + ne.Tag, ne.InnerException.Message);
                }
                //Otherwise there is no innerException, just show the message.
                else
                {
                    MessageBox.Show(ne.Message, "Note Exception");
                }
            }
            return false;
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
         *            cofirm they wish to add the note. If they do, it adds the new note to note list and notfile
         **************************************************************************************/
        private void pbAddNote_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text != "" && tbBody.Text != "")
            {
                Image lightAdd = EasyNote.Properties.Resources.Light_Ok_Button;
                Image darkAdd = EasyNote.Properties.Resources.Dark_Ok_Button;
                Image lightCancel = EasyNote.Properties.Resources.Light_Cancel_Button;
                Image darkCancel = EasyNote.Properties.Resources.Dark_Cancel_Button;
                DialogResult result =  CustomMessageBox.Show("Are you sure you wish to add this note?", "Add Note", lightCancel, darkCancel, lightAdd, darkAdd);
                //if the user confirms the adding of the note
                if (result == DialogResult.Yes)
                {
                    if(addNewNote(tbTitle.Text, tbBody.Text, tbTags.Text))
                    {

                        using (StreamWriter sw = File.AppendText(FILE_LOCATION))
                        {
                            sw.WriteLine(tbTitle.Text + "*" + tbBody.Text + "*" + tbTags.Text);
                        }

                        string[] row = { tbTitle.Text, tbBody.Text, tbTags.Text.Replace(":", ", ") };
                        dgvNotesList.Rows.Add(row);
                    }
                    clearText();
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
         * NOTES:     This function takes the values for the note selected in the dataGridView and copies
         *            them into textFields for the user to modify.  
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
            Image cancelButton = EasyNote.Properties.Resources.Light_Cancel_Button;
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
            Image cancelButton = EasyNote.Properties.Resources.Dark_Cancel_Button;
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
         * NOTES:     This function is called when the mouse is moved over pbDeleteBttn and changes
         *            the displayed image
         **************************************************************************************/
        private void pbDeleteBttn_MouseEnter(object sender, EventArgs e)
        {
            //create image from resource and display
            Image deleteButton = EasyNote.Properties.Resources.Light_Delete_Button;
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
            Image deleteButton = EasyNote.Properties.Resources.Dark_Delete_Button;
            pbDeleteBttn.Image = deleteButton;
        }

        /*******************************Updated for Assignment 3*******************************************************
         * FUNCTION:  private void pbSaveBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value, but the changes will appear in the 
         *            datagridview for the selected note and the database will be updated.  
         *
<<<<<<< HEAD
         * NOTES:     This function is called when the pbSaveBttn is clicked
         *            It saves the changes to the note in the datagridview and to the database.  
         *            
         **************************************************************************************/
        private void pbSaveBttn_Click(object sender, EventArgs e)
        {
            //Images for the message buttons on the message box.  Used to ask the user to confirm saving the note.  
            Image lightForward = EasyNote.Properties.Resources.Light_Save_Button;
            Image darkForward = EasyNote.Properties.Resources.Dark_Save_Button;
            Image lightBack = EasyNote.Properties.Resources.Light_Cancel_Button;
            Image darkBack = EasyNote.Properties.Resources.Dark_Cancel_Button;
            DialogResult result = CustomMessageBox.Show("Are you sure you wish to save this note?", "Add Note", lightBack, darkBack, lightForward, darkForward);

           

            //If the user wants to save the note, then start modifying the tables.   
            if (result == DialogResult.Yes)
            {
                //Remove the save / cancel button.  
                changeButtonView(); 

                //Get the time for the updated field of the note.  
                DateTime updatedTime =  DateTime.Now;
                try
                {
                    //Start by opening a new connection.  
                    using (connection = new SqlConnection(conString))
                    {
                        connection.Open();

                        //Grab the new title and body from the text boxes and update the database with them.  
                        using (command = new SqlCommand("update Notes set title = @title, body = @body, updated = @update where note_id = @id", connection))
                        {
                            command.Parameters.AddWithValue("title", tbTitle.Text);
                            command.Parameters.AddWithValue("body", tbBody.Text);
                            command.Parameters.AddWithValue("update", updatedTime);
                            command.Parameters.AddWithValue("id", selectedNote);
                            command.ExecuteNonQuery();

                            //For simplicity, just delete all the tags associated with the note.  We will add these tags back in next.  
                            //Without doing this, the program would have to check if each tag were still valid.  
                            command.CommandText = "delete from NoteTags where note_id = @id";
                            command.ExecuteNonQuery();

                            //Add two new parameters for sql commands for the tag text and tag_id.  They are defined here as they may change during
                            //the following loop.  
                            command.Parameters.AddWithValue("tag", "");
                            command.Parameters.AddWithValue("tagid", "");

                            string[] tags = Note.splitTags(tbTags.Text);            //Holds all of the tags the user typed in.  

                            //Now determine the tags from what the user input into the textfield.  If a tag exists, it will be added into the NoteTag table.  
                            //If a tag doesn't exist, it is created here before being added to the table.  
                            foreach (string tag in tags)
                            {
                                //Try to get the tag if it exists.  
                                command.CommandText = "select tag_id from Tag where text = @tag";
                                command.Parameters["tag"].Value = tag;

                                reader = command.ExecuteReader();

                                //It the tag existed, then add it to NoteTags
                                if (reader.Read())
                                {
                                    command.CommandText = "insert into NoteTags values (@id,@tagid)";
                                    command.Parameters["tagid"].Value = reader.GetInt32(0);
                                    reader.Close();
                                    command.ExecuteNonQuery();
                                }
                                //Otherwise, create the tag, then get its tag_id and insert it into note tags.  
                                else
                                {
                                    reader.Close();
                                    command.CommandText = "insert into Tag values (@tag)";
                                    command.ExecuteNonQuery();

                                    //Grab the tag_id of the tag we just created
                                    command.CommandText = "select tag_id from Tag where text = @tag";
                                    reader = command.ExecuteReader();
                                    reader.Read();

                                    //Use the tag_id and note_id to link the tag with the note.  
                                    command.CommandText = "insert into NoteTags values (@id,@tagid)";
                                    command.Parameters["tagid"].Value = reader.GetInt32(0);

                                    reader.Close();

                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        //Update the dataGridView to reflect the new entry.  
                        dgvNotesList.Rows[selectedRow].Cells["title"].Value = tbTitle.Text;
                        dgvNotesList.Rows[selectedRow].Cells["body"].Value = tbBody.Text;
                        dgvNotesList.Rows[selectedRow].Cells["updated"].Value = updatedTime;
                        dgvNotesList.Rows[selectedRow].Cells["Tags"].Value = tbTags.Text;
                        clearText();
                    }
                }
                catch(SqlException sqle)
                {
                    MessageBox.Show("There was an issue saving the update to the database: " + sqle.Message);
                }

            } //end if dialogresult == yes

=======
         * NOTES:     This function is called when the pbSaveBttn is clicked. It calls the 
         *            CustomerMessageBox Class and displays a confirmation box to the user to 
         *            cofirm they wish to save the note. If they do, It saves the changes to 
         *            the note in the notes list and notefile
         **************************************************************************************/
        private void pbSaveBttn_Click(object sender, EventArgs e)
        {
            Image lightForward = EasyNote.Properties.Resources.Light_Ok_Button;
            Image darkForward = EasyNote.Properties.Resources.Dark_Ok_Button;
            Image lightBack = EasyNote.Properties.Resources.Light_Cancel_Button;
            Image darkBack = EasyNote.Properties.Resources.Dark_Cancel_Button;
            DialogResult result = CustomMessageBox.Show("Are you sure you wish to save this note?", "Save Note", lightBack, darkBack, lightForward, darkForward);
            //if the use confirms the saving of the note
            if (result == DialogResult.Yes)
            {
                changeButtonView();
                if (currentNote != null && currentNote.Modifiable)
                {
                    currentNote.Title = tbTitle.Text;
                    currentNote.Tags = tbTags.Text.Split(':');
                    currentNote.Body = tbBody.Text;

                    //modify DGV, this is a little hacky
                    int i = notes.IndexOf(currentNote);
                    string[] row = { tbTitle.Text, tbBody.Text, tbTags.Text.Replace(":", ", ") };
                    dgvNotesList.Rows.RemoveAt(i);
                    dgvNotesList.Rows.Insert(i, row);

                    writeNotesFile();
                    currentNote = null;
                }
                clearText();
            }
>>>>>>> refs/remotes/origin/master
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
            Image SaveButton = EasyNote.Properties.Resources.Light_Save_Button;
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
            Image SaveButton = EasyNote.Properties.Resources.Dark_Save_Button;
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
            currentNote = null;
            changeButtonView();
            clearText();
        }

        /**********************Updated for Assignment 3****************************************************************
         * FUNCTION:  private void pbDeleteBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value, but the dataGridView may have a row be
         *            removed.  The database will also have this value deleted.  
         *
<<<<<<< HEAD
         * NOTES:     This function is called when the pbSaveBttn is clicked
         *            It deletes the currently selected row from the datagridview and the currently
         *            selected note from that row from the database.  
         *
         **************************************************************************************/
        private void pbDeleteBttn_Click(object sender, EventArgs e)
        {
            //Create the images for the custom message box, then display this message box to the user to determine if they want to delete this note.  
            Image lightForward = EasyNote.Properties.Resources.Light_Delete_Button;
            Image darkForward = EasyNote.Properties.Resources.Dark_Delete_Button;
            Image lightBack = EasyNote.Properties.Resources.Light_Cancel_Button;
            Image darkBack = EasyNote.Properties.Resources.Dark_Cancel_Button;

            DialogResult result = CustomMessageBox.Show("Are you sure you wish to delete this note?", "Add Note", lightBack, darkBack, lightForward, darkForward);

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
                        using (command = new SqlCommand("delete from NoteTags where note_id = @id", connection))
                        {
                            command.Parameters.AddWithValue("id", selectedNote);
                            command.ExecuteNonQuery();

                            //Then delete the note itself.  
                            command.CommandText = "delete from Notes where note_id = @id";
                            command.ExecuteNonQuery();
                        }

                        clearText();

                        //Hide the save/cancel/delete button.  
                        changeButtonView();

                        //Remove the row for the dgv to keep the database and local table in sync.  
                        dgvNotesList.Rows.RemoveAt(selectedRow);
                    }
                }
                catch(SqlException sqle)
                {
                    MessageBox.Show("There was an issue with deleting from the database: " + sqle.Message);
                }
            }
           
=======
         * NOTES:     This function is called when the pbSaveBttn is clicked.  It calls the 
         *            CustomerMessageBox Class and displays a confirmation box to the user to 
         *            cofirm they wish to delete the note. If they do, It deleted the currently
         *            selected note from the notes list and notefile
         **************************************************************************************/
        private void pbDeleteBttn_Click(object sender, EventArgs e)
        {
            Image lightForward = EasyNote.Properties.Resources.Light_Ok_Button;
            Image darkForward = EasyNote.Properties.Resources.Dark_Ok_Button;
            Image lightBack = EasyNote.Properties.Resources.Light_Cancel_Button;
            Image darkBack = EasyNote.Properties.Resources.Dark_Cancel_Button;
            DialogResult result = CustomMessageBox.Show("Are you sure you wish to delete this note?", "delete Note", lightBack, darkBack, lightForward, darkForward);
            //if user confirmed the deletion of the note
            if (result == DialogResult.Yes)
            {
                changeButtonView();
                if (currentNote != null && currentNote.Modifiable)
                {
                    int i = notes.IndexOf(currentNote);

                    //remove note from list
                    notes.RemoveAt(i);

                    //remove note from DGV
                    dgvNotesList.Rows.RemoveAt(i);

                    writeNotesFile();
                    currentNote = null;
                }
                clearText();
            }
>>>>>>> refs/remotes/origin/master
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
        * FUNCTION:  private void getDllNotes
        *
        * ARGUMENTS: none
        *
        * RETURNS:   This function has no return value
        *
        * NOTES:     This function fetches the notes stored in a referenced dll using a
        *             stringbuilder, save result to string, and string reader loop and then
        *             calls the addNewNote method to add the titles, bodies, and tags to the
        *             Notes List.
        **************************************************************************************/
        private void getDllNotes()
        {
            Notes.NoteComponents n = new Notes.NoteComponents();

            for (int i = 0; i < n.Count; i++)
            {
                //send title, body, tags, and false to modifier args
                this.addNewNote(n[i].Title, n[i].Body, String.Join(":", n[i].Tags.ToArray()), false);
            }
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

        /****************************New for Assignment 3***********************************************
        * FUNCTION:   private void tbSearch_TextChanged(object sender, EventArgs e)
        *
        * ARGUMENTS: sender - object that is calling the function
        *            e - any arguments pass for the event
        *
        * RETURNS:   This function has no return value
        *
        * NOTES:     This function is called when the text of tbSearch changes.  It sets the the clear 
        *            button visiblity appropriately, filters the results displayed in the DataGridView,
        *            and isplays the number of numbers that have tags containing the search text entered.   
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
                    if ((row.Cells[2]).Value.ToString().ToLower().Contains(tbSearch.Text.ToLower()))
                    {
                        dgvNotesList.Rows[row.Index].Visible = true;
                        count++;
                    }
                    //otherwise hide the row
                    else
                    {
                        dgvNotesList.Rows[row.Index].Visible = false;
                    }
                }
                //assign string with number of mathcing notes to lbTagsFound and display it
                lbMatching.Text = "Number of matchs: " + count.ToString();
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
        /**************************New for Assignment 3************************************************************
         * FUNCTION:  private void pbClearButton_MouseEnter(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the mouse is moved over pbClearButton and changes
         *            the displayed image
         **************************************************************************************/
        private void pbClearBtn_MouseEnter(object sender, EventArgs e)
        {
            Image ClearButton = EasyNote.Properties.Resources.Light_Clear_Button;
            pbClearBtn.Image = ClearButton;
        }
        /************************************New for Assignment 3*********************************************
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
            Image ClearButton = EasyNote.Properties.Resources.Dark_Clear_Button;
            pbClearBtn.Image = ClearButton;
        }
    }
}
