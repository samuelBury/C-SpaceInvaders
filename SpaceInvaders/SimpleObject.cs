using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    abstract class SimpleObject : GameObject
    {
        
        public Vecteur Position { get; set; }

       
        public int Lives { get; set; }
        public Bitmap Image { get; set; }


        public SimpleObject(side unSide) : base(unSide)
        {

        }
        public override bool IsAlive()
        {
            if (Lives > 0)
            {
                return true;
            }
            return false;

        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {

            graphics.DrawImage(Image, (float)Position.X, (float)Position.Y, Image.Size.Width, Image.Size.Height);
           


        }
        public bool HitBoxContact(int X, int Y)
        {
            if (X >= 0 && X < Image.Width && Y >= 0 && Y < Image.Height)
            {
                return true;
            }
            return false;



        }
        public override void Collision(Missile m)
        {
            if (Side != m.Side)
            {
                for (int i = 0; i < m.Image.Width; i++)
                {
                    for (int j = 0; j < m.Image.Height; j++)
                    {
                        int positionX = (int)m.Position.X + i - (int)Position.X;
                        int positionY = (int)m.Position.Y + j - (int)Position.Y;
                        if (HitBoxContact(positionX, positionY))
                        {

                            Color pixel = Image.GetPixel(positionX, positionY);


                            if (pixel.A == 255)
                            {



                                OnCollision(m, 1);
                            }
                        }
                    }

                }
            }
        }

        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);



    }
}
