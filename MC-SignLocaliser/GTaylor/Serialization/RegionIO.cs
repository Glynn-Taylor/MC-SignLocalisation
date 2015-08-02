using fNbt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGT_SignTranslator.GTaylor.Serialization
{
    class RegionIO
    {
        private const int CHUNK_SIZE = 32;
        private const int PAGE_LENGTH = 4096;
        private const byte COMPRESSION_MODE_ZLIB = 0x02;

        //Example of translated sign at r.0.2 [31,0] in test
        public static void LoadRegion(string path, MainForm form)
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
                                Console.WriteLine("chunk " + x + ":" + z);
                                form.LoadChunk(nbt);

                                //nbt.sa
                                break;
                            default:
                                throw new InvalidDataException("Invalid compression scheme provided by region file.");
                        }
                    }
                }
            }
            Console.WriteLine("Finished reading region file: " + path);
            regionFile.Close();
        }
      

        /// <summary>
        /// Saves an nbt file representing a chunk
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="cx"></param>
        /// <param name="cz"></param>
        /// <param name="savegameRootFolder"></param>
        public static void SaveChunk(NbtFile chunk, int cx, int cz, string savegameRootFolder)
        {
            int rx = cx >> 5; //ChunkX/32 as 32 chunks per region
            int rz = cz >> 5;
            //************************\\
            //        File IO         \\
            //************************\\
            //Get region file path
            string filePath = Path.Combine(savegameRootFolder, string.Format(@"region\r.{0}.{1}.mca", rx, rz));
            //Check if non existent
            if (!File.Exists(filePath))
            {
                // Check for region directory
                Directory.CreateDirectory(Path.Combine(savegameRootFolder, "region"));
                // Create empty region file with empty table
                using (var regionFile = File.Open(filePath, FileMode.CreateNew))
                {
                    byte[] buffer = new byte[8192];
                    regionFile.Write(buffer, 0, buffer.Length);
                }
                return;
            }
            //************************\\
            //      Region File       \\
            //************************\\
            using (var regionFile = File.Open(filePath, FileMode.Open))
            {
                //Get offset and length in pages
                var chunkData = GetChunkFromTable(cx, cz, regionFile);
                //Goto offset location
                regionFile.Seek(chunkData.Item1, SeekOrigin.Begin);
                int offset = chunkData.Item1;
                // Get nbt data as byte array
                byte[] nbtBuffer = chunk.SaveToBuffer(NbtCompression.ZLib);
                int bufferLength = nbtBuffer.Length;
                //Check if total length (data + header) exceeds total size of pages
                if (bufferLength + 5 > chunkData.Item2)
                {
                    Console.WriteLine("Chunk exceeded allocated length");
                }
                //Get the length of the chunk + header excluding length part of header
                byte[] chunkHeaderLength = BitConverter.GetBytes(bufferLength + 1);
                Array.Reverse(chunkHeaderLength);


                //************************\\
                //      Write Chunk       \\
                //************************\\
                //Goto beginning
                regionFile.Seek(offset, SeekOrigin.Begin);
                //Write Header
                regionFile.Write(chunkHeaderLength, 0, 4); //Header Length
                regionFile.WriteByte(COMPRESSION_MODE_ZLIB);//Header compression
                //Write data
                regionFile.Write(nbtBuffer, 0, nbtBuffer.Length);

                //Fill rest of page with empty
                int remainder;
                Math.DivRem(bufferLength + 4, PAGE_LENGTH, out remainder);

                byte[] padding = new byte[PAGE_LENGTH - remainder];
                if (padding.Length > 0)
                    regionFile.Write(padding, 0, padding.Length);
                regionFile.Close();
            }

        }
        private static Tuple<int, int> GetChunkFromTable(int x, int z, Stream regionFile) // <offset, length>
        {
            //Get start of the chunk in the location table
            int tableOffset = ((x & (CHUNK_SIZE-1)) + (z & (CHUNK_SIZE-1)) * CHUNK_SIZE) * 4;
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
            // Console.WriteLine("chunk " + x + ":" + z + " offset:" + offset + " length:" + length + " div: "+(offset%4096==0?"yes":"no"));
            return new Tuple<int, int>(offset, length * PAGE_LENGTH);
        }
        public static uint ReadUInt32(Stream stream)
        {
            return (uint)(
                (ReadUInt8(stream) << 24) |
                (ReadUInt8(stream) << 16) |
                (ReadUInt8(stream) << 8) |
                 ReadUInt8(stream));
        }
        public static byte ReadUInt8(Stream stream)
        {
            int value = stream.ReadByte();
            if (value == -1)
                throw new EndOfStreamException();
            return (byte)value;
        }
    }
}
