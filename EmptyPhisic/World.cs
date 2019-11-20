using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoEmpty.Component.Phisic
{
    class World
    {
        public List<AbstactColider> Coliders =new List<AbstactColider>();

        public Vector2 Force;
        public World(Vector2 Force)
        {
            GetWorld = this;
            this.Force = Force;
        }

        public static World GetWorld { get; private set; }
    }


    class Chunk
    {
        public Rectangle Rect;


    }
}
