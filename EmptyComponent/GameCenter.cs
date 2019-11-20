using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoEmpty.EmptyComponent
{
    public class GameCenter
    {
        public GameCenter()
        {
            intance = this;
        }

        public static GameCenter intance;
        public List<IDrawComponet> renderers = new List<IDrawComponet>();
        public List<IUpdateComponent> updateComponents = new List<IUpdateComponent>();

        public static void Draw(SpriteBatch spriteBatch) => intance.renderers.ForEach(item => item.Draw(spriteBatch));
        public static void Update() => intance.updateComponents.ForEach(item => item.Update());


    }
}
