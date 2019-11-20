using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace MonoEmpty.UI
{

    public static class FoxUITextureGen
    {
        public static int d;
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            d++;
            Texture2D texture = new Texture2D(device, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = paint(pixel);
            }

            texture.SetData(data);

            return texture;
        }
        public static Color SubColor(int id, int width, int hight)
        {
            Color col = (id % width % 17 == 0 || id / width % 17 == 0) ? Color.White : Color.Transparent;
            if ((id % width % 17 == 0 && id / width % 17 == 0)) col = Color.IndianRed;
            return col;
        }
    }
}
