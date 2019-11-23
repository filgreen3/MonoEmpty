using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoEmpty.EmptyComponent;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Example
{
    public class RotationAtMouse : Component, IUpdateComponent
    {
        public RotationAtMouse(GameObject gameObject) : base(gameObject) { }
        private float prevAngle;
        private float t;
        Transform2D tr;

        protected override Type[] ReqireComoponets => null;

        public override void Inicial()
        {
            base.Inicial();
            tr = gameObject.transform;
        }

        public void Update()
        {
            tr.angle += AngleMouse() - tr.angle;

            //Fox.DebugHelper.Debug.Add(this, tr.angle);
        }
        private float AngleMouse()
        {

            var tr = gameObject.transform;
            var mpos = Mouse.GetState().Position.ToVector2() - Game1.inputOffset - tr.Rect.Location.ToVector2() - tr.Rect.Size.ToVector2() / 2f;// - Game1.inputOffset 
            mpos /= mpos.Length();

            mpos.Y = (mpos.Y + 1f) / 2f;
            return (mpos.X > 0) ? MathHelper.Pi * mpos.Y : MathHelper.TwoPi - MathHelper.Pi * mpos.Y;


        }
    }
}
