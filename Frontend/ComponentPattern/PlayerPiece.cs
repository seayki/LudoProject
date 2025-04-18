using Frontend.ControllerPattern;
using Frontend.FactoryPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Frontend.ComponentPattern
{
    public class PlayerPiece:Component
    {
        private string spriteName;
        private Vector2 position;
        private Vector2 scale;
        private Texture2D sprite;
        private Texture2D pixel;
        private Rectangle rectangle;
        private Vector2 origin;
        private bool down;

        Guid pieceID { get; set; }
        private TileColor pieceColor;

        public bool moveAble = false;
        private int index;
        private bool moveToColorZone=false;
        private int hometilePosIndex;

        public PlayerPiece(TileColor pieceColor, int hometileIndex)
        {

            this.hometilePosIndex = hometileIndex;
            this.pieceColor = pieceColor;

        }

        public void MakeMoveAble()
        {
            moveAble = true;
          
        }


        public void RemoveFromGame()
        {
            GameWorld.Instance.Destroy(GameObject);
        }

        public void MoveToColorZone()
        {
            moveToColorZone = true;
        }

        public void MoveToHomeTiles()
        {
               
            GameObject.Transform.Position = GameWorld.Instance.homeTiles[pieceColor][hometilePosIndex].Transform.Position;

            this.position = GameObject.Transform.Position;
            rectangle = new Rectangle((int)(position.X - sprite.Width / 2), (int)(position.Y - sprite.Height / 2), (int)(sprite.Width * scale.X), (int)(sprite.Height * scale.Y));


        }

        public override void Start()
        {
            this.position = GameObject.Transform.Position;
            SpriteRenderer sr = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();
            this.scale = new Vector2(sr.Scale, sr.Scale);
            this.spriteName = sr.Sprite.Name;
            this.sprite = sr.Sprite;
            pixel = GameWorld.Instance.Content.Load<Texture2D>("pixel");
      
            origin = new Vector2(sprite.Width * scale.X / 2, sprite.Height * scale.Y / 2);
        }

        public override void Update()
        {

           

            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed && moveAble == true)
            {

                down = true;

            }
            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Released && down)
            {

                //Debug.Write("down");

                if (moveToColorZone == false)
                {
                    GameObject.Transform.Position = GameWorld.Instance.tiles[index].Transform.Position;
                }
                else
                {
                    GameObject.Transform.Position = GameWorld.Instance.colorTiles[pieceColor][index].Transform.Position;
                }

                this.position = GameObject.Transform.Position;
                rectangle = new Rectangle((int)(position.X - sprite.Width / 2), (int)(position.Y - sprite.Height / 2), (int)(sprite.Width * scale.X), (int)(sprite.Height * scale.Y));

                moveAble = false;
                down = false;
            }


        }

     


    }
}
