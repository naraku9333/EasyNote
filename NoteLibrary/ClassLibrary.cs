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
        public string Title { get; set; }
        public string Body { get; set; }
        public string[] Tags { get; set; }
        public bool Modifiable { get; set; }

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
        public Note(string title, string body, string[] tags)
        {
            this.Title = title;
            this.Body = body;
            this.Tags = tags;
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
            if (tags == "to-do")
            {
                throw new NoteException();
            }
            else if (tags == "meeting")
            {
                //Force a divide by 0 error.  
                int zero = 0;
                int error = 1 / zero;

            }
            else if (tags == "grades")
            {
                try
                {
                    //Force a divide by 0 error.
                    int zero = 0;
                    int error = 1 / zero;
                }
                catch (DivideByZeroException e)
                {
                    throw new NoteException("An exception with spliting the tags was encountered.", e, tags);
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
