using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoulSave
{
    partial class AboutBox : Form
    {
        public AboutBox() {
            InitializeComponent();
           
        }

        private void AboutBox_Load(object sender, EventArgs e) {
            //Stop the textbox1 from being selected/highlighted
            textBox1.TabStop = false;
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e) {

        }
    }
}
