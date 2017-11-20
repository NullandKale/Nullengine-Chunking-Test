using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class EntityMap
    {
        public static EntityMap e;
        public Dictionary<Vector2, Dictionary<Vector2, cEntityMapper>> boundingBoxes;
        public int boundingBoxSize;

        public EntityMap(int boundSize)
        {
            if (e == null)
            {
                e = this;
            }
            else
            {
                throw new Exception("Singleton Exception @ EntityMap.cs");
            }
            boundingBoxes = new Dictionary<Vector2, Dictionary<Vector2, cEntityMapper>>();
            boundingBoxSize = boundSize;
        }

        public void UpdateEntity(cEntityMapper c)
        {
            if(boundingBoxes.ContainsKey(c.key))
            {
                if(!boundingBoxes[c.key].ContainsKey(c.worldPos))
                {
                    boundingBoxes[c.key].Add(c.worldPos, c);
                }
            }
            else
            {
                boundingBoxes.Add(c.key, new Dictionary<Vector2, cEntityMapper>());
                boundingBoxes[c.key].Add(c.worldPos, c);
            }
        }

        public void RemoveEntity(cEntityMapper c)
        {
            if(boundingBoxes.ContainsKey(c.key))
            {
                if(boundingBoxes[c.key].ContainsKey(c.worldPos))
                {
                    boundingBoxes[c.key].Remove(c.worldPos);
                }

                if(boundingBoxes[c.key].Count == 0)
                {
                    boundingBoxes.Remove(c.key);
                }
            }
        }

        public static Vector2 getKey(Vector2 worldLoc)
        {
            return new Vector2(worldLoc.x / e.boundingBoxSize, worldLoc.y / e.boundingBoxSize);
        }

        public cEntityMapper CheckLocation(Vector2 worldLoc)
        {
            Vector2 key = getKey(worldLoc);
            if(boundingBoxes.ContainsKey(key) && boundingBoxes[key].ContainsKey(worldLoc))
            {
                return boundingBoxes[key][worldLoc];
            }
            else
            {
                return null;
            }
        }
    }
}
