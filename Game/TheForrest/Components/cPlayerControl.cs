using System;
using Engine;

namespace TheForrest.Components
{
    public class cPlayerControl : iComponent
    {
        public Vector2 movement;

        public cPlayerControl()
        {
            movement = new Vector2();
        }

        public void start(entity e)
        {
            
        }

        public void update(entity e)
        {
            movement = e.WorldPos;

            if(Game.i.IsKeyFalling(Program.moveUp))
            {
                movement.y++;
            }

            if (Game.i.IsKeyFalling(Program.moveDown))
            {
                movement.y--;
            }

            if (Game.i.IsKeyFalling(Program.moveleft))
            {
                movement.x--;
            }

            if (Game.i.IsKeyFalling(Program.moveRight))
            {
                movement.x++;
            }

            e.setPos(movement);
            Game.r.messages.Enqueue(e.ScreenPos.ToString());
        }
    }
}
