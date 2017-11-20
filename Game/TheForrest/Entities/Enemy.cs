using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheForrest.Entities
{
    public class Enemy : Engine.entity
    {
        public Enemy(char tex, Vector3 worldPos) : base(tex, worldPos)
        {
            addComponent(new Engine.cEntityMapper());
        }
    }
}
