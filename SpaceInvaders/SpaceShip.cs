using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;



namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {

      
        private double speedPixelPerSecond;
        public double SpeedPixelPerSecond { get; set; }

        
       

        public Missile missile;

       


        public SpaceShip(int Vies, double X1, double Y1, side unSide ) : base(unSide) 
        {
            Image = SpaceInvaders.Properties.Resources.ship3;
            
            Lives = Vies;
            Position = new Vecteur();
            Position.X = X1;
            Position.Y = Y1;
            SpeedPixelPerSecond = 100;
            

        }

        public void Shoot(Game game)
        {
            
            
            
                missile = new Missile(Position.X + 4, Position.Y, Side );
                game.AddNewGameObject(missile);
            
         

            
        }

      
        public void goRight(double deltaT)
        {
            Position.X += SpeedPixelPerSecond * deltaT;
        }
        public void goLeft(double deltaT)
        {
            Position.X -= SpeedPixelPerSecond * deltaT;
        }

        public override void Update(Game gameInstance, double deltaT)
        {

            


        }
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            
            if(m != missile)
            {
                Console.WriteLine(Lives);
                if (m.Lives <= Lives)
                {
                    Lives -=m.Lives;
                    m.Lives = 0;
                   
                    
                }
                else
                {
                    m.Lives -= Lives;
                    Lives = 0;
                    
                }
            }
           
        }


    }
}
