using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ControllerPattern
{
    internal class SetPlayerAmountButton : IButtonAction
    {
        private int playerAmount;
        public SetPlayerAmountButton(int playerAmount)
        {
            this.playerAmount = playerAmount;

        }

        public Button ButtonObject { get; set; }

        public void DoAction()
        {
            // Send player amount to backend.
            ButtonObject.DeSelectLinkedButtons();
            ButtonObject.selected = true;
          
        }
    }
}
