using System;
using Engine;
using TheForrest.world;

namespace TheForrest.Components
{
    public class cPlayerControl : iComponent
    {
        public static bool showInfo = false;

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
            if(Game.i.IsKeyRising(OpenTK.Input.Key.Escape))
            {
                Game.g.run = false;
            }

            if(Game.i.IsKeyFalling(Program.debugInfo))
            {
                showInfo = !showInfo;
            }

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

                Vector3 fullMove = correctMove(movement + e.WorldPos);

                cEntityMapper spaceToCheck = EntityMap.e.CheckLocation(fullMove);

                if (spaceToCheck == null || spaceToCheck.worldPos == e.WorldPos)
                {

                    if (fullMove.x > Renderer.windowBottom.x - 1)
                    {
                        Renderer.windowOffset.x++;
                    }

                    if (fullMove.x < Renderer.windowOffset.x)
                    {
                        Renderer.windowOffset.x--;
                    }

                    if (fullMove.y > Renderer.windowBottom.y - 1)
                    {
                        Renderer.windowOffset.y++;
                    }

                    if (fullMove.y < Renderer.windowOffset.y)
                    {
                        Renderer.windowOffset.y--;
                    }

                    e.setPos(fullMove);
                }
            }

            if(showInfo)
            {
                Game.r.messages.Enqueue("The Forest V0.0.1");
                Game.r.messages.Enqueue("@position: " + e.WorldPos.ToString() + Game.w.WorldPosToChunkPos(e.WorldPos));
                Game.r.messages.Enqueue("Update Time: " + Game.lastFrameTime);
                Game.r.messages.Enqueue("Window Offset: " + Renderer.windowOffset.ToString());
                Game.r.messages.Enqueue("World Offset: " + Game.w.worldOffset);
                Game.r.messages.Enqueue("Window Bottom: " + Renderer.windowBottom.ToString());
                Game.r.messages.Enqueue("Window Res: " + Renderer.windowRes.ToString());
            }
        }

        private Vector3 correctMove(Vector3 move)
        {
            Vector3 cMove = move;

            if(cMove.x < World.w.WorldSizeMin.x)
            {
                cMove.x = World.w.WorldSizeMin.x;
            }
            else if(cMove.x > World.w.WorldSizeMax.x)
            {
                cMove.x = World.w.WorldSizeMax.x;
            }

            if (cMove.y < World.w.WorldSizeMin.y)
            {
                cMove.y = World.w.WorldSizeMin.y;
            }
            else if (cMove.y > World.w.WorldSizeMax.y)
            {
                cMove.y = World.w.WorldSizeMax.y;
            }

            return cMove;
        }
    }
}
