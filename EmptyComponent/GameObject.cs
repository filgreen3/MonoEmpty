using System;

namespace MonoEmpty.Component
{
    public class GameObject
    {
        public Transform2D transform => GetComponent<Transform2D>();

        public GameObject(params Type[] components)
        {
            AddComponent<Transform2D>();
            foreach (var comp in components)
                AddComponent(comp);
        }


        public Component[] Components = new Component[0];
        public T AddComponent<T>() where T : Component
        {
            if (HasComponent(typeof(T))) throw new Exception("Already have this component");
            var component = (T)Activator.CreateInstance(typeof(T), new object[] { this });
            Array.Resize(ref Components, Components.Length + 1);
            Components[Components.Length - 1] = component;          
            return component;
        }
        public void AddComponent(Type type)
        {
            if (HasComponent(type)) return;
            var component = Activator.CreateInstance(type, new object[] { this });
            Array.Resize(ref Components, Components.Length + 1);
            Components[Components.Length - 1] = (Component)component;
        }

        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i] is T) return (T)Components[i];
            }
            return null;
        }
        public bool HasComponent(Type type)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i].GetType() == type) return true;
            }
            return false;
        }   
    }
}
