using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.ComponentPattern;

namespace Frontend
{
    public class GameObject : ICloneable
    {


        private List<Component> components = new List<Component>();
        public string tag;

        public Transform Transform { get; set; } = new Transform();

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public Component AddComponent(Component component)
        {
            component.GameObject = this;
            components.Add(component);

            return component;


        }

        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }


        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Awake();
            }

        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }

        public object Clone()
        {
            GameObject go = new GameObject();

            foreach (Component component in components)
            {
                go.AddComponent(component.Clone() as Component);
            }

            return go;
        }

    }
}
