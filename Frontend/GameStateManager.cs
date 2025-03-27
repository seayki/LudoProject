using Frontend.FactoryPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public enum GameState
    {
        Playing,
        Start,
        ChooseColor,
    }

    public class GameStateManager
    {
        // currentState gemmer spillets nuværende stadie.
        private GameState currentState;

        //public SpriteFont spriteFont { get; private set; }

        // constructoren der initializer stadiet som spillet starter i.
        public GameStateManager()
        {
            currentState = GameState.Start;
        }

        //ChangeGameState tillader at man kan ændre på stadiet fx Menu til Playing, eller omvendt.
        public void ChangeGameState(GameState newState)
        {
            currentState = newState;
        }

        public void Loadcontent()
        {
            switch (currentState)
            {

                case GameState.Start:

                    foreach (var go in GameWorld.Instance.gameObjects_MainMenu)
                    {
                        go.Start();
                    }
                    foreach (var item in GameWorld.Instance.buttons_MainMenu)
                    {
                        item.LoadContent();
                    }
                    break;

                case GameState.ChooseColor:

                    foreach (var go in GameWorld.Instance.gameObjects_ChooseColor)
                    {
                        go.Start();
                    }


                    foreach (var item in GameWorld.Instance.buttons_ColorSelection)
                    {
                        item.LoadContent();
                    }

                    break;

                case GameState.Playing:

                 

                    foreach (GameObject go in GameWorld.Instance.gameObjects_Playing)
                    {
                        go.Start();
                    }

                    foreach (var go in GameWorld.Instance.tiles)
                    {
                        go.Start();
                    }

                    foreach (var go in GameWorld.Instance.colorTiles)
                    {
                        foreach (var item in go.Value)
                        {
                            item.Start();
                        } 
                    }

                    foreach(var item in GameWorld.Instance.buttons_Playing)
                    {
                        item.LoadContent();
                    }
                    break;

            }
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.Start:

                    foreach (var go in GameWorld.Instance.gameObjects_MainMenu)
                    {
                        go.Update(gameTime);
                    }

                    foreach (var item in GameWorld.Instance.buttons_MainMenu)
                    {
                        item.Update();
                    }
                    break;


                case GameState.ChooseColor:

                    foreach (var go in GameWorld.Instance.gameObjects_ChooseColor)
                    {
                        go.Update(gameTime);
                    }


                    foreach (var item in GameWorld.Instance.buttons_ColorSelection)
                    {
                        item.Update();
                    }

                    break;

                case GameState.Playing:
                    for (int i = 0; i < GameWorld.Instance.gameObjects_Playing.Count; i++)
                    {
                        GameWorld.Instance.gameObjects_Playing[i].Update(gameTime);
                    }

                    foreach (var go in GameWorld.Instance.tiles)
                    {
                        go.Update(gameTime);
                    }

                    foreach (var go in GameWorld.Instance.colorTiles)
                    {
                        foreach (var item in go.Value)
                        {
                            item.Update(gameTime);
                        }
                    }

                    foreach(var item in GameWorld.Instance.buttons_Playing)
                    {
                        item.Update();
                    }    
                    break;

            }
        }

        // Draw metoden Draw alt der skal ind i det nuværende stadie
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {

                case GameState.Start:

                    foreach (var go in GameWorld.Instance.gameObjects_MainMenu)
                    {
                        go.Draw(spriteBatch);
                    }


                    foreach (var item in GameWorld.Instance.buttons_MainMenu)
                    {
                        item.Draw(spriteBatch);
                    }
                    break;



                case GameState.ChooseColor:

                    foreach (var go in GameWorld.Instance.gameObjects_ChooseColor)
                    {
                        go.Draw(spriteBatch);
                    }

                    foreach (var item in GameWorld.Instance.buttons_ColorSelection)
                    {
                        item.Draw(spriteBatch);
                    }

                    break;
                // Alt draw kode skal ind her
                case GameState.Playing:
                    spriteBatch.Draw(GameWorld.Instance.box, new Vector2(20, 130), null, Color.Red, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);


                    spriteBatch.Draw(GameWorld.Instance.box, new Vector2(20, 655), null, Color.Blue, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);


                    spriteBatch.Draw(GameWorld.Instance.box, new Vector2(665, 130), null, Color.Green, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);

                    spriteBatch.Draw(GameWorld.Instance.box, new Vector2(665, 655), null, Color.Yellow, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);



                    foreach (GameObject go in GameWorld.Instance.gameObjects_Playing)
                    {
                        go.Draw(spriteBatch);
                    }

                    foreach (var go in GameWorld.Instance.tiles)
                    {
                        go.Draw(spriteBatch);
                    }

                    foreach (var go in GameWorld.Instance.colorTiles)
                    {
                        foreach (var item in go.Value)
                        {
                            item.Draw(spriteBatch);
                        }
                    }

                    foreach (var item in GameWorld.Instance.buttons_Playing)
                    {
                        item.Draw(spriteBatch);
                    }

                    break;

            }
        }

       
    }

}
