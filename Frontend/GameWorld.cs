using Frontend.BuilderPattern;
using Frontend.ComponentPattern;
using Frontend.ControllerPattern;
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
        public Texture2D box;
        public Vector2 screenSize;

        public List<GameObject> gameObjects_Playing;
        public List<GameObject> gameObjects_SetupGame;
        public List<GameObject> gameObjects_MainMenu;
        public List<Button> buttons_MainMenu;
        public List<Button> buttons_SetupGame;
        public List<Button> buttons_Playing;

        private List<Button> newButtons;
        private List<Button> destroyedButtons;

        public List<GameObject> tiles = new List<GameObject>();
        public GameStateManager stateManager;


        private static GameWorld instance;
        private int currentTilesCount;

        private int tileSpacing = 70;

        private List<TileColor> playerColors;

        public Dictionary<TileColor, List<GameObject>> colorTiles;
        private List<GameObject> newGameObjects;
        private List<GameObject> destroyedGameObjects;

        public Texture2D pixel;
        public float deltaTime;

        public float FPS;

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

            gameObjects_MainMenu = new List<GameObject>();

            gameObjects_SetupGame = new List<GameObject>();

            gameObjects_Playing = new List<GameObject>();

            newGameObjects = new List<GameObject>();
            destroyedGameObjects = new List<GameObject>();

            colorTiles = new Dictionary<TileColor, List<GameObject>>();

            playerColors = new List<TileColor>() { TileColor.Green, TileColor.Yellow, TileColor.Blue, TileColor.Red };

            buttons_MainMenu = new List<Button>();
            buttons_Playing = new List<Button>();
            buttons_SetupGame = new List<Button>();

            newButtons = new List<Button>();
            destroyedButtons = new List<Button>();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            FPS = 60;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPS);
            buttons_MainMenu.Add(new Button("NewGame", new SetupGameButton(), new Vector2(screenSize.X / 2, screenSize.Y / 2), new Vector2(1, 1)));

            CreateSetupButtons();

            foreach (GameObject go in gameObjects_Playing)
            {
                go.Awake();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // board=Content.Load<Texture2D>("LudoBoard");
            box = Content.Load<Texture2D>("Box");
            pixel = Content.Load<Texture2D>("pixel");
            MakeClassicTileMap();

            CreateDice();
            stateManager.Loadcontent();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            stateManager.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {


            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);



            stateManager.Draw(_spriteBatch);

            // FOR SHOWCASE USE ONLY

            // _spriteBatch.Draw(box, tiles[46].Transform.Position, null, Color.Green, 0, new Vector2(box.Width / 2, box.Height / 2), new Vector2(0.1f, 0.1f), SpriteEffects.None, 0);


            //_spriteBatch.Draw(box, new Vector2(500, 500), null, Color.Red, 0, new Vector2(0, 0), new Vector2(0.2f, 0.2f), SpriteEffects.None, 0);




            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private void MakeClassicTileMap()
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(50 + tileSpacing + i * tileSpacing, 450);

                tiles.Add(go);
            }

            Vector2 pos1 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos1.X + tileSpacing, pos1.Y - tileSpacing * i);

                tiles.Add(go);

            }

            Vector2 pos2 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos2.X + tileSpacing * i, pos2.Y - tileSpacing);

                tiles.Add(go);
            }


            Vector2 pos3 = tiles[tiles.Count - 1].Transform.Position;


            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos3.X + tileSpacing, pos3.Y + tileSpacing * i);

                tiles.Add(go);

            }

            Vector2 pos4 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos4.X + i * tileSpacing, pos4.Y + tileSpacing);

                tiles.Add(go);
            }


            Vector2 pos5 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos5.X + tileSpacing, pos5.Y + tileSpacing * i);

                tiles.Add(go);

            }

            Vector2 pos6 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos6.X - i * tileSpacing, pos6.Y + tileSpacing);

                tiles.Add(go);
            }

            Vector2 pos7 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos7.X - tileSpacing, pos7.Y + i * tileSpacing);

                tiles.Add(go);
            }

            Vector2 pos8 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 2; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos8.X - tileSpacing * i, pos8.Y + tileSpacing);

                tiles.Add(go);

            }

            Vector2 pos9 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos9.X - tileSpacing, pos9.Y - i * tileSpacing);

                tiles.Add(go);
            }

            Vector2 pos10 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 5; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos10.X - i * tileSpacing, pos10.Y - tileSpacing);

                tiles.Add(go);
            }

            Vector2 pos11 = tiles[tiles.Count - 1].Transform.Position;

            for (int i = 0; i < 3; i++)
            {
                GameObject go = TileFactory.Instance.Create(TileColor.None, Content);
                go.Transform.Position = new Vector2(pos11.X - tileSpacing, pos11.Y - tileSpacing * i);

                tiles.Add(go);

            }

            SpriteRenderer startTile1 = (SpriteRenderer)tiles[0].GetComponent<SpriteRenderer>();

            startTile1.Color = Color.Red;


            SpriteRenderer startTile2 = (SpriteRenderer)tiles[12].GetComponent<SpriteRenderer>();

            startTile2.Color = Color.Green;

            SpriteRenderer startTile3 = (SpriteRenderer)tiles[24].GetComponent<SpriteRenderer>();

            startTile3.Color = Color.Yellow;


            SpriteRenderer startTile4 = (SpriteRenderer)tiles[36].GetComponent<SpriteRenderer>();

            startTile4.Color = Color.Blue;


            CreateColorTiles(playerColors);


        }



        private void CreateColorTiles(List<TileColor> tileColors)
        {
            for (int i = 0; i < tileColors.Count; i++)
            {


                List<GameObject> colorTileGameObjects = new List<GameObject>();
                Vector2 startColorPos = tiles[10 + i * 12].Transform.Position;
                for (int j = 0; j < 5; j++)
                {
                    GameObject go = TileFactory.Instance.Create(tileColors[i], Content);

                    switch (i)
                    {
                        case 0:
                            go.Transform.Position = new Vector2(startColorPos.X, startColorPos.Y + tileSpacing + tileSpacing * j);
                            break;

                        case 1:
                            go.Transform.Position = new Vector2(startColorPos.X - tileSpacing - tileSpacing * j, startColorPos.Y);
                            break;
                        case 2:
                            go.Transform.Position = new Vector2(startColorPos.X, startColorPos.Y - tileSpacing - tileSpacing * j);
                            break;
                        case 3:
                            go.Transform.Position = new Vector2(startColorPos.X + tileSpacing + tileSpacing * j, startColorPos.Y);
                            break;



                        default:
                            break;
                    }




                    colorTileGameObjects.Add(go);

                }

                colorTiles.Add(tileColors[i], colorTileGameObjects);

            }



        }



        private void CreateSetupButtons()
        {
            NonInteractableGOBuilder SelectAmountOfPlayersSign = new NonInteractableGOBuilder("SelectAmountOfPlayers", 1, new Vector2(screenSize.X / 2, 100), Color.White);

            List<Button> playerAmountButtons = new List<Button>();

            for (int i = 0; i < 3; i++)
            {
                int playerAmt = 2 + i;
                Button playerAmountBtn = new Button((playerAmt).ToString(), new SetPlayerAmountButton(playerAmt), new Vector2(750 + 100 * i, 200), new Vector2(1, 1));


                playerAmountButtons.Add(playerAmountBtn);
                buttons_SetupGame.Add(playerAmountBtn);
            }

            foreach (var btn in playerAmountButtons)
            {
                btn.linkedButtons = playerAmountButtons;
            }
            // default button selected is 2
            playerAmountButtons[0].buttonAction.DoAction();

            Button rollForPlayerOrderBtn = new Button("RollForPlayerOrder", new RollForPlayerOrderButton(), new Vector2(screenSize.X / 2, 300), new Vector2(1, 1));

            buttons_SetupGame.Add(rollForPlayerOrderBtn);

            gameObjects_SetupGame.Add(SelectAmountOfPlayersSign.BuildGameObject());

        }


        private void CreateDice()
        {
            GameObject diceGO = new GameObject();
            SpriteRenderer sr=(SpriteRenderer)diceGO.AddComponent(new SpriteRenderer());
            sr.SetSprite("Dice6",0,1,Color.White,Content);
            diceGO.AddComponent(new Animator());
            diceGO.AddComponent(new Dice(new List<string>() { "Dice1", "Dice2", "Dice3", "Dice4", "Dice5", "Dice6" },1));

            diceGO.Transform.Position = new Vector2(1200, 600);

            gameObjects_Playing.Add(diceGO);
            


        }

        public void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }

        public void Instantiate(Button button)
        {
            newButtons.Add(button);
        }

        public void Destroy(GameObject go)
        {
            destroyedGameObjects.Add(go);
        }

        public void Destroy(Button button)
        {
            destroyedButtons.Add(button);
        }

        public void Cleanup()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                if(stateManager.currentState==GameState.SetupGame)
                {
                    gameObjects_SetupGame.Add(newGameObjects[i]);
                }

                if(stateManager.currentState==GameState.Playing)
                {
                    gameObjects_Playing.Add(newGameObjects[i]);
                }
                
                newGameObjects[i].Awake();
                newGameObjects[i].Start();

            }

            destroyedGameObjects.Clear();
            newGameObjects.Clear();

            for (int i = 0; i < newButtons.Count; i++)
            {
                if (stateManager.currentState == GameState.SetupGame)
                {
                    buttons_SetupGame.Add(newButtons[i]);
                }

                if (stateManager.currentState == GameState.Playing)
                {
                    buttons_Playing.Add(newButtons[i]);
                }

                newButtons[i].LoadContent();
            }

            destroyedButtons.Clear();
            newButtons.Clear();
           
        }

    }
}
