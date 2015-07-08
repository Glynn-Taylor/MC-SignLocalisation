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
    public partial class ListControl : UserControl
    {
        public ListControl()
        {
            InitializeComponent();
        }
        public void  Add(Control c){
            flpListBox.Controls.Add(c);
            SetupAnchors();
        }

        public void Remove(string name){
            Control c = flpListBox.Controls[Name];
            flpListBox.Controls.Remove(c);
            c.Dispose();
            SetupAnchors();
        }

        public void Clear(){
            while (flpListBox.Controls.Count > 0)
            {
                Control c = flpListBox.Controls[0];
                flpListBox.Controls.Remove(c);
                c.Dispose();
            }
        }

        public int Count{
            get{
                return flpListBox.Controls.Count;
            }
        }
        void SetupAnchors()
        {
            if (this.flpListBox.Controls.Count > 0)
            {
                for (int i = 0; i < flpListBox.Controls.Count; i++)
                {
                    Control c = flpListBox.Controls[i];
                 
                        c.Anchor = AnchorStyles.Left & AnchorStyles.Top;
                  
                }
            }
        }

        private void OnLayout(object sender, LayoutEventArgs e)
        {
            for (int i = 0; i < flpListBox.Controls.Count; i++)
            {
                flpListBox.Controls[i].Width = flpListBox.Size.Width - SystemInformation.VerticalScrollBarWidth;

            }
               
        }
    }
}
