using Engine;
using System.Collections.Generic;
using System;
using TheForrest.Components;

namespace TheForrest.World
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
            for (int x = worldOffset.x; x < Renderer.windowRes.x - worldOffset.x; x++)
            {
                for (int y = worldOffset.y; y < Renderer.windowRes.y - worldOffset.y; y++)
                {
                    Tile t = getTile(new Vector2(x, y));
                    if(t != null)
                    {
                        t.doRender();
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

        public Tile getTile(Vector2 tilePos)
        {
            if(isValidWorldPos(tilePos))
            {
                Vector2 ChunkPos = WorldPosToChunkPos(tilePos);
                return chunks[ChunkPos.x, ChunkPos.y].getTile(WorldPosToTilePos(tilePos));
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

        public bool isOnScreen(Vector3 pos)
        {
            return utils.isInRange(Renderer.windowOffset.x, Renderer.windowBottom.x, pos.x) && utils.isInRange(Renderer.windowOffset.y, Renderer.windowBottom.y, pos.y) && pos.z == worldOffset.z;
        }

        public Vector2 WorldPosToChunkPos(Vector2 WorldPos)
        {
            return new Vector2(WorldPos.x / ChunkSize.x, WorldPos.y / ChunkSize.y);
        }

        public Vector2 WorldPosToChunkPos(Vector3 WorldPos)
        {
            return new Vector2(WorldPos.x / ChunkSize.x, WorldPos.y / ChunkSize.y);
        }

        public Vector3 WorldPosToTilePos(Vector2 WorldPos)
        {
            return new Vector3(WorldPos.x % ChunkSize.x, WorldPos.y % ChunkSize.y, worldOffset.z);
        }

        public Vector3 WorldPosToTilePos(Vector3 WorldPos)
        {
            return new Vector3(WorldPos.x % ChunkSize.x, WorldPos.y % ChunkSize.y, WorldPos.z);
        }


        public Vector3 ChunkTilePosToWorld(Vector2 ChunkPos, Vector3 TilePos)
        {
            return new Vector3(ChunkPos.x * ChunkSize.x + TilePos.x, ChunkPos.y * ChunkSize.y + TilePos.y, TilePos.z);
        }

        public bool isValidWorldPos(Vector3 worldPos)
        {
            return (utils.isInRange(WorldSizeMin.x, WorldSizeMax.x, worldPos.x)) && (utils.isInRange(WorldSizeMin.y, WorldSizeMax.y, worldPos.y)) && (utils.isInRange(WorldSizeMin.z, WorldSizeMax.z, worldPos.z));
        }

        public bool isValidWorldPos(Vector2 worldPos2D)
        {
            Vector3 worldPos3D = new Vector3(worldPos2D.x, worldPos2D.y, worldOffset.z);
            return isValidWorldPos(worldPos3D);
        }

        public Vector2 WorldToScreenPos(Vector3 WorldPos)
        {
            return new Vector2(WorldPos.x - worldOffset.x, WorldPos.y - worldOffset.y);
        }
    }

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
                        tiles[x, y, World.w.worldOffset.z].update();
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
                        tiles[x, y, World.w.worldOffset.z].doUpdate();
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

    public class Tile : Engine.entity
    {
        public Vector3 pos;
        public Vector2 pos2D;
        public Chunk parent;

        Vector3 worldPos;

        public Tile(char r, Vector3 position, Chunk parent) : base(r, position)
        {
            this.parent = parent;
            pos = position;
            pos2D = new Vector2(pos.x, pos.y);

            if (parent != null)
            {
                worldPos = World.w.ChunkTilePosToWorld(parent.Pos, pos);
            }
        }

        public void update()
        {
            if (worldPos == null)
            {
                worldPos = World.w.ChunkTilePosToWorld(parent.Pos, pos);
            }
        }
    }
}

