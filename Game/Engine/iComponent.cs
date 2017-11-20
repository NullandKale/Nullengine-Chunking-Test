using System;
namespace Engine
{
    public interface iComponent
    {
        void start(entity e);
        void update(entity e);
    }
}
