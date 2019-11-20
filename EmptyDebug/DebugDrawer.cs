using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEmpty.Component.Debug
{
    static class  DebugDrawer
    {
        public static  Color RectWire(int i,Point size)
        {
            if (i % (size.X) == 0 ||
                i % (size.X) == (size.X) - 1 ||
                i / size.X == 0 ||
                i / size.X == size.Y - 1)
                return Color.Red;
            return Color.Transparent;
        }
    }
}
