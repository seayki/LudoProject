using Frontend.FactoryPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Frontend
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private Texture2D board;
        private Texture2D box;
        public Vector2 screenSize;

        public List<GameObject> gameObjects;
        public Dictionary<int, GameObject> tiles = new Dictionary<int, GameObject>();
        private GameStateManager stateManager;


        private static GameWorld instance;
        private int currentTilesCount;

        private int tileSpacing = 70;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();

                }
                return instance;
            }

        }

        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1800;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            stateManager = new GameStateManager();

            gameObjects = new List<GameObject>();

           
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


          

            foreach (GameObject go in gameObjects)
            {
                go.Awake();
            }

          

          


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // board=Content.Load<Texture2D>("LudoBoard");
            box= Content.Load<Texture2D>("Box");

            MakeClassicTileMap();

            Debug.WriteLine(tiles.Count);
            stateManager.Loadcontent();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            stateManager.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
           

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(box, new Vector2(20, 130), null, Color.Red, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);


            _spriteBatch.Draw(box, new Vector2(20, 655), null, Color.Blue, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);


            _spriteBatch.Draw(box, new Vector2(665, 130), null, Color.Green, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);

            _spriteBatch.Draw(box, new Vector2(665, 655), null, Color.Yellow, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), SpriteEffects.None, 0);


            

            stateManager.Draw(_spriteBatch);

            // FOR SHOWCASE USE ONLY

            _spriteBatch.Draw(box, tiles[5].Transform.Position, null, Color.Green, 0, new Vector2(box.Width / 2, box.Height / 2), new Vector2(0.1f, 0.1f), SpriteEffects.None, 0);


            //_spriteBatch.Draw(box, new Vector2(500, 500), null, Color.Red, 0, new Vector2(0, 0), new Vector2(0.2f, 0.2f), SpriteEffects.None, 0);




            _spriteBatch.End();
            base.Draw(gameTime);
        }


        void MakeClassicTileMap()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(50 + i * tileSpacing, 450);

                tiles.Add(i, go);
            }
          
            Vector2 pos1 = tiles[tiles.Count-1].Transform.Position; 
           
            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos1.X+tileSpacing, pos1.Y - tileSpacing * i);

                tiles.Add(tiles.Count, go);

            }

            Vector2 pos2 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i <2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos2.X+tileSpacing*i, pos2.Y-tileSpacing);

                tiles.Add(tiles.Count, go);
            }


            Vector2 pos3 = tiles[tiles.Count - 1].Transform.Position;


            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos3.X+tileSpacing, pos3.Y + tileSpacing * i);

                tiles.Add(tiles.Count, go);

            }

            Vector2 pos4 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos4.X + i * tileSpacing, pos4.Y+tileSpacing);

                tiles.Add(tiles.Count, go);
            }


            Vector2 pos5 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos5.X + tileSpacing, pos5.Y + tileSpacing * i);

                tiles.Add(tiles.Count, go);

            }

            Vector2 pos6 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos6.X- i * tileSpacing, pos6.Y+tileSpacing);

                tiles.Add(tiles.Count, go);
            }

            Vector2 pos7 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos7.X-tileSpacing, pos7.Y + i* tileSpacing);

                tiles.Add(tiles.Count, go);
            }

            Vector2 pos8 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos8.X -tileSpacing*i, pos8.Y+tileSpacing);

                tiles.Add(tiles.Count, go);

            }

            Vector2 pos9 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos9.X-tileSpacing, pos9.Y - i * tileSpacing);

                tiles.Add(tiles.Count, go);
            }

            Vector2 pos10 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos10.X - i * tileSpacing, pos10.Y - tileSpacing);

                tiles.Add(tiles.Count, go);
            }

            Vector2 pos11 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos11.X- tileSpacing, pos11.Y - tileSpacing * i);

                tiles.Add(tiles.Count, go);

            }


         
        }
    }
}
