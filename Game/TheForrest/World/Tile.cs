using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheForrest.World
{
    public class Tile : Engine.entity
    {
        public Vector3 pos;
        public Vector2 pos2D;
        public Chunk parent;

        public bool firstStart;

        public Tile(char r, Vector3 position, Chunk parent) : base(r, position)
        {
            this.parent = parent;
            pos = position;
            pos2D = new Vector2(pos.x, pos.y);

            if (parent != null)
            {
                WorldPos = World.w.ChunkTilePosToWorld(parent.Pos, pos);
                updateScreenPos();
            }
        }

        public override void doUpdate()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].update(this);
            }
        }
    }
}
