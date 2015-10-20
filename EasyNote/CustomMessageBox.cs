/********************************New for Assignment 3*************************************
 * CLASS:     CustomMessageBox
 * AUTHORS:   Robert Kahren II
 *            
 *
 * NOTES:     Windows Form Class use to display a confirmation message boxes
 **************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyNote
{
    public partial class CustomMessageBox : Form
    {
        private Image darkBackBtn;                  //non selected back button image
        private Image lightBackBtn;                 //selected back button image
        private Image darkForwardBtn;               //non selected forward button image
        private Image lightForwardBtn;              //selected forward button image
        /*****************************New for Assignment 3************************************************
         * FUNCTION:  CustomMessageBox()
         *
         * ARGUMENTS: This function receives no arguments
         *
         * RETURNS:   This function has no return value
         *
         * NOTES:     Default constructor
         *            Initializes components
         **************************************************************************************/
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        static CustomMessageBox MsgBox;                             //instance of CustomMessageBox close to be created
        static DialogResult result = DialogResult.No;               //DialogResults to be returned
        /**************************************New for Assignment 3*******************************************
         * FUNCTION:  public static DialogResult Show(string messageText, string boxText, 
         *            Image lightBackImage, Image darkBackImage, Image lightForwardImage, Image darkForwardImage)
         *
         * ARGUMENTS: messageText - text to be displayed in the message box
         *            boxText - text to be the displayed as the title of the message box
         *            lightFowrdImage - image to be displayed on the forward button when it is selected
         *            darkForwardImage - image to be displayed on the forward button when it is not selected
         *            lightBackImage - image to be displayed on the back button when it is selected
         *            darkBackdImage - image to be displayed on the back button when it is not selected
         *
         * RETURNS:   DialogResults - an instance of the DialoResult class that contain the result of the
         *            user's choice
         *
         * NOTES:     This function creates a instance of the CustomMessageBox class, sets the text labels,
         *            title, and images for the button and then displays the message box.  When the box is
         *            closed, it returns the value of the user's choice.
         **************************************************************************************/
        public static DialogResult Show(string messageText, string boxText, Image lightBackImage, Image darkBackImage, Image lightForwardImage, Image darkForwardImage)
        {
            MsgBox = new CustomMessageBox();
            MsgBox.lbMessage.Text = messageText;
            MsgBox.lightBackBtn = lightBackImage;
            MsgBox.darkBackBtn = darkBackImage;
            MsgBox.lightForwardBtn = lightForwardImage;
            MsgBox.darkForwardBtn = darkForwardImage;
            MsgBox.Text = boxText;
            MsgBox.btBack.BackgroundImage = darkBackImage;
            MsgBox.btForward.BackgroundImage = darkForwardImage;
            MsgBox.ShowDialog();
            return result;
        }
        /**************************New for Assignment 3*******************************************
        * FUNCTION:  private void btForward_Click(object sender, EventArgs e)
        *
        * ARGUMENTS: sender - object that is calling the function
        *            e - any arguments pass for the event
        *
        * RETURNS:   This function has no return value
        *
        * NOTES:     This function is called when the forward button is clicked
                     It sets the return DailogResults to Yes and closes the message box
        **************************************************************************************/
        private void btForward_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            MsgBox.Close();
        }
        /***************************New for Assignment 3******************************************
       * FUNCTION:  private void btBack_Click(object sender, EventArgs e)
       *
       * ARGUMENTS: sender - object that is calling the function
       *            e - any arguments pass for the event
       *
       * RETURNS:   This function has no return value
       *
       * NOTES:     This function is called when the back button is clicked
       *            It sets the return DailogResults to No and closes the message box
       **************************************************************************************/
        private void btBack_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
        }
        /***************************New for Assignment 3*******************************************
        * FUNCTION:  private void btForward_MouseEnter(object sender, EventArgs e)
        *
        * ARGUMENTS: sender - object that is calling the function
        *            e - any arguments pass for the event
        *
        * RETURNS:   This function has no return value
        *
        * NOTES:     This function is called when the mouse is moved over btForward and changes
        *            the displayed image and sets the background color to transparent
        **************************************************************************************/
        private void btForward_MouseEnter(object sender, EventArgs e)
        {
            btForward.BackgroundImage = lightForwardBtn;
            btForward.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }
        /****************************New for Assignment 3******************************************
        * FUNCTION:  private void btForward_MouseLeave(object sender, EventArgs e)
        *
        * ARGUMENTS: sender - object that is calling the function
        *            e - any arguments pass for the event
        *
        * RETURNS:   This function has no return value
        *
        * NOTES:     This function is called when the mouse is off of btBack and changes
        *            the displayed image
        **************************************************************************************/
        private void btForward_MouseLeave(object sender, EventArgs e)
        {
            btForward.BackgroundImage = darkForwardBtn;
        }
        /*****************************New for Assignment 3****************************************
       * FUNCTION:  private void btBack_MouseEnter(object sender, EventArgs e)
       *
       * ARGUMENTS: sender - object that is calling the function
       *            e - any arguments pass for the event
       *
       * RETURNS:   This function has no return value
       *
       * NOTES:     This function is called when the mouse is moved over btBack and changes
       *            the displayed image and sets the background color to transparent
       **************************************************************************************/
        private void btBack_MouseEnter(object sender, EventArgs e)
        {
            btBack.BackgroundImage = lightBackBtn;
            btBack.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }
        /***************************New for Assignment 3****************************************
        * FUNCTION:  private void btBack_MouseLeave(object sender, EventArgs e)
        *
        * ARGUMENTS: sender - object that is calling the function
        *            e - any arguments pass for the event
        *
        * RETURNS:   This function has no return value
        *
        * NOTES:     This function is called when the mouse is off of btBack and changes
        *            the displayed image
        **************************************************************************************/
        private void btBack_MouseLeave(object sender, EventArgs e)
        {
            btBack.BackgroundImage = darkBackBtn;
        }
    }
}
