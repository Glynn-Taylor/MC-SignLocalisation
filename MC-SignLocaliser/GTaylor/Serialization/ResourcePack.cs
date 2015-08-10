using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGT_SignTranslator.GTaylor.Serialization
{
    public class ResourcePack
    {
        private const string PATH_LANG = "assets/minecraft/lang/";
        public string ZipPath = "";
        public Dictionary<string, Dictionary<string, string>> Languages = new Dictionary<string, Dictionary<string, string>>();
        private ResourcePack(string root)
        {
            ZipPath = root + Path.DirectorySeparatorChar + "resources.zip";
            if (File.Exists(ZipPath))
            {
                using (ZipFile zip1 = ZipFile.Read(ZipPath))
                {
                    foreach (ZipEntry e in zip1)
                    {
                        if ((e.FileName).StartsWith(PATH_LANG))
                        {
                            MemoryStream fs = new MemoryStream();
                            e.Extract(fs);
                            using (StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8, true))
                            {
                                Dictionary<string, string> currentLanguage = new Dictionary<string, string>();
                                Languages.Add(e.FileName.Substring(PATH_LANG.Length), currentLanguage);
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    string[] pair = line.Split('=');
                                    if(pair.Length==2)
                                        currentLanguage.Add(pair[0], pair[1]);
                                }
                                //fileContents = reader.ReadToEnd();
                            }
                            fs.Close();
                        }
                    }
                }
            }
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
        private ResourcePack(string[] languages)
        {

        }
    }
}
