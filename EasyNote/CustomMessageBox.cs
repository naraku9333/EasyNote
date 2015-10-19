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
        private Image darkBackBtn;
        private Image lightBackBtn;
        private Image darkForwardBtn;
        private Image lightForwardBtn;
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        static CustomMessageBox MsgBox;
        static DialogResult result = DialogResult.No;
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

        private void btForward_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            MsgBox.Close();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
        }

        private void btForward_MouseEnter(object sender, EventArgs e)
        {
            btForward.BackgroundImage = lightForwardBtn;
        }

        private void btForward_MouseLeave(object sender, EventArgs e)
        {
            btForward.BackgroundImage = darkForwardBtn;
        }

        private void btBack_MouseEnter(object sender, EventArgs e)
        {
            btBack.BackgroundImage = lightBackBtn;
        }

        private void btBack_MouseLeave(object sender, EventArgs e)
        {
            btBack.BackgroundImage = darkBackBtn;
        }
    }
}
