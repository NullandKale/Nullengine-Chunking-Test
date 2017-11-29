using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheForrest.world
{
    public class ChunkStorage
    {
        public Chunk[,] chunkStore;

        private Vector2 worldSize;
        private Vector3 chunkSize;

        private const string filePrefix = "//Worlds//";

        public ChunkStorage(Vector2 WorldSize, Vector3 ChunkSize, string fileName)
        {
            worldSize = WorldSize;
            chunkSize = ChunkSize;

            ChunksInit();
        }

        private void ChunksInit()
        {
            chunkStore = new Chunk[worldSize.x, worldSize.y];

            for (int x = 0; x < worldSize.x; x++)
            {
                for (int y = 0; y < worldSize.y; y++)
                {
                    chunkStore[x, y] = new Chunk('-', new Vector2(x, y), chunkSize);
                }
            }
        }

        public void SaveChunks()
        {

        }

    }
}