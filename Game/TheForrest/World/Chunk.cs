using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheForrest.World
{
    public class Chunk
    {
        public Vector2 Pos;
        public Vector3 TileHWL;

        public Tile[,,] tiles;

        public Chunk(char filler, Vector2 pos, Vector3 tileSize)
        {
            Pos = pos;
            TileHWL = tileSize;

            initTiles(filler);

        }

        public Tile getTile(Vector3 pos)
        {
            if (isValidPos(pos))
            {
                return tiles[pos.x, pos.y, pos.z];
            }
            else
            {
                return null;
            }
        }

        public void update()
        {
            for (int x = 0; x < TileHWL.x; x++)
            {
                for (int y = 0; y < TileHWL.y; y++)
                {
                    if (World.w.isOnScreen(new Vector3(x, y, World.w.worldOffset.z)))
                    {
                        tiles[x, y, World.w.worldOffset.z].doUpdate();
                    }
                }
            }
        }

        public void render()
        {
            for (int x = 0; x < TileHWL.x; x++)
            {
                for (int y = 0; y < TileHWL.y; y++)
                {
                    if (World.w.isOnScreen(new Vector3(x, y, World.w.worldOffset.z)))
                    {
                        tiles[x, y, World.w.worldOffset.z].doRender();
                    }
                }
            }
        }

        public bool isChunkOnScreen()
        {
            return (World.w.isOnScreen(World.w.ChunkTilePosToWorld(Pos, new Vector3(0, 0, 0))) || World.w.isOnScreen(World.w.ChunkTilePosToWorld(Pos, new Vector3(TileHWL.x, 0, 0))) ||
                   World.w.isOnScreen(World.w.ChunkTilePosToWorld(Pos, new Vector3(0, TileHWL.y, 0))) || World.w.isOnScreen(World.w.ChunkTilePosToWorld(Pos, new Vector3(TileHWL.x, TileHWL.y, 0))));
        }

        public bool isValidPos(Vector3 pos)
        {
            return (utils.isInRange(0, TileHWL.x - 1, pos.x) || utils.isInRange(0, TileHWL.y - 1, pos.y) || (utils.isInRange(0, TileHWL.z - 1, pos.z)));
        }

        private void initTiles(char filler)
        {
            tiles = new Tile[TileHWL.x, TileHWL.y, TileHWL.z];
            for (int x = 0; x < TileHWL.x; x++)
            {
                for (int y = 0; y < TileHWL.y; y++)
                {
                    for (int z = 0; z < TileHWL.z; z++)
                    {
                        tiles[x, y, z] = new Tile(filler, new Vector3(x, y, z), this);
                    }
                }
            }
        }
    }
}
