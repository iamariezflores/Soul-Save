using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveSoul
{
    public partial class LoadingDownload : Form
    {
        public LoadingDownload() {
            InitializeComponent();
        }

        private void LoadingDownload_Load(object sender, EventArgs e) {
            this.Owner = DropBoxMain.ActiveForm;

            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            for (int i = 0; i < progressBar1.Maximum; i++) {
                progressBar1.Value = i;
            }

        }

        public void bnClose_Click(object sender, EventArgs e) {
            File.Delete(DropBoxMain.downloadedFile);
            this.Hide();
        }

        internal Action<object, EventArgs> bnClose_Click() {
            throw new NotImplementedException();
        }
    }
}
