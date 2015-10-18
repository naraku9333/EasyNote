/**************************************************************************************
 * CLASS:     MyNotes
 * AUTHORS:   Cierria McPerryman
 *            Sean Vogel
 *            Robert Kahren
 *            Anthony Malmgren
 *
 * NOTES:     Main application form
 *            reads note file and fills DataGridView
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

        private Note currentNote = null;//currently selected note iff its a stored note

        private SqlConnection connection = null;    //Holds the connection to the database, using conString.

        private SqlCommand command = null;          //Holds a command to be executed on the database.  

        private SqlDataReader reader = null;        //Holds the result of a query from the database.  

        //The connection string to use for connecting to the notebase2 database.  
        private string conString = "server=10.158.56.48;uid=net2;pwd=dtbz2;database=notebase2;";

        DataSet dataSet = null;

        /**************************************************************************************
         * FUNCTION:  MyNotes()
         *
         * ARGUMENTS: None
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     Default constructor
         *            Initializes components, reads data file and calls createNoteTable
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

                        name = line[0];
                        body = line[1];
                        tags = line[2];

                        this.addNewNote(line[0], line[1], line[2]);
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
                        //Create a dataset from our query and fill the a table with this information. 
                        dataSet = new DataSet("noteData");
                        DataTable notesTable = new DataTable();
                        data.Fill(dataSet);
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
                MessageBox.Show(e.Message + " " + e.ToString());
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
         * RETURNS:   This function has no return value, but the notes list will be modified to
         *            have a new note with the entered values.
         *
         * NOTES:    AddNewNote not only adds the note, but it also does the exception checking for
         *           it as well.
         **************************************************************************************/
        private void addNewNote(string title, string text, string tagString, bool mod = true)
        {
            try
            {
                String[] tags = Note.splitTags(tagString);
                notes.Add(new Note(title, text, tags, mod));  //CEdge add modifiable arg
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
        }

        /**************************************************************************************
         * FUNCTION:  private void pbAddNote_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     Add new notes to note list and notfile
         **************************************************************************************/
        private void pbAddNote_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text != "" && tbBody.Text != "")
            {
                this.addNewNote(tbTitle.Text, tbBody.Text, tbTags.Text);

                using (StreamWriter sw = File.AppendText(FILE_LOCATION))
                {
                    sw.WriteLine(tbTitle.Text + "*" + tbBody.Text + "*" + tbTags.Text);
                }

                string[] row = { tbTitle.Text, tbBody.Text, tbTags.Text.Replace(":", ", ") };
                dgvNotesList.Rows.Add(row);

                clearText();
            }
        }

        /**************************************************************************************
         * FUNCTION:  private dgvNotesList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value, but textFields in the program will be modified.
         *
         * NOTES:     This function takes the values for the note selected in the dataGridView and copies
         *            them into textFields for the user to modify.  
         **************************************************************************************/
        private void dgvNotesList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Show the save,delete, and cancel buttons.  
            changeButtonView();

            //Get the select cell from the dataGridView.  
            int i = e.RowIndex;
            int j = e.ColumnIndex;

            //If the column or line headers were double clicked, do nothing
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            //Grab the title, body, and tags associated with the selected note and put them in
            //textfields for the user to see.  
            tbTitle.Text = dgvNotesList.Rows[i].Cells["title"].Value as string;
            tbBody.Text = dgvNotesList.Rows[i].Cells["body"].Value as string;
            tbTags.Text = dgvNotesList.Rows[i].Cells["Tags"].Value as string;
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

        /**************************************************************************************
         * FUNCTION:  private void pbSaveBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the pbSaveBttn is clicked
         *            It saves the changes to the note in the notes list and notefile
         **************************************************************************************/
        private void pbSaveBttn_Click(object sender, EventArgs e)
        {
            changeButtonView();
            if(currentNote != null && currentNote.Modifiable)
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

        /**************************************************************************************
         * FUNCTION:  private void pbDeleteBttn_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the pbSaveBttn is clicked
         *            It deleted the currently selected note from the notes list
         *            and notefile
         **************************************************************************************/
        private void pbDeleteBttn_Click(object sender, EventArgs e)
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
    }
}
