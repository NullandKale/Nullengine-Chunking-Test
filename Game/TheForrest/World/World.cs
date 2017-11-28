using Engine;
using System.Collections.Generic;
using System;
using TheForrest.Components;

namespace TheForrest.world
{
    public class World
    {
        public static World w;

        public int seed;

        public Vector2 WorldSize;
        public Vector3 ChunkSize;
        public Vector3 WorldSizeMin;
        public Vector3 WorldSizeMax;
        public Vector3 worldOffset;

        public Chunk[,] chunks;

        public bool showWorldPosition = false;

        public World(Vector2 WorldSizeChunks, Vector3 ChunkSizeTiles)
        {
            if (w == null)
            {
                w = this;
            }
            else
            {
                throw new Exception("Singleton Exception @ World.cs");
            }

            worldOffset = Renderer.windowOffset;
            WorldSizeMin = new Vector3(0, 0, 0);
            WorldSizeMax = new Vector3(WorldSizeChunks.x * ChunkSizeTiles.x - 1, WorldSizeChunks.y * ChunkSizeTiles.y - 1, ChunkSizeTiles.z);


            WorldSize = WorldSizeChunks;
            ChunkSize = ChunkSizeTiles;

            chunks = new Chunk[WorldSize.x, WorldSize.y];

            WorldInit();
        }

        public void update()
        {
            worldOffset = Renderer.windowOffset;
            WorldUpdate();
            render();
        }

        public void render()
        {
            for (int x = worldOffset.x; x < Renderer.windowRes.x + worldOffset.x; x++)
            {
                for (int y = worldOffset.y; y < Renderer.windowRes.y + worldOffset.y; y++)
                {
                    Tile t = getTile(new Vector2(x, y));
                    if(t != null)
                    {
                        t.doRender();
                    }
                    else
                    {
                        Renderer.renderer.clearScreenAt(new Vector2(x, y));
                    }
                }
            }
        }

        private void WorldUpdate()
        {
            for (int x = 0; x < WorldSize.x; x++)
            {
                for (int y = 0; y < WorldSize.y; y++)
                {
                    chunks[x, y].update();
                }
            }
        }

        private void WorldInit()
        {
            for (int x = 0; x < WorldSize.x; x++)
            {
                for (int y = 0; y < WorldSize.y; y++)
                {
                    chunks[x, y] = new Chunk('-', new Vector2(x, y), ChunkSize);
                }
            }
        }

        public Tile getTile(Vector2 worldPos)
        {
            if(isValidWorldPos(worldPos))
            {
                Vector2 ChunkPos = WorldPosToChunkPos(worldPos);
                return chunks[ChunkPos.x, ChunkPos.y].getTile(WorldPosToTilePos(worldPos));
            }
            else
            {
                return null;
            }
        }

        public Tile getTile(Vector3 tilePos)
        {
            Vector2 ChunkPos = WorldPosToChunkPos(tilePos);
            return chunks[ChunkPos.x, ChunkPos.y].getTile(WorldPosToTilePos(tilePos));
        }

        public bool isWorldPosOnScreen(Vector3 pos)
        {
            return utils.isInRange(Renderer.windowOffset.x, Renderer.windowBottom.x, pos.x) && utils.isInRange(Renderer.windowOffset.y, Renderer.windowBottom.y, pos.y) && pos.z == worldOffset.z;
        }

        public bool isWorldPosOnScreen(int x, int y)
        {
            return utils.isInRange(Renderer.windowOffset.x, Renderer.windowBottom.x, x) && utils.isInRange(Renderer.windowOffset.y, Renderer.windowBottom.y, y);
        }

        public Vector2 WorldPosToChunkPos(Vector2 WorldPos)
        {
            return new Vector2(WorldPos.x / ChunkSize.x, WorldPos.y / ChunkSize.y);
        }

        public Vector3 WorldPosToTilePos(Vector2 WorldPos)
        {
            return new Vector3(WorldPos.x % ChunkSize.x, WorldPos.y % ChunkSize.y, worldOffset.z);
        }

        public Vector3 ChunkTilePosToWorld(Vector2 ChunkPos, Vector3 TilePos)
        {
            return new Vector3((ChunkPos.x * ChunkSize.x) + TilePos.x, (ChunkPos.y * ChunkSize.y) + TilePos.y, TilePos.z);
        }

        public bool isValidWorldPos(Vector3 worldPos)
        {
            return (utils.isInRange(WorldSizeMin.x, WorldSizeMax.x, worldPos.x)) && (utils.isInRange(WorldSizeMin.y, WorldSizeMax.y, worldPos.y)) && (utils.isInRange(WorldSizeMin.z, WorldSizeMax.z, worldPos.z));
        }

        public bool isValidWorldPos(Vector2 worldPos)
        {
            return (utils.isInRange(WorldSizeMin.x, WorldSizeMax.x, worldPos.x)) && (utils.isInRange(WorldSizeMin.y, WorldSizeMax.y, worldPos.y));
        }

        public Vector2 WorldToScreenPos(Vector3 WorldPos)
        {
            return new Vector2(WorldPos.x - worldOffset.x, WorldPos.y - worldOffset.y);
        }
    }
}

