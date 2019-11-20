using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoEmpty.EmptyComponent
{
    public abstract class Transform : Component,ITransform
    {
        public Transform(GameObject gameObject) : base(gameObject)
        {
        }
        public const int SizePower = 4;

        public Vector2 originAnchor { get; protected set; }
        internal List<Transform> Child = new List<Transform>();
        public Transform[] GetChild() => Child.ToArray();

        public virtual void SetParent(Transform transform)
        {
            if (transform != null)
                transform.Child.Add(this);
            else if (parent != null)
                parent.Child.Remove(this);

            parent = transform;

        }


        public float angle { get; set; }

        public virtual void SetPosition()
        {
            var RectUI = Rect;
            RectUI.X = (int)(Position.X - Offset.X + originAnchor.X);
            RectUI.Y = (int)(Position.Y - Offset.Y + originAnchor.Y);
            Rect = RectUI;
        }

        public void SetParent(ITransform transform)
        {
            throw new NotImplementedException();
        }

        public Rectangle Rect { get;set; }
        public Vector2 Position
        {
            get => LocalPosition + ((parent != null) ? parent.Position + parent.originAnchor : Vector2.Zero);

            set
            {

                value.X = (int)(value.X / SizePower);
                value.Y = (int)(value.Y / SizePower);

                LocalPosition = value * SizePower;
            }

        }
        public Vector2 LocalPosition { get; set; }

        public Vector2 Offset { get; set; }

        public ITransform parent { get; set; }

        List<ITransform> ITransform.Child { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }






}