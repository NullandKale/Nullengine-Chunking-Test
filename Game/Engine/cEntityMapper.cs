using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class cEntityMapper : iComponent
    {
        public Vector2 key;
        public Vector3 worldPos;

        public void start(entity e)
        {

        }

        public void update(entity e)
        {
            if(e.WorldPos != worldPos)
            {
                worldPos = e.WorldPos;
                key = EntityMap.getKey(worldPos.toVec2());
                EntityMap.e.UpdateEntity(this);
            }
        }
    }
}
