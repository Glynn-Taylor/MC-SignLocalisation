using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCGT_SignTranslator;
using fNbt;
using System.IO;
using MCGT_SignTranslator.GTaylor.Serialization;

namespace MCGT_SignTranslator
{
    public partial class MainForm : Form
    {
        private const int CHUNK_SIZE = 32;
        private const int PAGE_LENGTH = 4096;
        private string[] Languages = { "English", "Deutsch", "Francais", "Espanol" };
        private Image[] LanguageIcons = { Properties.Resources.en1, Properties.Resources.de, Properties.Resources.fr, Properties.Resources.es };
        private Dictionary<Point, NbtFile> LoadedChunks = new Dictionary<Point, NbtFile>();


        public MainForm()
        {
            InitializeComponent();
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void OnLoad(object sender, EventArgs e)
        {
            for (int i = 0; i < Languages.Length; i++)
            {
                LanguageSelectionItem btn = new LanguageSelectionItem();
                btn.Initialise(LanguageIcons[i], Languages[i]);
                this.listControl1.Add(btn);

            }
        }

        //Example of translated sign at r.0.2 [31,0] in test
        public void LoadChunk(NbtFile myFile)
        {
            //Console.WriteLine(myFile.FileName + ":" + myFile.FileCompression.ToString());
            bool KeepLoaded = false;
            NbtCompound chunkTag = myFile.RootTag;
            NbtCompound levelTag = chunkTag.Get<NbtCompound>("Level");
            NbtList tEntitiesTag = levelTag.Get<NbtList>("TileEntities");
            //Chunks
            foreach (NbtCompound entTag in tEntitiesTag)
            {
                NbtString id = entTag.Get<NbtString>("id");
                if (id.StringValue == "Sign")
                {
                    string t1 = entTag.Get<NbtString>("Text1").StringValue;
                    string t2 = entTag.Get<NbtString>("Text2").StringValue;
                    string t3 = entTag.Get<NbtString>("Text3").StringValue;
                    string t4 = entTag.Get<NbtString>("Text4").StringValue;
                    NbtInt x = entTag.Get<NbtInt>("x");
                    NbtInt y = entTag.Get<NbtInt>("y");
                    NbtInt z = entTag.Get<NbtInt>("z");
                    string composite = t1 + t2 + t3 + t4;
                    Console.WriteLine(composite);
                    if (!(String.IsNullOrWhiteSpace(composite) || (composite.Distinct().Count() == 1 && composite[0] == '"')))
                    {
                        TreeNode signNode = new TreeNode("[" + x.IntValue + "," + y.IntValue + "," + z.IntValue + "]" + getFirstText(t1, t2, t3, t4));
                        Color signColor = Color.Green;
                        if (GenerateSignTextNode(signNode, t1) | GenerateSignTextNode(signNode, t2) | GenerateSignTextNode(signNode, t3) | GenerateSignTextNode(signNode, t4))
                            signColor = Color.Red;
                        signNode.BackColor = signColor;
                        Console.WriteLine("adding signNode");
                        treeView1.Nodes.Add(signNode);
                    }
                    KeepLoaded = true;
                }
            }
            if (KeepLoaded)
                LoadedChunks.Add(new Point(levelTag.Get<NbtInt>("xPos").IntValue, levelTag.Get<NbtInt>("zPos").IntValue), myFile);
        }

        private bool GenerateSignTextNode(TreeNode signNode, string text)
        {
            if (text.Contains("\"translate\":"))
            {
                TreeNode node = new TreeNode(text);
                node.BackColor = Color.Green;
                signNode.Nodes.Add(node);
                return false;
            }
            else if (String.IsNullOrWhiteSpace(text) || text.Equals("null"))
            {
                return false;
            }
            else
            {
                Console.WriteLine("bad:" + text);
                TreeNode node = new TreeNode(text);
                node.BackColor = Color.Red;
                signNode.Nodes.Add(node);
                return true;
            }
        }

        private string getFirstText(string p1, string p2, string p3, string p4)
        {
            if (!String.IsNullOrWhiteSpace(p1))
                return p1;
                //return p1.Substring(0, p1.Length > 9 ? 10 : p1.Length);
            if (!String.IsNullOrWhiteSpace(p2))
                return p2;
            if (!String.IsNullOrWhiteSpace(p3))
                return p3;
            if (!String.IsNullOrWhiteSpace(p4))
                return p4;
            return "";
        }

        OpenFileDialog OFD = new OpenFileDialog();

        private void onClickOpenMap(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = OFD.FileName;
                FileAttributes attr = File.GetAttributes(path);
                //detect whether its a directory or file
                string[] files;
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {

                    files = System.IO.Directory.GetFiles(path, "*.mca", SearchOption.AllDirectories);
                }
                else
                {
                    files = System.IO.Directory.GetFiles(Path.GetDirectoryName(path), "*.mca", SearchOption.AllDirectories);
                }
                for (int i = 0; i < files.Length; i++)
                {
                    RegionIO.LoadRegion(files[i], this);
                }
            }
        }

        private void treeviewOnMouseClick(object sender, MouseEventArgs e)
        {

        }

        private void treeviewOnMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            textBox1.Text = e.Node.Text;
        }

    }
}
