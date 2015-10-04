/**************************************************************************************
 * CLASS:     Note
 * AUTHORS:   Anthony Malmgren
 * 
 * NOTES:     class library with utility function to split tag string
 **************************************************************************************/
using System;
using System.Text;

namespace NoteLibrary
{
    public class Note
    {
        public string Title { get; set; }          //The title to be assigned to a note
        public string Body { get; set; }           //The text held in the body of the note.
        public string[] Tags { get; set; }         //An array of tags to be associated with the note.
        public bool Modifiable { get; set; }       //A flag for whether or not this note can be changed.  

        /**************************************************************************************
         * FUNCTION:  public Note(string title, string body, string[] tags)
         * 
         * ARGUMENTS: title - note title
         *            body - note body
         *            tags - array of note tags
         * 
         * RETURNS:   This function has no return value
         * 
         * NOTES:     Constructor
         *            Sets values of data members
         **************************************************************************************/
        public Note(string title, string body, string[] tags, bool mod)
        {
            this.Title = title;
            this.Body = body;
            this.Tags = tags;
            this.Modifiable = mod; //CEdge chang add modifiable set
            
        }

        /**************************************************************************************
         * FUNCTION:  public override string ToString()
         * 
         * ARGUMENTS: None
         * 
         * RETURNS:   string representation of a note
         * 
         * NOTES:     creates and returns a string with all note data
         **************************************************************************************/
        public override string ToString()
        {
            return Title + " " + Body + " " + getTagString();
        }

        /**************************************************************************************
         * FUNCTION:  public string getTagString()
         * 
         * ARGUMENTS: None
         * 
         * RETURNS:   string of tags
         * 
         * NOTES:     returns string of tags seperated by colons
         **************************************************************************************/
        public string getTagString()
        {
            return String.Join(":", Tags);
        }

        /**************************************************************************************
         * FUNCTION:  public static string[] splitTags(String tags)
         * 
         * ARGUMENTS: tags - string of tags
         * 
         * RETURNS:   string[] array of tags
         * 
         * NOTES:     returns an array of the tags linked to the note
         **************************************************************************************/
        public static string[] splitTags(String tags)
        {
            //If we encounter a tags of "to-do" just throw a basic note exception.  
            if (tags == "to-do")
            {
                throw new NoteException();
            }
            //If we encounter a tags of just "meeting", force a divide by zero error and let it propagate up the program stack.  
            else if (tags == "meeting")
            {
                int zero = 0;
                int error = 1 / zero;
            }
            //If we encounter a tags of just "grades", then we will cause a divide by zero exception and use that as an
            //inner exception for a new NoteException. 
            else if (tags == "grades")
            {
                try
                {
                    int zero = 0;
                    int error = 1 / zero;
                }
                catch (DivideByZeroException e)
                {
                    throw new NoteException("An exception with spliting the tags was encountered: ", e, tags);
                }
            }

            return tags.Split(':');
        }        
    }

    public class NoteException : Exception
    {
        private string tag;
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public NoteException() : base() { }
        public NoteException(string message) : base(message) { }
        public NoteException(string message, Exception innerException) : base(message, innerException) { }
        public NoteException(string message, Exception innerException, string tag)
            : base(message, innerException)
        {
            this.Tag = tag;
        }
    }
}
