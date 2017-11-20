using System;
namespace TheForrest.Entities
{
    public class playerCharacter : Engine.entity
    {
        public playerCharacter(char tex, Vector2 worldPos) : base(tex, worldPos)
        {
            addComponent(new Components.cPlayerControl());
        }
    }
}
