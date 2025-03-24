using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class Transform
    {
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public void Translate(Vector2 translation)
        {
            Position += translation;
        }
    }
}
