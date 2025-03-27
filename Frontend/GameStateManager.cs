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
            currentState = GameState.Playing;
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
                case GameState.Playing:

                 

                    foreach (GameObject go in GameWorld.Instance.gameObjects)
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
                    break;

            }
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {

                case GameState.Playing:
                    for (int i = 0; i < GameWorld.Instance.gameObjects.Count; i++)
                    {
                        GameWorld.Instance.gameObjects[i].Update(gameTime);
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
                    break;

            }
        }

        // Draw metoden Draw alt der skal ind i det nuværende stadie
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {

                // Alt draw kode skal ind her
                case GameState.Playing:


                    foreach (GameObject go in GameWorld.Instance.gameObjects)
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

                    break;

            }
        }

       
    }

}
