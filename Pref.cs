using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.DirectoryServices;

namespace SaveSoul
{
    public partial class Pref : Form
    {
        public Pref() {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if(txtDarkSoulsPath.Text == ""){
                MessageBox.Show("DarkSoul Paths are Empty", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if(txtDarkSouls2Path.Text == ""){
                MessageBox.Show("DarkSoul 2 Paths are Empty", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if(txtDarkSouls3Path.Text == "") {
                MessageBox.Show("DarkSoul 3 Paths are Empty", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if(txtSekiroPath.Text == "") {
                MessageBox.Show("Sekiro Paths are Empty", "SoulSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (MessageBox.Show("Save Continue?, Verify that the Paths are Correct before Saving", "SoulSave", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                File.WriteToTextFile(txtDarkSoulsPath.Text, "paths.ini");
                File.WriteToTextFile(txtDarkSouls2Path.Text, "paths.ini");
                File.WriteToTextFile(txtDarkSouls3Path.Text, "paths.ini");
                File.WriteToTextFile(txtSekiroPath.Text, "paths.ini");

                this.Close();
            }
        }

        private void btnBrowse0_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                txtDarkSoulsPath.Text = folderBrowserDialog.SelectedPath;
                Environment.SpecialFolder root = folderBrowserDialog.RootFolder;
            }
        }

        private void Pref_Load(object sender, EventArgs e) {
            //DarkSouls Remastered
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pathDarkSouls = myDocuments + @"\NBGI\DARK SOULS REMASTERED";
            txtDarkSoulsPath.Text = pathDarkSouls;

            //Dark Souls II
            string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string pathDarkSouls2 = AppData + @"\DarkSoulsII";
            txtDarkSouls2Path.Text = pathDarkSouls2;

            //DarkSouls III
            string pathDarkSouls3 = AppData + @"\DarkSoulsIII";
            txtDarkSouls3Path.Text = pathDarkSouls3;

            //Sekiro
            string pathSekiro = AppData + @"\Sekiro";
            txtSekiroPath.Text = pathSekiro;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnBrowse1_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                txtDarkSouls2Path.Text = folderBrowserDialog.SelectedPath;
                Environment.SpecialFolder root = folderBrowserDialog.RootFolder;
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                txtDarkSouls3Path.Text = folderBrowserDialog.SelectedPath;
                Environment.SpecialFolder root = folderBrowserDialog.RootFolder;
            }
        }

        private void btnBrowse3_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                txtSekiroPath.Text = folderBrowserDialog.SelectedPath;
                Environment.SpecialFolder root = folderBrowserDialog.RootFolder;
            }
        }
    }
}
