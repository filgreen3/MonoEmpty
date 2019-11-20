using Fox.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoEmpty.Component.Debug
{

    public class Debug
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


        public class DebugDrawRectangle : Component, IDrawComponet
        {

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

        class DebugText : Text
        {
            private const int numStrings = 30;
            public override string text
            {
                get
                {
                    var result = string.Empty;
                    for (int i = 0; i <= numStrings; i++)
                    {
                        if (DebugTextesCaller.Count > 0)
                            result += String.Join("_", DebugTextes.Dequeue(), DebugTextesCaller.Dequeue().ToString()) + '\n';

                    }
                    return result;
                }
            }

            public DebugText(Rectangle rectangle, ScreenAnchor anchor) : base(rectangle, anchor) { }
            public override void Draw(SpriteBatch spriteBatch)
            {
                var t = text;
                spriteBatch.DrawString(DefaultFont, t, Position + Vector2.One * 0.6f, Color.Black);
                spriteBatch.DrawString(DefaultFont, t, Position, Color.Red);



            }




        }


    }
    public class DebugTextPositionObj : Text
    {
        public DebugTextPositionObj(Transform parent) : base(new Rectangle(0, 0, 1, 1), ScreenAnchor.Middle_Middle)
        {
            SetParent(parent);
        }

        public override Color FontColor => Color.White;
        public override string text => parent.Rect.Location.ToString();
        public DebugTextPositionObj(Rectangle rectangle, ScreenAnchor anchor) : base(rectangle, anchor) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Debug.DefaultFont, text, Position + Vector2.One * 0.6f - new Vector2(text.Length * 4, 0), Color.DarkRed);
            spriteBatch.DrawString(Debug.DefaultFont, text, Position - new Vector2(text.Length * 4, 0), Color.White);
        }



    }
   

}
