using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    private SqlConnection connection = null;    //Holds the connection to the database, using conString.

    //The connection string to use for connecting to the notebase2 database.  
    private const string conString = "server=10.158.56.48;uid=net2;pwd=dtbz2;database=notebase2;";

    private int selectedRow;                //The current row selected in the dgv

    private string filename = null;         //filename of attachment, for use with attachment table

    private byte[] attachment = null;       //Holds the provided attachment as its individual bytes, for use with attachment table.  
    private DataTable notesTable = null;    //The source for the dgv that holds queried data from the database.  
    private bool changingValue = false;     //A flag for a value being either updated/viewed/deleted.  It is used to prevent the switching
                                            //of the save, delete, and cancel buttons when selecting another value in the table.  It is
                                            //reset after save,delete, or cancel is clicked.  
    enum View { Add, Save }

    protected void Page_Load(object sender, EventArgs e)
    {
        createNoteTable();

    }

    /******************************Updated for Assignment 4********************************
    * FUNCTION:  private void createNoteTable()
    *
    * ARGUMENTS: none
    *
    * RETURNS:   This function has no return value
    *
    * NOTES:     This function gets the note and tag data from the Notes and Tags tables in
    *            the database and places them into a gridview 
    **************************************************************************************/
    private void createNoteTable()
    {
        //Try to execute the following Sql statements
        try
        {
            ////Connect to the notebase2 database.                  
            using (connection = new SqlConnection(conString))
            {
                //Use the notedisplay stored procedure to generate the data for the noteTable.  
                using (var com = new SqlCommand("notedisplay", connection) { CommandType = CommandType.StoredProcedure })
                {
                    com.Connection = connection;
                    notesTable = new DataTable();

                    //Fill the dgv with the data from the notesTable.   
                    using (var adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(notesTable);

                        //Create a view from this notesTable to apply sorting and filtering, then use that as the source
                        //for the notes list.  
                        DataView noteSource = new DataView(notesTable);

                        if (Session["sortString"] != null)
                        {
                            noteSource.Sort = Session["sortString"] as string;
                        }

                        dgvNotesList.DataSource = noteSource;
                        dgvNotesList.DataBind();
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Response.Write("Creating the DataGridView of the notes list failed:" + e.Message + "\n" + e.ToString());
        }
    }

    /********************************New for Assignment 4******************************
        * FUNCTION:  protected void dgvNotesList_Sorting(object sender, GridViewSortEventArgs e)
        *
        * ARGUMENTS: sender - object that is calling the function
        *            e - the sorting arguments, including the direction and column.  
        *
        * RETURNS:   No return value, but the dgvNotesList will be sorted based on the column selected
        *            and the sort direction given.   
        *
        * NOTES:     This event is fired when any hyperlink is clicked on the dgvNotesList girdview.  
        *            The event grabs the given sort direction and transforms it into a string, as well
         *           as the sort expression (Column id).  These two arguments are used to set a DataView's
        *            Sort property.  This updated table is then shown by changing the dgvNotesList DataSource.  
        **************************************************************************************/
    protected void dgvNotesList_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortString = "ASC";          //The direction of the sort as a string.  

        //If we already sorted once, flip the sort order.  
        if (Session[e.SortExpression] != null && (String)Session[e.SortExpression] == "ASC")
            sortString = "DESC";

        //Remember the last direction we sorted for this column.
        Session[e.SortExpression] = sortString;

        //Create and remember the specified sort.  
        sortString = e.SortExpression + ' ' + sortString;
        Session["sortString"] = sortString;

        //Apply the sort and update the webpage.  
        using (DataView sortedTable = new DataView(notesTable))
        {
            sortedTable.Sort = sortString;
            dgvNotesList.DataSource = sortedTable;
            dgvNotesList.DataBind();
        }

    }

    /*******************************Updated for Assignment 4*******************************
    * FUNCTION:  private void pbSaveBttn_Click(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   This function has no return value, but the changes will appear in the 
    *            gridview for the selected note and the database will be updated.  
    *
    * NOTES:     This function is called when the pbSaveBttn is clicked
    *            It saves the changes to the note in the dgridview and to the database using
    *            a stored procedure.  
    *            
    **************************************************************************************/
    public void pbSaveBttn_Click(object sender, EventArgs e)
    {        
        //Remove the save / cancel button.  
        changeButtonView(View.Save);
        try
        {
            int selectedNote = (int)Session["selectedNote"];
            using (connection = new SqlConnection(conString))
            {
                //Use the updatenote stored procedure to update the note with the entered values from the textboxes.  
                using (var com = new SqlCommand("updatenote", connection) { CommandType = CommandType.StoredProcedure })
                {               
                    com.Connection = connection;
                    com.Parameters.AddWithValue("@noteid", selectedNote);
                    com.Parameters.AddWithValue("@title", tbTitle.Text);
                    com.Parameters.AddWithValue("@body", tbBody.Text);
                    com.Parameters.AddWithValue("@tags", tbTags.Text);

                    //Update the local table to show the updated note/tags.  
                    using (var adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(notesTable);
                        createNoteTable();
                    }
                }
            }
            attachFile(selectedNote);
            clearText();
        }
        catch (SqlException sqle)
        {
            Response.Write(sqle.Message);
        }
        changingValue = false;
    } //end if dialogresult == yes  

    /**************************Updated for Assignment 4************************************
     * FUNCTION:  private void changeButtonView()
     *
     * ARGUMENTS: View v - an enmeration used to determine whether to display the attach or
     *                     retrieve button.  It will be set to View.Add for the add button.  
     *
     * RETURNS:   This function has no return value
     *
     * NOTES:     Helper function to swap button images for UI views
     **************************************************************************************/
    private void changeButtonView(View v)
    {        
        //pbCancelBttn.Visible = !pbCancelBttn.Visible;

        if (v == View.Add)
        {
            pbAddNote.Visible = true;
            pbSaveBttn.Visible = false;
            pbDeleteBttn.Visible = false;
            pbAttachBtn.Visible = true;
            pbRetrieveBttn.Visible = false;
            UploadAttachment.Visible = true;
            //pbSelectBttn.Visible = true;
        }
        else
        {
            pbAddNote.Visible = false;
            pbSaveBttn.Visible = true;
            pbDeleteBttn.Visible = true;
            pbAttachBtn.Visible = false;
            pbRetrieveBttn.Visible = true;

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
        tbTags.Text = "";
        tbTitle.Text = "";
        tbBody.Text = "";
        dgvNotesList.SelectedIndex = -1;
    }

    /********************************Updated for Assignment 4******************************
    * FUNCTION:  private dgvNotesList_SelectedIndexChanged(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   No return value, but textFields in the program will be modified.
    *
    * NOTES:     The selectedIndexChanged event for dgvNotesList is fired when any of the
     *           select hyperlinks in the first column of the dgvNotesList is clicked. 
     *           It uses the selecte index (row) to retrieve information from the dgvNotesList
     *           gridview and place it into the Title,Body,and Tags text boxes.  It also places
     *           the selected note from the row into the session, so that it can be used later
     *           to determine what note to modify/delete.  
    **************************************************************************************/
    protected void dgvNotesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Show the save,delete, and cancel buttons. 
        if (!changingValue)
            changeButtonView(View.Save);

        //Grab the title, body, and tags associated with the selected note and put them in
        //textfields for the user to see.  
        selectedRow = dgvNotesList.SelectedIndex;

        tbTitle.Text = dgvNotesList.Rows[selectedRow].Cells[2].Text;
        tbBody.Text = dgvNotesList.Rows[selectedRow].Cells[3].Text;
        tbTags.Text = dgvNotesList.Rows[selectedRow].Cells[6].Text;

        //Grab the id of the note the user selected from the table for later queries.  
        int selectedNote = Convert.ToInt32(dgvNotesList.Rows[selectedRow].Cells[1].Text);
        Session["selectedNote"] = selectedNote;

        //check if this note has an attachment and set the correct button visible
        try
        {
            using (var connection = new SqlConnection(conString))
            {
                using (var com = new SqlCommand("select count(*) from AttachedNotes where note_id = " + selectedNote, connection))
                {
                    com.Connection = connection;
                    connection.Open();
                    int a = (int)com.ExecuteScalar();
                    if (a != 0)
                    {
                        pbRetrieveBttn.Visible = true;
                        pbAttachBtn.Visible = false;
                        UploadAttachment.Visible = false;
                        //pbSelectBttn.Visible = false;
                    }
                    else
                    {
                        pbRetrieveBttn.Visible = false;
                        pbAttachBtn.Visible = true;
                        UploadAttachment.Visible = true;
                        //pbSelectBttn.Visible = true;
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Response.Write("There was an issue adding attachment: " + ex.Message);
        }

        //Set the changingValue flag to prevent the buttons from switching until the user is done modifying a note.  
        changingValue = true;
    }

    /**********************Updated for Assignment 4****************************************
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
    public void pbDeleteBttn_Click(object sender, EventArgs e)
    {
        int selectedNote = (int)Session["selectedNote"];

        try
        {
            //Open a connection
            using (connection = new SqlConnection(conString))
            {
                connection.Open();

                //Remove references to the note in note_tags.  
                using (var command = new SqlCommand("delete from Notes where note_id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", selectedNote);
                    command.ExecuteNonQuery();
                }

                clearText();

                //Hide the save/cancel/delete button.  
                changeButtonView(View.Add);

                //Remove the row for the dgv to keep the database and local table in sync. 
                createNoteTable();
            }
        }
        catch (SqlException sqle)
        {
            Response.Write("There was an issue with deleting from the database: " + sqle.Message);
        }

        changingValue = false;
    }
    /****************************************************************************************
         * FUNCTION:   protected void pbAddNote_Click(object sender, EventArgs e)
         *
         * ARGUMENTS: sender - object that is calling the function
         *            e - any arguments pass for the event
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     This function is called when the pbAddNote button is clicked  It adds the new note to 
         *            the database.
         **************************************************************************************/
    protected void pbAddNote_Click(object sender, EventArgs e)
    {
        //Ensure there is something to add.  
        if (tbTitle.Text != "" && tbBody.Text != "")
        {
            try
            {
                int note_id;
                using (connection = new SqlConnection(conString))
                {
                    //Use the updatenote stored procedure to update the note with the entered values from the textboxes.  
                    using (var com = new SqlCommand("addnote", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        //Grab the title,text, and body from the textboxes for the stored procedure. 
                        com.Connection = connection;
                        com.Parameters.AddWithValue("@note_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        com.Parameters.AddWithValue("@title", tbTitle.Text);
                        com.Parameters.AddWithValue("@body", tbBody.Text);
                        com.Parameters.AddWithValue("@tags", tbTags.Text);

                        //Update the local table to show the updated note/tags.  
                        using (var adapter = new SqlDataAdapter(com))
                        {
                            adapter.Fill(notesTable);
                            createNoteTable();
                        }
                        note_id = int.Parse(com.Parameters["@note_id"].Value.ToString());
                    }
                }         
                attachFile(note_id);
                clearText();
            }
            catch (SqlException sqle)
            {
                Response.Write(sqle.Message);    
            }
        }
        changingValue = false;
    }
    /****************************************************************************************
    * FUNCTION:   protected void pbSearch_Click(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   This function has no return value
    *
    * NOTES:     This function is called when the pbSearch button is clicked.It filters 
    *            the results displayed in the DataGridView, and isplays the number of numbers that have tags 
    *            containing the search text entered.   
    **************************************************************************************/
    protected void pbSearch_Click(object sender, EventArgs e)
    {
        int count = 0;                   //tracks matching number of notes
        //if the user has text entered into the search box
        if (tbSearch.Text != "")
        {

            //check each row in the DataGridView
            foreach (GridViewRow row in dgvNotesList.Rows)
            {
                //if a tag contains the search text, display the row and increament count
                if (row.Cells[6].Text.ToLower().Contains(tbSearch.Text.ToLower()))
                {
                    row.Visible = true;
                    count++;
                }
                //otherwise hide the row
                else
                    row.Visible = false;
            }
            //assign string with number of mathcing notes to lbFound
            lbFound.Text = "Notes Found: " + count;
        }
        else
        {
            //set each row in the DataGridView to visible
            foreach (GridViewRow row in dgvNotesList.Rows)
            {
                row.Visible = true;
            }
            lbFound.Text = "";
        }
    }
    /************************New for Assignment 3******************************************
    * FUNCTION:  private void pbClearButton_Click(object sender, EventArgs e)
    *
    * ARGUMENTS: sender - object that is calling the function
    *            e - any arguments pass for the event
    *
    * RETURNS:   This function has no return value
    *
    * NOTES:     This function is called when the pbClearButton is clicked
    *            It clears the tbSearch and displays all notes
    **************************************************************************************/
    protected void pbClear_Click(object sender, EventArgs e)
    {
        tbSearch.Text = "";
        //set each row in the DataGridView to visible
        foreach (GridViewRow row in dgvNotesList.Rows)
        {
            row.Visible = true;
        }
        lbFound.Text = "";
    }

    protected void pbAttachBtn_Click(object sender, EventArgs e)
    {
        //UploadAttachment.AllowMultiple = false;

       // if (UploadAttachment.HasFile)
        {
            using (Stream fs = UploadAttachment.PostedFile.InputStream)
            {
                Session["filename"] = Path.GetFileName(UploadAttachment.PostedFile.FileName);

                BinaryReader br = new BinaryReader(fs);
                Session["attachment"] = br.ReadBytes((int)fs.Length);
            }
        }
    }

    /********************************New for Assignment 3**********************************
    * FUNCTION:  private void attachFile(int id)
    *
    * ARGUMENTS: id - note id of note to attach to
    *
    * RETURNS:   No return value, but textFields in the program will be modified.
    *
    * NOTES:     This function adds a file attachment to the database and links it to 
    *            the corresponding note.  
    **************************************************************************************/
    private void attachFile(int id)
    {
        string filename = (string)Session["filename"];
        byte[] attachment = (byte[])Session["attachment"];

        if (filename != null && attachment != null)
        {
            try
            {
                using (var connection = new SqlConnection(conString))
                {
                    using (var com = new SqlCommand("addattachment", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        com.Connection = connection;
                        com.Parameters.AddWithValue("@note_id", id);
                        com.Parameters.AddWithValue("@attachment", attachment);
                        com.Parameters.AddWithValue("@filename", filename);

                        connection.Open();
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Response.Write("There was an issue adding attachment: " + e.Message);
            }
            finally
            {
                attachment = null;
                filename = null;
            }
        }
    }

    protected void pbRetrieveBttn_Click(object sender, EventArgs e)
    {
        try
        {
            int selectedNote = (int)Session["selectedNote"];
            //Open a connection
            using (connection = new SqlConnection(conString))
            {
                connection.Open();

                string sql = "select attachment, filename from Attachment, AttachedNotes "
                    + "where attachment.attach_id = AttachedNotes.attach_id and AttachedNotes.note_id = @note_id";

                //Remove references to the note in note_tags.  
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@note_id", selectedNote);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attachment = (byte[])reader.GetSqlBinary(0);
                            filename = reader.GetString(1);
                           
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                            Response.BinaryWrite(attachment);
                            Response.Flush();
                            Response.End();                            

                        }
                    }
                }
            }
        }
        catch (SqlException sqle)
        {
            Response.Write("There was an issue with deleting from the database: " + sqle.Message);
        }
    }

    protected void pbCancelBttn_Click(object sender, ImageClickEventArgs e)
    {
        changeButtonView(View.Add);
        changingValue = false;
        clearText();
    }
}
           