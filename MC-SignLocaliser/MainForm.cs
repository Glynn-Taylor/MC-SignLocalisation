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

namespace MCGT_SignTranslator
{
    public partial class MainForm : Form
    {
        private const int CHUNK_SIZE = 32;
        private const int PAGE_LENGTH = 4096;
        private string[] Languages = { "English", "Deutsch", "Francais", "Espanol" };
        private Image[] LanguageIcons = { Properties.Resources.en1, Properties.Resources.de, Properties.Resources.fr, Properties.Resources.es };
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
        private void LoadSigns(string path)
        {
            Stream regionFile = File.Open(path, FileMode.OpenOrCreate);

            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int z = 0; z < CHUNK_SIZE; z++)
                {
                    var chunkData = GetChunkFromTable(x, z, regionFile);
                    if (chunkData != null)
                    {
                        regionFile.Seek(chunkData.Item1, SeekOrigin.Begin);
                        int length = (int)ReadUInt32(regionFile);
                        int compressionMode = regionFile.ReadByte();
                        //Console.WriteLine("Compression mode: " + compressionMode.ToString());
                        switch (compressionMode)
                        {
                            case 1: // gzip
                                break;
                            case 2: // zlib
                                var nbt = new NbtFile();
                                nbt.LoadFromStream(regionFile, NbtCompression.ZLib, null);
                                FindSigns(nbt);
                                break;
                            default:
                                throw new InvalidDataException("Invalid compression scheme provided by region file.");
                        }
                    }
                }
            }
            Console.WriteLine("Finished reading region file: " + path);

        }
       
        private void FindSigns(NbtFile myFile)
        {
            //Console.WriteLine(myFile.FileName + ":" + myFile.FileCompression.ToString());
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
                }
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
                    LoadSigns(files[i]);
                }
            }
        }

        private Tuple<int, int> GetChunkFromTable(int x, int z, Stream regionFile) // <offset, length>
        {
            //Get start of the chunk in the location table
            int tableOffset = ((x % CHUNK_SIZE) + (z % CHUNK_SIZE) * CHUNK_SIZE) * 4;
            //goto chunklocation start
            regionFile.Seek(tableOffset, SeekOrigin.Begin);
            //Read the offset
            byte[] offsetBuffer = new byte[4];
            regionFile.Read(offsetBuffer, 0, 3);
            Array.Reverse(offsetBuffer);
            //Get the length in pages
            int length = regionFile.ReadByte();
            //offset int conversion from byte and back to amount of bytes, but in int
            int offset = BitConverter.ToInt32(offsetBuffer, 0) << 4;
            if (offset == 0 || length == 0)
                return null;
            return new Tuple<int, int>(offset, length * PAGE_LENGTH);
        }
        public uint ReadUInt32(Stream stream)
        {
            return (uint)(
                (ReadUInt8(stream) << 24) |
                (ReadUInt8(stream) << 16) |
                (ReadUInt8(stream) << 8) |
                 ReadUInt8(stream));
        }
        public byte ReadUInt8(Stream stream)
        {
            int value = stream.ReadByte();
            if (value == -1)
                throw new EndOfStreamException();
            return (byte)value;
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
