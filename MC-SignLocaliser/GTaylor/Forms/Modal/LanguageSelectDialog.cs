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
    public partial class LanguageSelectDialog : Form
    {
        public string ReturnLanguage { get; set; }

        public LanguageSelectDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            ReturnLanguage = LanguageBox.Text;
            this.Close();
        }
    }
}
