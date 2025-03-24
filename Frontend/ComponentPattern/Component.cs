using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ComponentPattern
{

    public abstract class Component
    {
        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        public GameObject GameObject { get; set; }


        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        //Shallow Copy Lavet. Dvs der refereres til den originale component og kopieres. 
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

    }

}
