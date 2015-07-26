using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCGT_SignTranslator
{
    public partial class LanguageSelectionItem : UserControl
    {
        public LanguageSelectionItem()
        {
            InitializeComponent();
        }
        public void Initialise(Image langIcon, string langName)
        {
            this.languageName.Text = langName;
            languageImage.Image = langIcon;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            this.BackColor = Color.Red;
        }
    }
    
}
