using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmpty.UI;

namespace MonoEmpty.EmptyComponent.DebugHelp
{
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
