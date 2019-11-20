using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoEmpty.EmptyComponent.DebugHelp;

namespace MonoEmpty.EmptyComponent.Phisic
{
    class Body : Component, IPhisicBody, IUpdateComponent
    {

        public Body(GameObject gameObject) : base(gameObject)
        {
        }


        public Queue<Vector2> Force = new Queue<Vector2>();
        public Vector2 Velocity { get; set; }

        public override void Inicial()
        {
            ReqireComoponets = new Type[] { typeof(Transform2D) };
            base.Inicial();
            transform = gameObject.GetComponent<Transform2D>();
            Colider = gameObject.GetComponent<Colider2D>();
            world = World.GetWorld;
            Mass = 1;

        }



        public Transform transform { get; set; }
        public World world { get; set; }
        public float Mass { get; set; }
        public AbstactColider Colider { get; set; }

        public void Update()
        {
            if (!Colider.isColide())
            Force.Enqueue(-world.Force);
            
            while (Force.Count != 0)
                Velocity += Force.Dequeue();

            Velocity *= 0.99f;

            Debug.Add(this, Velocity);

            

            Debug.Add(this, Velocity);

            transform.Position += Velocity;
        }
    }


}
