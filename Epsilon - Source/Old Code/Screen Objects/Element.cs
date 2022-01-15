using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Element
    {
        public bool destroyed { get; private set; } = false;
        public Rect bounds;
        private List<Component> components = new List<Component>();
        public Game game
        {
            get
            {
                if (!destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }
                return _scene.game;
            }
        }
        public Scene scene
        {
            get
            {
                if (destroyed)
                {
                    throw new Exception("GameObject has been destroyed.");
                }
                return _scene;
            }
        }
        private Scene _scene = null;
        public GameObject(Scene scene)
        {
            if (scene is null)
            {
                throw new NullReferenceException();
            }
            _scene = scene;
            _scene.AddGameObject(this);
        }
        public void Destroy()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            foreach (Component component in components)
            {
                component.CallDestroy();
            }
            _scene.RemoveGameObject(this);
            destroyed = true;
        }
        public void Prepare()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            foreach (Component component in components)
            {
                component.CallPrepare();
            }
        }
        public void Update()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            foreach (Component component in components)
            {
                component.CallUpdate();
            }
        }
        public void Cleanup()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            foreach (Component component in components)
            {
                component.CallCleanup();
            }
        }
        public RenderTexture Render()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            RenderTexture output = new RenderTexture();
            foreach (Component component in components)
            {
                output.Merge(component.CallRender());
            }
            output.Offset(position);
            return output;
        }
        public Component GetComponent(int index)
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            if (index < 0 || index >= components.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return components[index];
        }
        public Component GetComponent(Type type)
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            if (type is null)
            {
                throw new NullReferenceException();
            }
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(type))
                {
                    return components[i];
                }
            }
            return null;
        }
        public T GetComponent<T>() where T : Component
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)components[i];
                }
            }
            return null;
        }
        public List<Component> GetComponents()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            return new List<Component>(components);
        }
        public List<Component> GetComponents(Type type)
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            if (type is null)
            {
                throw new NullReferenceException();
            }
            if (!type.IsAssignableFrom(typeof(Component)))
            {
                throw new ArgumentException();
            }
            List<Component> output = new List<Component>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(type))
                {
                    output.Add(components[i]);
                }
            }
            return output;
        }
        public List<T> GetComponents<T>() where T : Component
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            List<T> output = new List<T>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)components[i]);
                }
            }
            return output;
        }
        public int GetComponentCount()
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            return components.Count;
        }
        public void RemoveComponent(Component component)
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            if (component is null)
            {
                throw new Exception("Component was null.");
            }
            if (component.gameObject != this)
            {
                throw new Exception("Component belongs on a different GameObject.");
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] == component)
                {
                    components.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Component not found on this GameObject.");
        }
        public void AddComponent(Component component)
        {
            if (destroyed)
            {
                throw new Exception("GameObject has been destroyed.");
            }
            if (component is null)
            {
                throw new Exception("Component was null.");
            }
            if (component.gameObject != this)
            {
                throw new Exception("Component belongs to a different GameObject.");
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] == component)
                {
                    throw new Exception("Component was already added.");
                }
            }
            components.Add(component);
            component.CallInitialize();
        }
    }
}