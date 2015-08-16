using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCGT_SignTranslator.GTaylor.Serialization
{
    public class ResourcePack
    {
        public static readonly string PATH_DEFAULTFOLDER = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/Global Sign Editor";
        public static readonly string PATH_DEFAULTFILE = PATH_DEFAULTFOLDER + "/resouces.zip";

        private const string PATH_LANG = "assets/minecraft/lang/";
        public const string TAG_UNTRANSLATED = "[?Untranslated?]";
        private const string LINE_SEPERATOR = @"\r\n";
        public string ZipPath = "";
        public Dictionary<string, Dictionary<string, string>> Languages = new Dictionary<string, Dictionary<string, string>>();

       /* public void Print()
        {
            foreach (KeyValuePair<string, Dictionary<string,string>> langPair in Languages)
            {
                Console.WriteLine("LANGUAGE = {0}", langPair.Key);
                foreach (KeyValuePair<string, string> kvp in langPair.Value)
                {
                    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }
        }*/
        public ResourcePack(string root, string filenameWithExtension)
        {
            Init(root +Path.DirectorySeparatorChar+ filenameWithExtension);
        }
        public ResourcePack(string path)
        {
            Init(path);
        }
        private void Init(string path)
        {
            ZipPath = path;
            if (File.Exists(ZipPath))
            {
                using (ZipFile zip1 = ZipFile.Read(ZipPath))
                {
                    foreach (ZipEntry e in zip1)
                    {
                        if ((e.FileName).StartsWith(PATH_LANG) &&e.FileName.EndsWith(".lang"))
                        {
                            MemoryStream fs = new MemoryStream();
                            e.Extract(fs);
                            StreamReader reader = new StreamReader(fs);
                            fs.Position = 0;
                                Dictionary<string, string> currentLanguage = new Dictionary<string, string>();
                                string langname = e.FileName.Substring(PATH_LANG.Length);
                                //langname = langname.Substring(0,langname.LastIndexOf(".lang"));
                                Languages.Add(e.FileName.Substring(PATH_LANG.Length), currentLanguage);
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    
                                    string[] pair = line.Split('=');
                                    if(pair.Length==2)
                                        currentLanguage.Add(pair[0], pair[1]);
                                }
                                //fileContents = reader.ReadToEnd();
                            
                            fs.Close();
                        }
                    }
                }
            }
            else
            {
                Languages.Add(MCLocalize.getFileName(Properties.Settings.Default.Language), new Dictionary<string, string>());

            }
            //Print();
            /*
                     zip.RemoveEntry(entry);
                    zip.AddEntry(entry.FileName, text, ASCIIEncoding.Unicode);
            */
            /*
                 String zipToCreate = "Content.zip";
                String fileNameInArchive = "Content-From-Stream.bin";
                using (System.IO.Stream streamToRead = MyStreamOpener())
                {
                    using (ZipFile zip = new ZipFile())
                    {
                          ZipEntry entry= zip.AddEntry(fileNameInArchive, streamToRead);
                         zip.Save(zipToCreate);  // the stream is read implicitly here
                    }
                }

            */
        }
        public ResourcePack(string[] languages)
        {

        }
        public void Save()
        {
            
            if (File.Exists(ZipPath)) {
                ZipFile zip = ZipFile.Read(ZipPath);
                foreach (KeyValuePair<string, Dictionary<string, string>> langPair in Languages)
                {
                    if (zip.ContainsEntry(PATH_LANG + langPair.Key)) 
                        zip.RemoveEntry(PATH_LANG + langPair.Key);
                    zip.AddEntry(PATH_LANG + langPair.Key, CreateFileString(langPair.Value), Encoding.Unicode);
                }
                zip.Save();
            }
            else
            {
                //FileStream fs = File.Create(ZipPath);
                //fs.Close();
                //pFile zip = new ZipFile()
                using (ZipFile zip = new ZipFile()){ 
                    
                    //zip.AddDirectory("assets");
                    // zip.AddDirectory("assets/minecraft");
                   // zip.AddDirectory("assets/minecraft/lang");
                    zip.AddEntry("pack.mcmeta", "{\"pack\": { \"pack_format\": 1,\"description\": \"My MC Pack\" }}");
                    foreach (KeyValuePair<string, Dictionary<string, string>> langPair in Languages)
                    {
                        //zip.RemoveEntry(PATH_LANG + langPair.Key);
                        zip.AddEntry(PATH_LANG + langPair.Key, CreateFileString(langPair.Value), Encoding.Unicode);
                    }
                    zip.Save(ZipPath);
                }
            }
        }

        private string CreateFileString(Dictionary<string, string> value)
        {
            string text="";
            foreach(KeyValuePair<string, string> pair in value)
            {
                text += pair.Key + "=" + pair.Value + Environment.NewLine;
            }
            return text;
        }

        public void PopulateLanguageComboBox(ComboBox box)
        {
            box.Items.Clear();
            foreach (KeyValuePair<string, Dictionary<string, string>> langPair in Languages)
            {
                box.Items.Add(langPair.Key);
            }
            box.SelectedIndex = 0;
        }

        internal string Localise(string m_typeValue1, string currentLanguage)
        {
            if (Languages.ContainsKey(currentLanguage))
            {
                Dictionary<String, String> Language = Languages[currentLanguage];
                if (Language.ContainsKey(m_typeValue1))
                {
                    return Language[m_typeValue1];
                }
                else
                {
                    Language.Add(m_typeValue1, TAG_UNTRANSLATED);
                    return "";
                }
            }
            else
            {
                Languages.Add(currentLanguage, new Dictionary<string, string>());
                Languages[currentLanguage].Add(m_typeValue1, TAG_UNTRANSLATED);
                return "";
            }
        }
        public void SetKey(string key, string value, string currentLanguage)
        {
            if (Languages.ContainsKey(currentLanguage))
            {
                Dictionary<String, String> Language = Languages[currentLanguage];
                if (Language.ContainsKey(key))
                {
                    Language[key]=value;
                }
                else
                {
                    Language.Add(key, value);
                    
                }
            }
            else
            {
                Languages.Add(currentLanguage, new Dictionary<string, string>());
                Languages[currentLanguage].Add(key, value);
               
            }
        }
    }
}
