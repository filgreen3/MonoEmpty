using FastMath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoEmpty.EmptyComponent
{
    public class Sprite : Component, IDrawComponet
    {
        public Transform2D GetTransform { get; protected set; }

        public override void Inicial()
        {
            base.Inicial();

            GetTransform = gameObject.GetComponent<Transform2D>();
            if (texture != null)
            {
                pic = new Color[texture.Width * texture.Height];
                texture.GetData(pic);
                saveTexture = new RenderTarget2D(texture.GraphicsDevice, texture.Width, texture.Height);

                GetTransform.SetPosition(new Vector2(texture.Width, texture.Height));

                saveTexture.SetData(pic);
            }
        }


        public Sprite(GameObject gameObject) : base(gameObject)
        {



        }


        public static GraphicsDevice device { get; set; }

        public Color[] textureData { get; set; }
        public Texture2D texture { get; set; }


        RenderTarget2D saveTexture;

        private float prevAngle;

        float scale;

        Color[] pic;

        private static MemoizedCos cos = MemoizedCos.ConstructByMaxError(0.01f);
        private static MemoizedSin sin = MemoizedSin.ConstructByMaxError(0.01f);


        Texture2D GetTexture(float angle)
        {
            if (prevAngle == angle) return saveTexture;

            prevAngle = angle;

            angle %= MathHelper.TwoPi;
            angle = ((int)(Math.Abs(angle) * 16)) / 16f;

            int PIC_WIDTH = texture.Width;
            int PIC_HEIGHT = texture.Height;

            scale = ((int)(sin.Calculate(angle % MathHelper.Pi) * .5f + 1f * 16)) / 16f;

            int PIC_WIDTH_NEW = (int)(PIC_WIDTH * scale) + 10;
            int PIC_HEIGHT_NEW = (int)(PIC_HEIGHT * scale) + 10;

            var cosangel = cos.Calculate(angle);
            var sinangel = sin.Calculate(angle);


            //int PIC_WIDTH_NEW = (int)MathHelper.LerpPrecise(PIC_HEIGHT,PIC_WIDTH, cosangel * cosangel);
            //int PIC_HEIGHT_NEW = (int)MathHelper.LerpPrecise(PIC_HEIGHT, PIC_WIDTH, sinangel * sinangel);




            int DIV = (PIC_WIDTH_NEW - PIC_WIDTH) / 2;

            Color[] bufPic = new Color[(PIC_WIDTH_NEW * PIC_HEIGHT_NEW)];

            //texture.GetData(pic);


            float midX, midY;
            float deltaX, deltaY;
            int rotX, rotY;
            int i, j;

            midX = PIC_WIDTH / 2.0f;
            midY = PIC_HEIGHT / 2.0f;

            int id;
            for (i = 0; i < PIC_WIDTH; i++)
                for (j = 0; j < PIC_HEIGHT; j++)
                {
                    deltaX = i - midX;
                    deltaY = j - midY;

                    rotX = (int)(midX + deltaX * sin.Calculate(angle) + deltaY * cos.Calculate(angle));
                    rotY = (int)(midY + deltaX * cos.Calculate(angle) - deltaY * sin.Calculate(angle));


                    id = (rotX + DIV) * PIC_WIDTH_NEW + rotY + DIV;

                    if (id > 0 && id < PIC_HEIGHT_NEW * PIC_WIDTH_NEW)
                    {
                        bufPic[id] = pic[j * PIC_WIDTH + i];
                        if (bufPic[id - 1].A == 0 && angle % (Math.PI / 4) != 0)
                            bufPic[id - 1] = bufPic[id];
                    }
                }



            RenderTarget2D tex = saveTexture;
            if (this.saveTexture.Width != PIC_WIDTH_NEW || this.saveTexture.Height != PIC_HEIGHT_NEW)
                tex = new RenderTarget2D(device, PIC_WIDTH_NEW, PIC_HEIGHT_NEW);


            tex.SetData(bufPic);

            saveTexture = tex;

            return saveTexture;
        }





        public void Draw(SpriteBatch spriteBatch)
        {
            if (saveTexture == null) return;
            GetTransform.SetPosition(saveTexture.Bounds.Size.ToVector2());
            spriteBatch.Draw(GetTexture(GetTransform.angle), GetTransform.Rect, Color.White);
        }
    }






}