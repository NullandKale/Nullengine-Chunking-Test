using System;
using System.Collections.Generic;

namespace Engine
{
    public class Game
    {
        public static Game g;
        public static Renderer r;
        public static Input i;

        public bool run;

        public List<Action> toUpdate;

        public TheForrest.Entities.playerCharacter pc;

        public Game(Vector2 resolution, char clear)
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

            r = new Renderer(resolution, clear);
            i = new Input();

            run = true;

            pc = new TheForrest.Entities.playerCharacter('@', new Vector2(10, 10));
            toUpdate.Add(pc.doUpdate);
        }

        public void update()
        {
            i.doUpdate();

            for (int i = 0; i < toUpdate.Count; i++)
            {
                toUpdate[i].Invoke();
            }

            r.doUpdate();
        }

        public void StartGame()
        {
            while(run)
            {
                update();
            }
        }
    }
}
