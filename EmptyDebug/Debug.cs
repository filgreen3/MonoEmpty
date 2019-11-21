using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MonoEmpty.UI;

namespace MonoEmpty.EmptyComponent.DebugHelp
{

    public partial class Debug
    {
        public Debug(SpriteFont font,GraphicsDevice graphicsDevice)
        {
            DefaultFont = font;
            device = graphicsDevice;
            new DebugText(new Rectangle(1,1,10,10),ScreenAnchor.UP_Left);
        }

        public static GraphicsDevice device { get; private set; }

        protected static Queue<string> DebugTextes = new Queue<string>();
        protected static Queue<object> DebugTextesCaller = new Queue<object>();
        public static SpriteFont DefaultFont { get; private set; }

        public static void Add(object caller, object text)
        {
            if (!DebugTextesCaller.Contains(caller) || true)
            {
                DebugTextes.Enqueue(text.ToString());
                DebugTextesCaller.Enqueue(caller);
            }
        }


    }
   

}
