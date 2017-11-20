using System;
using System.Collections.Generic;

namespace Engine
{
    public class entity
    {
        public char texture;
        public int renderPriority = 0;

        public int componentCount;
        public List<iComponent> components;

        public Vector2 ScreenPos;
        public Vector2 WorldPos;

        public entity(char tex, Vector2 worldPos)
        {
            texture = tex;
            WorldPos = worldPos;
            ScreenPos = Renderer.worldPosToScreenPos(worldPos);
            components = new List<iComponent>();
            componentCount = 0;
        }

        public void doUpdate()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].update(this);
            }

            doRender();
        }

        public void doRender()
        {
            Renderer.renderer.render(ScreenPos, texture);
        }

        public void addComponent(iComponent c)
        {
            components.Add(c);
            componentCount++;
            c.start(this);
        }

        public void setPos(Vector2 updatedPos)
        {
            Renderer.renderer.render(ScreenPos, Renderer.renderer.clear);
            WorldPos = updatedPos;
            updateScreenPos();
        }

        private void updateScreenPos()
        {
            ScreenPos = Renderer.worldPosToScreenPos(WorldPos);
        }
    }
}
