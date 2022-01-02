using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
        public Bunker(double X1, double Y1, side unSide) : base(unSide)
        {
            Position = new Vecteur();
            Position.X = X1;
            Position.Y = Y1;
            Image = SpaceInvaders.Properties.Resources.bunker;
            Lives = 2;

        }
        public override void Update(Game gameInstance, double deltaT)
        {
           
        }
        public override void Collision(Missile m)
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


                            Image.SetPixel(positionX, positionY, Color.FromArgb(0, 255, 255, 255));
                            OnCollision(m, 1);
                        }
                    }
                }

            }
        }



        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            m.Lives--;
        }

    }
}
