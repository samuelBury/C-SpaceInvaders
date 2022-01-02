using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {
        private double randomShootProbability;


        private bool goingRight;
        private bool goingLeft;
        private int SpeedPixelPerSecond;
        private HashSet<SpaceShip> enemyShips = new HashSet<SpaceShip>();
        private int baseWidth;
        public Size Size { get; set; }
        public Vecteur Position { get; set; }

        int getRight()
        {
            int max = -1; 
           foreach( SpaceShip ship in enemyShips)
            {
                if (ship.Position.X+ship.Image.Width> max)
                {
                    max = (int)ship.Position.X + ship.Image.Width;
                }
            }
            return max;
        }
        int getLeft()
        {
            int min = 1000000;
            foreach (SpaceShip ship in enemyShips)
            {
                if (ship.Position.X < min)
                {
                    min = (int)ship.Position.X;
                }
            }
            return min;
        }

        int getBottom()
        {
            int max = -1;
            foreach (SpaceShip ship in enemyShips)
            {
                if (ship.Position.Y+ship.Image.Height > max)
                {
                    max = (int)ship.Position.Y+ ship.Image.Height;
                }
            }
            return max;
        }
        int getTop()
        {
            int min = 100000;
            foreach (SpaceShip ship in enemyShips)
            {
                if (ship.Position.Y < min)
                {
                    min = (int)ship.Position.Y;
                }
            }
            return min;
        }

        public EnemyBlock(int larg, Vecteur Pos,side unSide) : base(unSide)
        {
            randomShootProbability = 2;
            goingRight = true;
            goingLeft = false;
            SpeedPixelPerSecond = 100;
            baseWidth = larg;
            Position = Pos;
        }
        public void AddLine(int nbShips, int nbLives, Bitmap shipImage, Game gameinstance)
        {
            for (int i =0; i<nbShips; i++)
            {
                SpaceShip newShip = new SpaceShip(nbLives,Position.X+ baseWidth / nbShips * i, Position.Y + Size.Height, side.Enemy);
                newShip.Image = shipImage;
                gameinstance.AddNewGameObject(newShip);
                enemyShips.Add(newShip);
                

            }
            UpdateSize();
          

        }
        public void UpdateSize()
        {
            Position.X = getLeft();
            Position.Y = getTop();
            Size newSize = new Size(getRight() - getLeft(),30+ getBottom() - getTop());
            Size = newSize;
            


        }

        public override void Collision(Missile m)
        {

            foreach (SpaceShip enemy in enemyShips)
            {
                enemy.Collision(m);
            }

        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
           foreach( SpaceShip enemy in enemyShips)
            {
               
                
                    enemy.Draw(gameInstance, graphics);
                
                
            }
            
           
        }

        public override bool IsAlive()
        {
            
                if (enemyShips.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public void goRight(double deltaT)
        {
            
            foreach (SpaceShip ship in enemyShips)
            {
                ship.Position.X += SpeedPixelPerSecond * deltaT+0.1;
            }
            Position.X += SpeedPixelPerSecond * deltaT+ 0.1;
            
        }
        public void goLeft(double deltaT)
        {

            foreach (SpaceShip ship in enemyShips)
            {
                ship.Position.X -= SpeedPixelPerSecond * deltaT +0.1;
            }
            
            Position.X -= SpeedPixelPerSecond * deltaT+0.1;
        }
        public void goDown(double deltaT)
        {
            foreach (SpaceShip ship in enemyShips)
            {
                ship.Position.Y += SpeedPixelPerSecond * deltaT*10;
            }

            Position.Y += SpeedPixelPerSecond * deltaT*10;
        }
        public void changeDirection(double deltaT)
        {
            if (goingRight)
            {
                goingRight = false;
                goingLeft = true;
            }
            else if (goingLeft)
            {
                goingLeft = false;
                goingRight = true;
            }
            goDown(deltaT);
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            double r;
            Random randomisateur = new Random();
            foreach (SpaceShip ship in enemyShips)
            {
               
                r = randomisateur.NextDouble();
                if (r <= randomShootProbability * deltaT)
                {
                    ship.Shoot(gameInstance);
                }
            }
                
            

            enemyShips.RemoveWhere(gameObject => !gameObject.IsAlive());
                UpdateSize();
            
               
            if (goingRight)
            {
                goRight(deltaT);
            }
            if (goingLeft)
            {
                goLeft(deltaT);
            }
           
            if (getLeft()<=0|| getRight() >= gameInstance.gameSize.Width)
            {
                changeDirection(deltaT);
            }







        }

       
    }
}
