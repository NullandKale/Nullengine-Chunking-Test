using System;
namespace TheForrest.Entities
{
    public class playerCharacter : Engine.entity
    {
        public Components.cPlayerControl controller;
        public Engine.cEntityMapper mapper;

        public playerCharacter(char tex, Vector3 worldPos) : base(tex, worldPos)
        {
            mapper = new Engine.cEntityMapper();
            controller = new Components.cPlayerControl(mapper);
            addComponent(controller);
            addComponent(mapper);
        }
    }
}
