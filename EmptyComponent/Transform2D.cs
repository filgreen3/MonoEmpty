using Microsoft.Xna.Framework;
using System;

namespace MonoEmpty.EmptyComponent
{


    public class Transform2D : Transform
    {
        protected override Type[] ReqireComoponets => null;

        public Transform2D(GameObject gameObject):base(gameObject)
        {
          
        }

        public void LookAt(Vector2 toVector)
        {

            var vec = (Position + new Vector2(0, -1)) - Position;
            var mpos = toVector - Rect.Location.ToVector2() - Rect.Size.ToVector2() / 2;

            mpos /= mpos.Length();

            var dot = Vector2.Dot(vec, mpos);

            var normal = (float)Math.Acos(dot);
            var revers = (float)(MathHelper.TwoPi - Math.Acos(dot));


            angle = (mpos.X > 0) ? normal : revers;
        }

        public void SetPosition(Vector2 size)
        {
            var mp = (float)SizePower;
            int SizeX = (int)(size.X * mp);
            int SizeY = (int)(size.Y * mp);
            var RectUI = Rect;
            RectUI.Size = new Point(SizeX, SizeY);
            RectUI.X = (int)(Position.X - Offset.X + originAnchor.X);
            RectUI.Y = (int)(Position.Y - Offset.Y + originAnchor.Y);
            Rect = RectUI;

            Offset = new Vector2(SizeX / 2, SizeY / 2);
        }
    }






}