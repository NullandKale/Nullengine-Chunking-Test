﻿using System;
using System.Collections.Generic;

namespace Engine
{
    [Serializable]
    public class entity
    {
        public char texture;
        public int renderPriority = 0;

        public int componentCount;
        public List<iComponent> components;

        public Vector2 ScreenPos;
        public Vector3 WorldPos;

        public entity(char tex, Vector3 worldPos)
        {
            texture = tex;
            WorldPos = worldPos;
            ScreenPos = Renderer.worldPosToScreenPos(worldPos);
            components = new List<iComponent>();
            componentCount = 0;
        }

        public virtual void doUpdate()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].update(this);
            }

            doRender();
        }

        public virtual void doRender()
        {
            updateScreenPos();
            Renderer.renderer.render(ScreenPos, texture);
        }

        public void addComponent(iComponent c)
        {
            components.Add(c);
            componentCount++;
            c.start(this);
        }

        public void setPosRelative(Vector3 updatedPos)
        {
            Renderer.renderer.render(ScreenPos, Renderer.renderer.clear);
            WorldPos += updatedPos;
            updateScreenPos();
        }

        public void setPos(Vector3 updatedPos)
        {
            Renderer.renderer.render(ScreenPos, Renderer.renderer.clear);
            WorldPos = updatedPos;
            updateScreenPos();
        }

        public void updateScreenPos()
        {
            ScreenPos = Renderer.worldPosToScreenPos(WorldPos);
        }

        public iComponent getComponent(Type t)
        {
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].GetType() == t)
                {
                    return components[i];
                }
            }

            throw new Exception("Component does not exist");
        }
    }
}
