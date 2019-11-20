using Microsoft.Xna.Framework.Graphics;

namespace MonoEmpty.Component
{
    public interface IRenderer: IDrawComponet
    {
        Texture2D texture { get; set; }
    }
}

