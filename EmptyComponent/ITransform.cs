using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoEmpty.EmptyComponent
{
    public interface ITransform
    {
        float angle { get; set; }
        Vector2 LocalPosition { get; set; }
        Vector2 Offset { get; set; }
        Vector2 originAnchor { get; }
        ITransform parent { get; }
        Vector2 Position { get; set; }
        Rectangle Rect { get; set; }

        List<ITransform> Child { get; set; }
        void SetParent(ITransform transform);
        void SetPosition();
    }
}