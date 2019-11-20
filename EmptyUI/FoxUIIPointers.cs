using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Fox.UI
{

    public interface IPointerClick : IGraphicUI
    {
        void OnClick();
    }
    public interface IPointerDrag : IGraphicUI
    {
        bool IsDrag { get; set; }
        void OnDrag(PointData data);
    }

    public struct PointData
    {
        public MouseState PrevState;
        public MouseState CurState;
        public Point Position => CurState.Position;

        public bool LeftButtonClick => CurState.LeftButton.Equals(ButtonState.Pressed) && PrevState.LeftButton.Equals(ButtonState.Released);

        public PointData(MouseState prev, MouseState cur)
        {
            PrevState = prev;
            CurState = cur;
        }

    }

}
