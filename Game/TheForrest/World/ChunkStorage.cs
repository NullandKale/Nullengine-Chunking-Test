using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TheForrest.world
{
    public class ChunkStorage
    {
        public Chunk[,] chunkStore;

        private Vector2 worldSize;
        private Vector3 chunkSize;

        private const string filePrefix = "\\Worlds\\";
        private const string filePostfix = ".cnk";

        private string workingDir;
        private string fileName;

        private IFormatter formatter;

        public ChunkStorage(Vector2 WorldSize, Vector3 ChunkSize, string worldName)
        {
            worldSize = WorldSize;
            chunkSize = ChunkSize;

            workingDir = AppDomain.CurrentDomain.BaseDirectory;
            fileName = new string((workingDir + filePrefix + worldName + filePostfix).ToCharArray());
            formatter = new BinaryFormatter();

            ChunksInit();
        }

        private void ChunksInit()
        {
            bool invalidFile = false;
            if(File.Exists(fileName))
            {
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                try
                {
                    chunkStore = (Chunk[,])formatter.Deserialize(stream);
                }
                catch
                {
                    invalidFile = true;
                }

                stream.Close();
            }
            else
            {
                invalidFile = true;
            }

            if(invalidFile)
            {
                chunkStore = new Chunk[worldSize.x, worldSize.y];

                for (int x = 0; x < worldSize.x; x++)
                {
                    for (int y = 0; y < worldSize.y; y++)
                    {
                        chunkStore[x, y] = new Chunk('-', new Vector2(x, y), chunkSize);
                    }
                }

                SaveChunks();
            }
        }

        public void SaveChunks()
        {
            Directory.CreateDirectory(workingDir + filePrefix);
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, chunkStore);
            stream.Close();
        }

    }
}