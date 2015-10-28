using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyNote
{
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        public void changePicture(byte[] attachment)
        {
            MemoryStream m = new MemoryStream(attachment);
            Bitmap picture = new Bitmap(m);
            pictureBox1.Image = picture;
            pictureBox1.Width = picture.Width;
            pictureBox1.Height = picture.Height;
            this.Height = picture.Height;
            this.Width = picture.Width;
            this.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
