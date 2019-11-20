using FastMath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoEmpty.Component
{
    public class Sprite :Component, IDrawComponet
    {
        public Transform2D GetTransform { get; set; }

        public override void Inicial()
        {
            ReqireComoponets = new Type[] { typeof(Transform2D) };
            base.Inicial();

          
            var mp = (float)Transform.SizePower;
            if (texture != null)
            {
                int SizeX = (int)(((int)(texture.Width / mp)) * mp) * (int)mp;
                int SizeY = (int)(((int)(texture.Height / mp)) * mp) * (int)mp;
                pic = new Color[texture.Width * texture.Width];
                texture.GetData(pic);
                saveTexture = new RenderTarget2D(texture.GraphicsDevice,texture.Width,texture.Height);
            }


            GetTransform = gameObject.GetComponent<Transform2D>();

         

        }


        public Sprite(GameObject gameObject):base(gameObject)
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
            angle = ((int)(Math.Abs(angle) * 32)) / 32f;

            int PIC_WIDTH = texture.Width;
            int PIC_HEIGHT = texture.Height;

            scale = sin.Calculate(angle % MathHelper.Pi) * .5f + 1f;

            int PIC_WIDTH_NEW = (int)(PIC_WIDTH * scale) + 10;
            int PIC_HEIGHT_NEW = (int)(PIC_HEIGHT * scale) + 10;
            int DIV = (PIC_WIDTH_NEW - PIC_WIDTH) / 2;
            DIV = (int)(DIV / 1f) * 1;

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

            //bufPic[0] = Color.Red;
            //bufPic[PIC_WIDTH_NEW - 1] = Color.Red;
            //bufPic[bufPic.Length - PIC_WIDTH_NEW] = Color.Red;
            //bufPic[bufPic.Length - 1] = Color.Red;


            RenderTarget2D tex = saveTexture;
            if (this.saveTexture.Width != PIC_WIDTH_NEW)
                tex = new RenderTarget2D(device, PIC_WIDTH_NEW, PIC_HEIGHT_NEW);


            tex.SetData(bufPic);

            saveTexture = tex;

            return saveTexture;
        }





        public void Draw(SpriteBatch spriteBatch)
        {
            if (saveTexture == null) return;
            GetTransform.SetPosition(Vector2.One * saveTexture.Width);
            spriteBatch.Draw(GetTexture(GetTransform.angle), GetTransform.Rect, Color.White);
        }
    }






}