using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCGT_SignTranslator.GTaylor.Forms.Modal
{
    public partial class ResourcePackDialog : Form
    {
        public string ReturnPath { get; set; }

        public ResourcePackDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BundledPackButton_Click(object sender, EventArgs e)
        {
            ReturnPath = "bundled";
            this.Close();
        }

        private void ExternalPackButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Resource Packs|*.zip";
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReturnPath = OFD.FileName;
                this.Close();
            }
        }
    }
}
