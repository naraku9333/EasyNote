using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(noteUnhandledExceptionEvent);

            Application.Run(new MyNotes());
        }

        private static void noteUnhandledExceptionEvent(object sender, ThreadExceptionEventArgs t)
        {
            try
            {
                StreamWriter errorOutput = new StreamWriter("logging.txt", true);

                errorOutput.WriteLine("Exception occured on " + DateTime.Now.ToString() + " at " + t.Exception.Source
                    + " with target " + t.Exception.TargetSite + Environment.NewLine + "Data values are as follows:");
                foreach (KeyValuePair<string, string> data in t.Exception.Data)
                {
                    errorOutput.WriteLine(data.Key + ' ' + data.Value);
                }
                errorOutput.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

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
