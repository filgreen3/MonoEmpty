using Fox.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Fox.DebugHelper;

namespace Fox.UI
{
    public class Button : Box, IPointerClick
    {
        public delegate void ClickHandler();
        public event ClickHandler Click;

        public Button(Texture2D texture, Point pos, ScreenAnchor anchor) : base(texture, pos, anchor)
        {

        }

        public void OnClick()
        {
            Click?.Invoke();


        }
    }

    public class MovableHandle : Box, IPointerDrag
    {
        public MovableHandle(Texture2D texture, Point pos, ScreenAnchor anchor) : base(texture, pos, anchor)
        {

        }

        public bool IsDrag { get; set; }

        public void OnDrag(PointData point)
        {
            Debug.Add(this, Rect.Location.ToString());
            Offset = AnchorUtility.GetPivot(ScreenAnchor.Middle_Middle) * Rect.Size.ToVector2();
            Position = point.Position.ToVector2() - Offset - (originAnchor - Offset);

        }
    }


   

    public class Box : GraphicUI
    {
        public Texture2D texture;

        public Box(Texture2D texture, Rectangle rectangle, ScreenAnchor anchor) : base(rectangle, anchor)
        {
            this.texture = texture;

        }
        public Box(Texture2D texture, Point pos, ScreenAnchor anchor) : base(new Rectangle(pos.X, pos.Y, texture.Width *Transform.SizePower, texture.Height * Transform.SizePower), anchor)
        {
            this.texture = texture;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            SetPosition();

            if (parent != null)
            {
                //throw new Exception($"{Rect}{parent.Rect}");
            }
            spriteBatch.Draw(texture, Rect, Color.White);
        }
    }


    public class LayoutGroupGrid : Box
    {
        public int GridSize = 16;
        public int Spacing = 10;


        public void GetInGroup(Box box)
        {
            box.screenAnchor = ScreenAnchor.UP_Left;
            box.SetParent(this);
            SetOrder();
        }


        public void SetOrder()
        {
            var child = GetChild();
            for (int i = 0; i < child.Length; i++)
            {
                child[i].LocalPosition =Vector2.UnitX*( i * GridSize + Spacing);

            }
        }

        public LayoutGroupGrid(Texture2D texture, Rectangle rectangle, ScreenAnchor anchor) : base(texture, rectangle, anchor)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Debug.Add(this, Rect.Location);
            var child = GetChild();
            for (int i = 0; i < child.Length; i++)
            {
                Debug.Add(this, child[i].Rect.Location);
            }
        }
    }



    public class Text : GraphicUI
    {
        public virtual string text { get; set; }
        public virtual SpriteFont Font { get; set; }
        public virtual Color FontColor { get; set; }
        public Text(Rectangle rectangle, ScreenAnchor anchor) : base(rectangle, anchor) { }

        public  void SetParent(Transform transform)
        {
            base.SetParent(transform);
            LocalPosition = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Font == null) Font = Debug.DefaultFont;
            if (text == null) text = "Text";

            Debug.Add(this, Position.ToString());
            Debug.Add(this, LocalPosition + "Raw");
            spriteBatch.DrawString(Font, text, Position - new Vector2(text.Length * 4, 0), FontColor);
        }
    }

}
