using System;
using Engine;

namespace TheForrest.Components
{
    public class cPlayerControl : iComponent
    {
        public Vector3 movement;
        public cEntityMapper entityMapper;

        private int timeBetweenMovements;

        public cPlayerControl(cEntityMapper mapper, int debounceTime)
        {
            movement = new Vector3();
            entityMapper = mapper;
            timeBetweenMovements = debounceTime;
        }

        public void start(entity e)
        {
            
        }

        public void update(entity e)
        {
            if(Game.doTick(timeBetweenMovements))
            {
                movement = new Vector3();

                if (Game.i.IsKeyHeld(Program.moveUp))
                {
                    movement.y--;
                }

                if (Game.i.IsKeyHeld(Program.moveDown))
                {
                    movement.y++;
                }

                if (Game.i.IsKeyHeld(Program.moveleft))
                {
                    movement.x--;
                }

                if (Game.i.IsKeyHeld(Program.moveRight))
                {
                    movement.x++;
                }

                Vector3 fullMove = movement + e.WorldPos;

                if(fullMove.x < World.World.w.WorldSizeMin.x)
                {
                    movement.x++;
                }

                if(fullMove.x > World.World.w.WorldSizeMax.x)
                {
                    movement.x--;
                }

                if (fullMove.y < World.World.w.WorldSizeMin.y)
                {
                    movement.y++;
                }

                if (fullMove.y > World.World.w.WorldSizeMax.y)
                {
                    movement.y--;
                }

                cEntityMapper spaceToCheck = EntityMap.e.CheckLocation(fullMove);

                if (spaceToCheck == null || spaceToCheck.worldPos == e.WorldPos)
                {

                    if (fullMove.x > Renderer.windowBottom.x && fullMove.x < World.World.w.WorldSizeMax.x)
                    {
                        Renderer.windowOffset.x++;
                    }

                    if (fullMove.x < Renderer.windowOffset.x && fullMove.x > World.World.w.WorldSizeMin.x)
                    {
                        Renderer.windowOffset.x--;
                    }

                    if (fullMove.y > Renderer.windowBottom.y && fullMove.y < World.World.w.WorldSizeMax.y)
                    {
                        Renderer.windowOffset.y++;
                    }

                    if (fullMove.y < Renderer.windowOffset.y && fullMove.y > World.World.w.WorldSizeMin.y)
                    {
                        Renderer.windowOffset.y--;
                    }

                    e.setPos(movement);
                }
                else
                {
                    movement = new Vector3();
                }

                if (Game.i.IsKeyRising(OpenTK.Input.Key.E))
                {
                    World.Tile t = World.World.w.getTile(new Vector2(e.WorldPos.x + 1, e.WorldPos.y));
                    (t.getComponent(Type.GetType("cGameOfLife")) as cGameOfLife).alive = true;
                }

                if(Game.i.IsKeyFalling(OpenTK.Input.Key.Space))
                {
                    cGameOfLife.pause = !cGameOfLife.pause;
                }
            }

            Game.r.messages.Enqueue("@position: " + e.WorldPos.ToString() + Game.w.WorldPosToChunkPos(e.WorldPos));
            Game.r.messages.Enqueue("Window Offset: " + Renderer.windowOffset.ToString());
            Game.r.messages.Enqueue("World Offset: " + Game.w.worldOffset);
            Game.r.messages.Enqueue("Window Bottom: " + Renderer.windowBottom.ToString());
            Game.r.messages.Enqueue("Window Res: " + Renderer.windowRes.ToString());
        }
    }
}
