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
using MCGT_SignTranslator.GTaylor.Data;

namespace MCGT_SignTranslator
{
    public partial class MainForm : Form
    {
        private const int CHUNK_SIZE = 32;
        private const int PAGE_LENGTH = 4096;
        private readonly Color HIGHLIGHT_GREY = Color.FromArgb(150,50, 50, 50);
        private string[] Languages = { "English", "Deutsch", "Francais", "Espanol" };
        private Image[] LanguageIcons = { Properties.Resources.en1, Properties.Resources.de, Properties.Resources.fr, Properties.Resources.es };
        private Dictionary<Point, NbtFile> LoadedChunks = new Dictionary<Point, NbtFile>();
        private static string LevelRootPath = "";
        public SignNode CurrentNode=null;
        public Label Sign_Text1 { get { return SignText1; } }
        public Label Sign_Text2 { get { return SignText2; } }
        public Label Sign_Text3 { get { return SignText3; } }
        public Label Sign_Text4 { get { return SignText4; } }
        public ComboBox Sign_ColorChooser { get { return ColorChooser; } }
        public ComboBox Sign_TypeChooser { get { return TypeChooser; } }
        public TextBox Sign_TypeValue1{ get { return TypeValue1; } }
        public TextBox Sign_TypeValue2 { get { return TypeValue2; } }
        public TextBox Sign_RawText { get { return RawTextBox; } }
        public RadioButton Sign_EventButton { get { return EventButton; } }
        public ComboBox Sign_Action { get { return ActionChooser; } }
        public TextBox Sign_ActionValue { get { return ActionValueBox; } }
        public bool UnsavedChanges = false;
        public ResourcePack Resource;

        public MainForm()
        {
            InitializeComponent();
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void MarkUnsaved()
        {
            UnsavedChanges = true;
        }
        private void OnLoad(object sender, EventArgs e)
        {
            /*for (int i = 0; i < Languages.Length; i++)
            {
                LanguageSelectionItem btn = new LanguageSelectionItem();
                btn.Initialise(LanguageIcons[i], Languages[i]);
                this.listControl1.Add(btn);

            }*/
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
                        SignNode signNode = new SignNode(entTag, "[" + x.IntValue + "," + y.IntValue + "," + z.IntValue + "]" + getFirstText(t1, t2, t3, t4));

                        if (GenerateSignTextNode(signNode, t1) | GenerateSignTextNode(signNode, t2) | GenerateSignTextNode(signNode, t3) | GenerateSignTextNode(signNode, t4))
                            signNode.BackColor = Color.Red;
                        Console.WriteLine("adding signNode");
                        EntityTree.Nodes.Add(signNode);
                    }
                    KeepLoaded = true;
                }
            }
            if (KeepLoaded) {
                Point p = new Point(levelTag.Get<NbtInt>("xPos").IntValue, levelTag.Get<NbtInt>("zPos").IntValue);
                if (!LoadedChunks.ContainsKey(p))
                    LoadedChunks.Add(p, myFile);
                Console.WriteLine("Adding at: " + levelTag.Get<NbtInt>("xPos").StringValue + ":" + levelTag.Get<NbtInt>("zPos").StringValue);
            }
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
            //Text Files (*.txt)|*.txt|All Files (*.*)|*.*
            OFD.Filter = "Level Files|Level.dat";
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = OFD.FileName;
                FileAttributes attr = File.GetAttributes(path);
                //detect whether its a directory or file
                string[] files;
                string regionPath = Path.GetDirectoryName(path)+Path.DirectorySeparatorChar+"region";
                //Check for regionpath
                if (!Directory.Exists(regionPath))
                {
                    MessageBox.Show("No region folder found", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    return;
                }
                LevelRootPath = Path.GetDirectoryName(path);
                files = System.IO.Directory.GetFiles(regionPath, "*.mca", SearchOption.AllDirectories);
                /* if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                 {

                     files = System.IO.Directory.GetFiles(path, "*.mca", SearchOption.AllDirectories);
                 }
                 else
                 {
                     files = System.IO.Directory.GetFiles(Path.GetDirectoryName(path), "*.mca", SearchOption.AllDirectories);
                 }*/

                for (int i = 0; i < files.Length; i++)
                {
                    RegionIO.LoadRegion(files[i], this);
                }
                Resource = new ResourcePack(Path.GetDirectoryName(path));
            }
        }

     

        private void treeviewOnMouseClick(object sender, MouseEventArgs e)
        {

        }

        private void treeviewOnMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            RawTextBox.Text = e.Node.Text;
            ((SignNode)e.Node).OnClick(this);
            //SignText1
        }

        private void RenameTest(object sender, EventArgs e)
        {
            foreach (SignNode node in EntityTree.Nodes)
            {
                node.SignData.Get<NbtString>("Text1").Value = "derp";
            }
            if (!String.IsNullOrWhiteSpace(LevelRootPath))
            {
                foreach (KeyValuePair<Point, NbtFile> entry in LoadedChunks)
                {
                    RegionIO.SaveChunk(entry.Value, entry.Key.X, entry.Key.Y, LevelRootPath);
                }

            }
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ColorChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CurrentNode!= null&& CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.TextColor = SignNode.ColorDictionary[((ComboBox)sender).Text];
                CurrentNode.LineChanged(this);
            }
        }

        private void SignText1_Click(object sender, EventArgs e)
        {
            ResetTextColor();
            SignText1.BackColor = HIGHLIGHT_GREY;
            if (CurrentNode != null)
                CurrentNode.OnClick(1, this);
        }

        private void SignText2_Click(object sender, EventArgs e)
        {
            ResetTextColor();
            SignText2.BackColor = HIGHLIGHT_GREY;
            if (CurrentNode != null)
                CurrentNode.OnClick(2, this);
        }

        private void SignText3_Click(object sender, EventArgs e)
        {
            ResetTextColor();
            SignText3.BackColor = HIGHLIGHT_GREY;
            if (CurrentNode != null)
                CurrentNode.OnClick(3, this);
        }

        private void SignText4_Click(object sender, EventArgs e)
        {
            ResetTextColor();
            SignText4.BackColor = HIGHLIGHT_GREY;
            if (CurrentNode != null)
                CurrentNode.OnClick(4, this);
        }
        private void ResetTextColor()
        {
            //Console.WriteLine(CurrentNode == null);
            SignText1.BackColor = Color.Transparent;
            SignText2.BackColor = Color.Transparent;
            SignText3.BackColor = Color.Transparent;
            SignText4.BackColor = Color.Transparent;
        }

        private void TypeValue1_TextChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.TypeValue1 = ((TextBox)sender).Text;
                CurrentNode.LineChanged(this);
            }
        }

        private void TypeValue2_TextChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.TypeValue2 = ((TextBox)sender).Text;
                CurrentNode.LineChanged(this);
            }
        }

        private void EventButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.HasEvent = ((RadioButton)sender).Checked;
                CurrentNode.LineChanged(this);
            }
        }

        private void ActionChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.Action= ((ComboBox)sender).Text;
                CurrentNode.LineChanged(this);
            }
        }

        private void ActionValueBox_TextChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.ActionValue = ((TextBox)sender).Text;
                CurrentNode.LineChanged(this);
            }
        }

        private void BoldButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.Bold = ((CheckBox)sender).Checked;
                CurrentNode.LineChanged(this);
            }
        }

        private void ItalicButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.Italic = ((CheckBox)sender).Checked;
                CurrentNode.LineChanged(this);
            }
        }

        private void UnderlineButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.Underlined = ((CheckBox)sender).Checked;
                CurrentNode.LineChanged(this);
            }
        }

        private void StrikethroughButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.Strikethrough = ((CheckBox)sender).Checked;
                CurrentNode.LineChanged(this);
            }
        }

        private void ObfuscateButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentNode != null && CurrentNode.CurrentLine != null)
            {
                CurrentNode.CurrentLine.Obfuscated = ((CheckBox)sender).Checked;
                CurrentNode.LineChanged(this);
            }
        }
        public void SetFormatting(bool B, bool I, bool U, bool S, bool Ob)
        {
            BoldButton.Checked = B;
            ItalicButton.Checked = I;
            UnderlineButton.Checked = U;
            StrikethroughButton.Checked = S;
            ObfuscateButton.Checked = Ob;
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(LevelRootPath))
            {
                foreach (KeyValuePair<Point, NbtFile> entry in LoadedChunks)
                {
                    RegionIO.SaveChunk(entry.Value, entry.Key.X, entry.Key.Y, LevelRootPath);
                }

            }
            UnsavedChanges = false;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if(UnsavedChanges)
            if (MessageBox.Show("There is unsaved changes, are you sure you wish to exit?", "Application Exit", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
