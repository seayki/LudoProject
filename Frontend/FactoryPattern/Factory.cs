using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.FactoryPattern
{
   
   public abstract class Factory
   {
      public abstract GameObject Create(Enum type,ContentManager content);

   }
   
}
