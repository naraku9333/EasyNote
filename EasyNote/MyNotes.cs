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


namespace EasyNote
{
    public partial class MyNotes : Form
    {
        public const String FILE_LOCATION = "notefile.txt";
        
        private List<Note> notes = new List<Note>();

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
            readNotesFile();
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
            string text = tBTags.Text;

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
                StreamReader reader = new StreamReader(FILE_LOCATION);

                //Read the notes file, splitting the line into its parts.  Then, use these parts to construct a note.  
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Split('*');

                    name = line[0];
                    body = line[1];
                    tags = line[2];

                    notes.Add(new Note(name, body, Note.splitTags(tags)));

                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error processing the file: " + e.Message);
                Environment.Exit(-1);
            }

        }

        /**************************************************************************************
         * FUNCTION:  private void createNoteTable()
         * 
         * ARGUMENTS: none
         * 
         * RETURNS:   This function has no return value
         * 
         * NOTES:     This function adds the note data to a DataTable which is added to
         *            the DataGridView
         **************************************************************************************/
        private void createNoteTable()
        {
            DataTable noteTable = new DataTable();

            //Create the headers for the columns as strings.
            noteTable.Columns.Add("Title");
            noteTable.Columns.Add("Text");
            noteTable.Columns.Add("Tags");

            //Add a new row for every note.  The row will contain information on the 
            //title, body, and tags (joined by :) of the note
            foreach (Note n in notes)
            {
                String [] row = { n.Title, n.Body, n.getTagString() };
                noteTable.Rows.Add(row);
            }

            //Display the noteTable in a data grid form.  
            dgvNotesList.DataSource = noteTable;

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
         * FUNCTION:  private void pbAddNote_Click(object sender, EventArgs e)
         * 
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         * 
         * RETURNS:   This function has no return value
         * 
         * NOTES:     NOT-IMPLEMENTED
         **************************************************************************************/
        private void pbAddNote_Click(object sender, EventArgs e)
        {
        }
    }
}
