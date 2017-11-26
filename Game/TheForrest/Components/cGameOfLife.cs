using Engine;
using TheForrest.World;

namespace TheForrest.Components
{
    public class cGameOfLife : iComponent
    {
        public bool alive;

        public static bool pause = false;

        public void start(entity e)
        {
            alive = false;
        }

        public void update(entity e)
        {
            if(!pause)
            {
                int neighborStatus = GetArea(e);

                if (neighborStatus > 2)
                {
                    alive = false;
                }
                else if (neighborStatus < 3)
                {
                    alive = false;
                }

                if (neighborStatus == 3)
                {
                    alive = true;
                }
            }

            if(alive)
            {
                e.texture = '#';
            }
            else
            {
                e.texture = ' ';
            }
        }

        public int GetArea(entity e)
        {
            int temp = 0;

            for(int x = -1; x > 2; x++)
            {
                for(int y = -1; y > 2; y++)
                {
                    if(World.World.w.getTile(new Vector3(e.WorldPos.x + x, e.WorldPos.y + y, e.WorldPos.z)).texture == '#')
                    {
                        temp++;
                    }
                }
            }

            return temp;
        }
    }
}
