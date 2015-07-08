using fNbt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBT_Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog OFD = new OpenFileDialog();
        private void onClickLoad(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadNBT(OFD.FileName);
            }
        }

        private void LoadNBT(string path)
        {
            var myFile = new NbtFile();
            myFile.LoadFromFile(path);
            NbtCompound myCompoundTag = myFile.RootTag;
            foreach (NbtTag tag in myCompoundTag.Tags)
            {
                Console.WriteLine(tag.Name + " = " + tag.TagType);
            }
            NbtCompound dataTag = myCompoundTag.Get<NbtCompound>("Data");
            foreach (NbtTag tag in dataTag.Tags)
            {
                Console.WriteLine(tag.Name + " = " + tag.TagType);
            }
            Console.WriteLine(myFile.FileName + ":" + myFile.FileCompression.ToString());
        }
    }
}
