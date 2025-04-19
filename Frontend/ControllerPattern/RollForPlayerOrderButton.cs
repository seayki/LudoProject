using Frontend.BuilderPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ControllerPattern
{
    internal class RollForPlayerOrderButton : IButtonAction
    {
        public Button ButtonObject { get; set; }

        public void DoAction()
        {
            // request backend to roll for playerorder
            GameWorld.Instance.StartGame();



            List<NonInteractableGOBuilder> playerColorBoxes=new List<NonInteractableGOBuilder>();



            /// temporary code until hocked up with backend
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    NonInteractableGOBuilder colorBox = new NonInteractableGOBuilder("Box", 0.2f, new Vector2(700+100*i, 400), Color.Blue);
                    playerColorBoxes.Add(colorBox);
                }

                if (i == 1)
                {
                    NonInteractableGOBuilder colorBox = new NonInteractableGOBuilder("Box", 0.2f, new Vector2(700+100*i, 400), Color.Yellow);
                    playerColorBoxes.Add(colorBox);
                }

                if (i == 2)
                {
                    NonInteractableGOBuilder colorBox = new NonInteractableGOBuilder("Box",0.2f, new Vector2(700+100*i, 400), Color.Red);
                    playerColorBoxes.Add(colorBox);
                }

                if (i == 3)
                {
                    NonInteractableGOBuilder colorBox = new NonInteractableGOBuilder("Box", 0.2f, new Vector2(700+100*i, 400), Color.Green);
                    playerColorBoxes.Add(colorBox);
                }
            }


            foreach (var item in playerColorBoxes)
            {
                GameWorld.Instance.Instantiate(item.BuildGameObject());
            }

            Button startGameButton = new Button("StartGame", new StartGameButton(), new Vector2(GameWorld.Instance.screenSize.X/2, 600),new Vector2(1,1));

            GameWorld.Instance.Instantiate(startGameButton);






        }
    }
}
