using Common.DTOs;
using Common.Enums;
using Frontend.ControllerPattern;
using Frontend.FactoryPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    public class PlayerPiece : Component
    {
        private string spriteName;
        private Vector2 position;
        private Vector2 scale;
        private Texture2D sprite;
        private Texture2D pixel;
        private Rectangle rectangle;
        private Vector2 origin;
        private bool down;

        public Guid pieceID { get; set; }

        private bool isInplay;
        private ColourEnum pieceColor;
        private int startTileIndex;
        public bool moveAble = false;
        private int index;
        private bool moveToColorZone = false;
        private int hometilePosIndex;
        private bool isFinished;

        public PlayerPiece(ColourEnum pieceColor, int hometileIndex, int startTileIndex,Guid pieceId,bool isInplay,bool isFinished)
        {

            this.hometilePosIndex = hometileIndex;
            this.pieceColor = pieceColor;
            this.startTileIndex = startTileIndex;
            this.pieceID = pieceID;
            this.isInplay = isInplay;
            this.isFinished = isFinished;

        }

        public void MakeMoveAble()
        {
            moveAble = true;

        }

        public void MoveToStartTile()
        {
            GameObject.Transform.Position = GameWorld.Instance.tiles[startTileIndex].Transform.Position;

        }

        public void RemoveFromGame()
        {
            IsEnabled = false;
            GameObject.GetComponent<SpriteRenderer>().IsEnabled = false;

            foreach (var item in GameWorld.Instance.playerPieces[pieceColor])
            {
               PlayerPiece p=(PlayerPiece)item.GetComponent<PlayerPiece>();

                if(p.pieceID==pieceID)
                {
                    GameWorld.Instance.playerPieces[pieceColor].Remove(item);
                }
            }
           

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

        public void UpdatePiece(PieceDTO updatedPiece)
        {
            if(updatedPiece.IsInPlay==false)
            {
                MoveToHomeTiles();
            }
            else if (updatedPiece.IsInPlay!=isInplay)
            {
                isInplay = updatedPiece.IsInPlay;
                MoveToStartTile();

            }

            if(updatedPiece.IsFinished)
            {
                RemoveFromGame();
            }

            if(updatedPiece.PosIndex.Colour==pieceColor)
            {
                MoveToColorZone();
            }

            index = updatedPiece.PosIndex.Index;
            



            
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

            MoveToHomeTiles();
        }

        public override void Update()
        {

           

            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed && moveAble == true)
            {

                down = true;

            }
            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Released && down)
            {


                //SEND PIECEID TO BACKEND AND GET A LIST OF PIECEDTO THAT REPRESENTS THE UPDATED PIECES

                //TEMPORARY 


                List<PieceDTO> piecesToUpdate = new List<PieceDTO>() { new PieceDTO() { ID = pieceID, PosIndex = new PosIndex { Colour = pieceColor, Index = 4 } } };


                //TEMPORARY

                GameWorld.Instance.UpdatePieces(piecesToUpdate);





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




                //SEND NEXT TURN TO BACKEND AND UPDATE WHOS TURN IT IS BASED ON A PLAYER GUID

                //TEMPORARY
                Guid nextPlayersTurn = GameWorld.Instance.playerColors.Keys.ToList()[1];
                //TEMPORARY

                GameWorld.Instance.UpdateCurrentPlayer(nextPlayersTurn);

            }


        }

     


    }
}
