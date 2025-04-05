using Frontend.ControllerPattern;
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
    public class Dice:Component
    {
        List<string> numberSpriteNames;
        List<Texture2D> numberSprites;
        Vector2 scale;
        Vector2 position;
        private Texture2D spriteForSizeRef;
        private Rectangle rectangle;
        private Vector2 origin;
        private Animator animator;
        private Random random;
        private bool down;
        private bool animationStarted;
        private float timeElapsed;
        private float rollTime;

        public Dice(List<string> numberSpriteNames,float rollTime)
        {
            this.numberSpriteNames = numberSpriteNames;

            this.rollTime = rollTime;

            numberSprites = new List<Texture2D>();

        }

        public override void Start()
        {
            position = GameObject.Transform.Position;
            SpriteRenderer sr = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();
            scale = new Vector2(sr.Scale, sr.Scale);
            foreach (var name in numberSpriteNames )
            {
                numberSprites.Add(GameWorld.Instance.Content.Load<Texture2D>(name));
            }
            
            spriteForSizeRef = numberSprites[0];
            rectangle = new Rectangle((int)(position.X - spriteForSizeRef.Width / 2), (int)(position.Y - spriteForSizeRef.Height / 2), (int)(spriteForSizeRef.Width * scale.X), (int)(spriteForSizeRef.Height * scale.Y));
            origin = new Vector2(spriteForSizeRef.Width * scale.X / 2, spriteForSizeRef.Height * scale.Y / 2);
            animator = (Animator)GameObject.GetComponent<Animator>();
            animator.animationPaused = true;

            animator.AddAnimation(new Animation("RollDice", numberSprites.ToArray(), 10));
            random = new Random();

          
            
            base.Start();
        }

       
        public override void Update()
        {
            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                down = true;

            }
            if (rectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Released && down)
            {
                animator.PlayAnimation("RollDice");
                animationStarted = true;
                // SEND RAND0M NUMBER TO BACKEND
                //Debug.Write("down");
                
                down = false;
            }

            if(animationStarted==true)
            {
                timeElapsed += GameWorld.Instance.deltaTime;

                if(timeElapsed>=rollTime)
                {
                    int randNumber = random.Next(0, numberSpriteNames.Count);

                    animator.StopAnimationAndSetSprite(numberSprites[randNumber]);


                    timeElapsed = 0;
                    animationStarted = false;
                }


            }
        }

       
    }
}
