using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Frontend.BuilderPattern
{
    internal class NonInteractableGOBuilder : IBuilder
    {
        string spriteName;
        float scale;
        Vector2 pos;
        public NonInteractableGOBuilder(string spriteName,float scale,Vector2 position)
        {
            this.spriteName = spriteName;
            this.scale = scale;
            this.pos = position;

        }
        public GameObject BuildGameObject()
        {
            GameObject GO = new GameObject();

            SpriteRenderer sr=(SpriteRenderer)GO.AddComponent(new SpriteRenderer());
            sr.SetSprite(spriteName,0,scale,Color.White,GameWorld.Instance.Content);

            GO.Transform.Position = pos;
            return GO;
        }
    }
}
