using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Frontend.ControllerPattern
{
    public class Button
    {
        private IButton button;
        private string spriteName;
        private Vector2 position;
        private Texture2D sprite;
        private Rectangle rectangle;
        private bool down;
        private Vector2 origin;

        public Vector2 Scale { get; private set; }

        public Button(string spriteName, IButton button, Vector2 position,Vector2 scale)
        {
            this.button = button;
            this.spriteName = spriteName;
            this.position = position;
            this.Scale = scale;
           
        }

        public void Update()
        {

            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                down = true;

            }
            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Released && down)
            {

                //Debug.Write("down");
                button.DoAction();
                down = false;
            }


        }

        public void LoadContent()
        {
            sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
            
            rectangle = new Rectangle((int)(position.X - sprite.Width / 2), (int)(position.Y - sprite.Height / 2), (int)(sprite.Width*Scale.X), (int)(sprite.Height*Scale.Y));
            origin = new Vector2(sprite.Width*Scale.X / 2, sprite.Height*Scale.Y / 2);

            Debug.WriteLine("width:"+(int)(sprite.Width*Scale.X) + "height:" + (int)(sprite.Height*Scale.Y));

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, Scale, SpriteEffects.None, 1);


        }

    }
}
