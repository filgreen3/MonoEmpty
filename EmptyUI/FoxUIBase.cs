using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using MonoEmpty.EmptyComponent;

namespace MonoEmpty.UI
{
    public static class UIManager
    {
        internal static MouseState prevMouseState, curMouseState;

        internal static List<IGraphicUI> graphicUIs = new List<IGraphicUI>();
        internal static List<IPointerClick> clickableUIs = new List<IPointerClick>();
        internal static List<IPointerDrag> dragableUIs = new List<IPointerDrag>();
        public static SpriteBatch spriteBatchUI;

        public static void DrawUI()
        {
            spriteBatchUI.Begin(samplerState: SamplerState.PointClamp);
            graphicUIs.ForEach((item) => item.Draw(spriteBatchUI));
            spriteBatchUI.End();
        }
        public static void UpdateUI()
        {
            prevMouseState = curMouseState;
            curMouseState = Mouse.GetState();


            var mouseRect = new Rectangle(curMouseState.Position, new Point(1, 1));

            foreach (var item in clickableUIs)
            {
                if (mouseRect.Intersects(item.Rect)
                    && curMouseState.LeftButton.Equals(ButtonState.Pressed)
                    && prevMouseState.LeftButton.Equals(ButtonState.Released))
                    item.OnClick();
            }
            foreach (var item in dragableUIs)
            {
                if ((mouseRect.Intersects(item.Rect) || item.IsDrag)
                    && curMouseState.LeftButton.Equals(ButtonState.Pressed))
                {
                    item.IsDrag = true;
                    item.OnDrag(new PointData(prevMouseState, curMouseState));
                }
                else
                {
                    item.IsDrag = false;
                }
            }
        }
    }



    public abstract class GraphicUI : ITransform, IGraphicUI
    {
        public ScreenAnchor screenAnchor;


        public List<ITransform> ChildField = new List<ITransform>();

        public float angle { get; set; }
        public Vector2 LocalPosition { get; set; }
        public Vector2 Offset { get; set; }

        public Vector2 originAnchor { get; set; }

        public ITransform parent { get; set; }

        public Vector2 Position
        {
            get => LocalPosition + ((parent != null) ? parent.Position + parent.originAnchor : Vector2.Zero);

            set
            {

                value.X = (int)(value.X / Transform.SizePower);
                value.Y = (int)(value.Y / Transform.SizePower);

                LocalPosition = value * Transform.SizePower;
            }

        }
        public Rectangle Rect { get; set; }
        public List<ITransform> Child { get => ChildField; set => ChildField = value; }

        public void SetParent(ITransform tr)
        {
            if (tr != null)
                tr.Child.Add(this);
            else if (parent != null)
                parent.Child.Remove(this);

            parent = tr;

            var pivot = AnchorUtility.GetPivot(screenAnchor);

            LocalPosition = Rect.Size.ToVector2() * pivot;

            originAnchor = AnchorUtility.GetPosition(screenAnchor, parent);

        }



        public GraphicUI(Rectangle rectangle, ScreenAnchor anchor)
        {


            if (this is IPointerClick)
                UIManager.clickableUIs.Add((IPointerClick)this);
            if (this is IPointerDrag)
                UIManager.dragableUIs.Add((IPointerDrag)this);


            UIManager.graphicUIs.Add(this);
            Rect = rectangle;
            this.screenAnchor = anchor;
            Position = rectangle.Location.ToVector2();

            originAnchor = AnchorUtility.GetPosition(anchor);

            var pivot = AnchorUtility.GetPivot(screenAnchor);

            Offset = Rect.Size.ToVector2() * pivot;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public Transform[] GetChild()
        {
            throw new NotImplementedException();
        }


        public void SetPosition()
        {
            var RectUI = Rect;
            RectUI.X = (int)(Position.X - Offset.X + originAnchor.X);
            RectUI.Y = (int)(Position.Y - Offset.Y + originAnchor.Y);
            Rect = RectUI;
        }
    }

    public static class AnchorUtility
    {
        public static Viewport GetViewport => UIManager.spriteBatchUI.GraphicsDevice.Viewport;
        public static Vector2 GetPosition(ScreenAnchor anchor)
        {
            var viewport = GetViewport;
            switch (anchor)
            {
                case ScreenAnchor.UP_Left:
                    return Vector2.Zero;

                case ScreenAnchor.UP_Middle:
                    return new Vector2(viewport.Width / 2, 0);

                case ScreenAnchor.UP_Right:
                    return new Vector2(viewport.Width, 0);

                case ScreenAnchor.Middle_Left:
                    return new Vector2(0, viewport.Height / 2);

                case ScreenAnchor.Middle_Middle:
                    return new Vector2(viewport.Width / 2, viewport.Height / 2);

                case ScreenAnchor.Middle_Right:
                    return new Vector2(viewport.Width, viewport.Height / 2);

                case ScreenAnchor.Down_Left:
                    return new Vector2(0, viewport.Height);

                case ScreenAnchor.Down_Middle:
                    return new Vector2(viewport.Width / 2, viewport.Height);

                case ScreenAnchor.Down_Right:
                    return new Vector2(viewport.Width, viewport.Height);

            }
            return Vector2.Zero;
        }
        public static Vector2 GetPosition(ScreenAnchor anchor, ITransform transform)
        {
            transform.SetPosition();

            //throw new Exception(transform.Rect.ToString()) ;
            var zeroX = transform.Rect.X;
            var zeroY = transform.Rect.Y;
            switch (anchor)
            {
                case ScreenAnchor.UP_Left:
                    return new Vector2(zeroX, zeroY);
                case ScreenAnchor.UP_Middle:
                    return new Vector2(zeroX + transform.Rect.Width / 2, zeroY);

                case ScreenAnchor.UP_Right:
                    return new Vector2(zeroX + transform.Rect.Width, zeroY);

                case ScreenAnchor.Middle_Left:
                    return new Vector2(zeroX, zeroY + transform.Rect.Height / 2);

                case ScreenAnchor.Middle_Middle:
                    return new Vector2(zeroX + transform.Rect.Width / 2, zeroY + transform.Rect.Height / 2);

                case ScreenAnchor.Middle_Right:
                    return new Vector2(zeroX + transform.Rect.Width, zeroY + transform.Rect.Height / 2);

                case ScreenAnchor.Down_Left:
                    return new Vector2(zeroX, zeroY + transform.Rect.Height);

                case ScreenAnchor.Down_Middle:
                    return new Vector2(zeroX + transform.Rect.Width / 2, zeroY + transform.Rect.Height);

                case ScreenAnchor.Down_Right:
                    return new Vector2(zeroX + transform.Rect.Width, zeroY + transform.Rect.Height);

            }
            return Vector2.Zero;
        }

        public static Vector2 GetPivot(ScreenAnchor anchor)
        {
            switch (anchor)
            {
                case ScreenAnchor.UP_Left:
                    return Vector2.Zero;

                case ScreenAnchor.UP_Middle:
                    return new Vector2(.5f, 0);

                case ScreenAnchor.UP_Right:
                    return new Vector2(1, 0);

                case ScreenAnchor.Middle_Left:
                    return new Vector2(0, .5f);

                case ScreenAnchor.Middle_Middle:
                    return new Vector2(.5f, .5f);

                case ScreenAnchor.Middle_Right:
                    return new Vector2(1, .5f);

                case ScreenAnchor.Down_Left:
                    return new Vector2(0, 1);

                case ScreenAnchor.Down_Middle:
                    return new Vector2(.5f, 1);

                case ScreenAnchor.Down_Right:
                    return new Vector2(1, 1);

            }
            return Vector2.Zero;
        }
    }

    public interface IGraphicUI : IDrawComponet
    {
        Rectangle Rect { get; }
    }



    public enum ScreenAnchor
    {
        UP_Left,
        UP_Middle,
        UP_Right,
        Middle_Left,
        Middle_Middle,
        Middle_Right,
        Down_Left,
        Down_Middle,
        Down_Right

    }
}