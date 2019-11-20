using Microsoft.Xna.Framework.Graphics;

namespace MonoEmpty.EmptyComponent
{
    public interface IRenderer: IDrawComponet
    {
        Texture2D texture { get; set; }
    }
}

