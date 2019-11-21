using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmpty.UI;

namespace MonoEmpty.EmptyComponent.DebugHelp
{

    public partial class Debug
    {
        public class DebugDrawRectangle : Component, IDrawComponet
        {
            protected override Type[] ReqireComoponets => null; 

            Transform2D transform;
            public Point Size;

            public DebugDrawRectangle(GameObject gameObject) : base(gameObject)
            {
               
            }
            public override void Inicial()
            {          
                base.Inicial();
                transform = gameObject.GetComponent<Transform2D>();
            }

            private Texture2D GetTexture
            {
                get
                {
                    Size = (transform.Rect.Size.ToVector2() / Transform.SizePower).ToPoint()+new Point(1,1);
                    return FoxUITextureGen.CreateTexture(device, Size.X, Size.Y, pixel => DebugDrawer.RectWire(pixel,Size));
                    
                }

            }
           


            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(GetTexture ,destinationRectangle: transform.Rect,color: Color.White);
            }
        }


    }
   

}
