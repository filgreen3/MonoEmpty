﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEmpty.EmptyComponent
{
    public abstract class Component
    {
        protected virtual Type[] ReqireComoponets => null;
        public GameObject gameObject;
        protected Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
            if (this is IDrawComponet) GameCenter.intance.renderers.Add((IDrawComponet)this);
            if (this is IUpdateComponent) GameCenter.intance.updateComponents.Add((IUpdateComponent)this);
            Inicial();
        }
        public virtual void Inicial()
        {      
            CallRequramentComponet();
        }
        protected void CallRequramentComponet()
        {
            if (ReqireComoponets == null) return;
            foreach (var comp in ReqireComoponets)
            {
                if (!gameObject.HasComponent(comp))
                    gameObject.AddComponent(comp);

            }
        }


    }
}
