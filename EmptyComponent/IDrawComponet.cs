using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
namespace MonoEmpty.EmptyComponent
{
    public interface IDrawComponet
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
