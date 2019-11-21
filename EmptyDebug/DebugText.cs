using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoEmpty.UI;

namespace MonoEmpty.EmptyComponent.DebugHelp
{

    public partial class Debug
    {
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
}
