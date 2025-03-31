using Frontend.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class SpriteRenderer : Component
    {

        public Texture2D Sprite { get; set; }

        public float Rotate { get; set; }
        public float Scale { get; set; }

        public Color Color { get; set; }

        public Vector2 Origin { get; set; }


        public override void Start()
        {
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        public void SetSprite(string spriteName, float rotate, float scale,Color color,ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spriteName);
            Rotate = rotate;
            Scale = scale;
            this.Color = color;
        }




        public override void Draw(SpriteBatch spriteBatch)
        {
            //this is to draw the sprites
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color, Rotate, Origin, Scale, SpriteEffects.None, 1);
        }
    }
}
