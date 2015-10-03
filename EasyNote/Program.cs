using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;

namespace EasyNote
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(noteUnhandledExceptionEvent);

            Application.Run(new MyNotes());
        }

        /**************************************************************************************
         * FUNCTION:  private static void noteUnhandledExceptionEvent(object sender, ThreatExceptionEventArgs t)
         * 
         * ARGUMENTS: sender - object that is calling the function
         *            t - the event information including the exception.  
         * 
         * OUTPUT:   Information about the error that caused this event will be placed in the logging.txt file.
         * 
         * EXIT CONDITIONS:  The program may be restarted if the user requests so, or the program may end here.  
         * 
         **************************************************************************************/
        private static void noteUnhandledExceptionEvent(object sender, ThreadExceptionEventArgs t)
        {
            //Create the logging file and write the error information to it.  
            using(StreamWriter errorOutput = new StreamWriter("logging.txt", true) )
            {
                errorOutput.WriteLine("Exception occured on " + DateTime.Now.ToString() + " at " + t.Exception.Source
                    + " in method " + t.Exception.TargetSite);
                errorOutput.WriteLine("Data values are as follows:");
                foreach (DictionaryEntry data in t.Exception.Data)
                {
                    errorOutput.WriteLine("\t" + data.Key + ' ' + data.Value);
                }
            }

            //Let the user choose to restart the program or not.  
            DialogResult answer = MessageBox.Show("An error has occured that has caused this program to crash.\n" +
                            "The reason for this was : " + t.Exception.Message + "\n" +
                            "Would you like to restart this program?", "Easy Note Error", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                //Start a new process using the aboslute path to the file that was used to create this process.
                //Only works without debugging mode because it will try to use the debug exe file which will not start
                //on its own.  
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            }

            Process.GetCurrentProcess().Kill();
        }
    }    
}
