using System;
using System.Collections.Generic;
using TheForrest.Entities;
using TheForrest.World;
namespace Engine
{
    public class Game
    {
        public static Game g;
        public static Renderer r;
        public static Input i;
        public static EntityMap e;
        public static World w;

        public static int frameCount;

        public bool run;

        public Vector2 windowReserve;

        public List<Action> toUpdate;

        public playerCharacter pc;
        public Enemy enemy; 

        public Game(Vector2 resolution, char clear, int boundSize)
        {
            if (g == null)
            {
                g = this;
            }
            else
            {
                throw new Exception("Singleton Exception @ Game.cs");
            }

            toUpdate = new List<Action>();

            windowReserve = new Vector2(20, 4);

            r = new Renderer(resolution, clear, windowReserve);
            i = new Input();
            e = new EntityMap(boundSize);
            w = new World(new Vector2(10,10), new Vector3(20, 20, 5));

            frameCount = 0;

            run = true;

            enemy = new Enemy('E', new Vector3(15, 15, 0));
            toUpdate.Add(enemy.doUpdate);

            pc = new playerCharacter('@', new Vector3(10, 10, 0));
            toUpdate.Add(pc.doUpdate);
        }

        public void update()
        {
            w.update();
            i.doUpdate();

            for (int i = 0; i < toUpdate.Count; i++)
            {
                toUpdate[i].Invoke();
            }

            r.doUpdate();

            frameCount++;

            System.Threading.Thread.Sleep(1);
        }

        public void StartGame()
        {
            while(run)
            {
                update();
            }
        }

        public static bool doTick(int tickCount)
        {
            return frameCount % tickCount == 0;
        }
    }
}
