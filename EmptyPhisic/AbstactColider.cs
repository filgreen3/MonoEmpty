using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoEmpty.Component;

namespace MonoEmpty.Component.Phisic
{
    public abstract class AbstactColider : Component
    {
        protected AbstactColider(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Inicial()
        {
            base.Inicial();
            transform = gameObject.GetComponent<Transform2D>();
            spritedata = gameObject.GetComponent<Sprite>();
        }

        public Transform2D transform;
        public Sprite spritedata;
        public abstract bool isColide();

        public bool IntersectPixels(AbstactColider abstactColiderB)
        {

            var rectangleA = transform.Rect;
            var rectangleB = abstactColiderB.transform.Rect;


            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point

                }
            }

            // No intersection found
            return false;
        }

    }


    public struct ColisionData
    {
        public AbstactColider[] ContactedColider;

        public ColisionData(AbstactColider[] contactedColider)
        {
            ContactedColider = contactedColider;
        }
    }
}
