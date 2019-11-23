using System;

namespace MonoEmpty.EmptyComponent
{
    public class GameObject
    {
        public Transform2D transform => GetComponent<Transform2D>();

        /// <summary>
        /// Создает gameobject с указаными типами
        /// </summary>
        /// <param name="components"></param>
        public GameObject(params Type[] components)
        {
            AddComponent<Transform2D>();
            foreach (var comp in components)
                AddComponent(comp);
        }


        private Component[] Components = new Component[0];

        /// <summary>
        /// Добовляет компонент
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>() where T : Component
        {
            if (HasComponent(typeof(T))) throw new Exception("Already have this component");
            var component = (T)Activator.CreateInstance(typeof(T), new object[] { this });
            Array.Resize(ref Components, Components.Length + 1);
            Components[Components.Length - 1] = component;          
            return component;
        }
        /// <summary>
        /// Добовляет компонент с указаным типом
        /// </summary>
        /// <param name="type"></param>
        public void AddComponent(Type type) 
        {
            if (type != typeof(Component)|| HasComponent(type)) return;
            var component = Activator.CreateInstance(type, new object[] { this });
            Array.Resize(ref Components, Components.Length + 1);
            Components[Components.Length - 1] = (Component)component;
        }
        /// <summary>
        /// Возвращает указаный компонент, сохраняя послендий компонет в кэше
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i] is T)
                    return (T)Components[i];
            }
            return null;
        }
        /// <summary>
        /// Проверяеет наличие компонента
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
