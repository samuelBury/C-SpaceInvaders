using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {

      
      
        public double Vitesse { get; set; }
      


        public Missile(double X, double Y, side unSide) : base(unSide)
        {

            Lives = 1;
            Vitesse = 600;
            Image = SpaceInvaders.Properties.Resources.shoot1;
            Position = new Vecteur(X, Y);
          

        }
        public override void Update(Game gameInstance, double deltaT)
        {

            
            if(Side== side.Ally)
            {
                Position.Y -= Vitesse * deltaT;
                if (Position.Y < 0)
                {
                    Lives = 0;


                }
            }
            if (Side == side.Enemy)
            {
                Position.Y += Vitesse * deltaT;
                if (Position.Y > gameInstance.gameSize.Height)
                {
                    Lives = 0;


                }
            }



            foreach (GameObject gameObject in gameInstance.gameObjects)
            {
                gameObject.Collision(this);

            }







        }
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
           
            if ( this != m)
            {
                 m.Lives = 0;
                Lives = 0;

            }


        }

    }
}
