using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoEmpty.Component;

namespace MonoEmpty.Component.Phisic
{
    class Colider2D : AbstactColider
    {
        public Colider2D(GameObject gameObject) : base(gameObject)
        {
        }
        public Transform2D transform2D;


        public override void Inicial()
        {
            base.Inicial();
            transform2D = gameObject.GetComponent<Transform2D>();

            World.GetWorld.Coliders.Add(this);
        }

        public override bool isColide()
        {
            var coliders = World.GetWorld.Coliders;
            int count = 0;
            foreach (var item in coliders)
            {
                if (item == this) continue;         
                    count++;                  
            }
            DebugHelper.Debug.Add(this,count);
            return count > 0;
        }
     
    }
}
