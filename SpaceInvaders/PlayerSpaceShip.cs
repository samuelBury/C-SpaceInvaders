using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class PlayerSpaceShip : SpaceShip
    {

        private Bitmap WidgetVie { get; set; }
        private int WidgetWidth;
        public int MaxLifes { get; set; }


        public PlayerSpaceShip(int Vies, double X1, double Y1, side unSide) : base(Vies, X1, Y1, unSide)
        {
            MaxLifes = Vies;
            WidgetVie = SpaceInvaders.Properties.Resources.VieRouge;
            WidgetWidth= WidgetVie.Width;
            
        }
        public override void Update(Game gameInstance, double deltaT)
        {

            if (gameInstance.keyPressed.Contains(Keys.Right) && Position.X < gameInstance.gameSize.Width - Image.Width)
            {
                goRight(deltaT);
            }
            if (gameInstance.keyPressed.Contains(Keys.Left) && gameInstance.PlayerShip.Position.X > 0)
            {
                goLeft(deltaT);
            }
            if (gameInstance.keyPressed.Contains(Keys.Space) && !gameInstance.gameObjects.Contains(missile))
            {
                Shoot(gameInstance);

                gameInstance.ReleaseKey(Keys.Space);
            }

            



        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {

            
            base.Draw(gameInstance, graphics);
            
            graphics.DrawImage(WidgetVie, (float)Position.X-8, (float)Position.Y+30,  30+10, 4);


            
            graphics.DrawImage(SpaceInvaders.Properties.Resources.VieVerte, (float)Position.X - 8, (float)Position.Y + 30, Lives / MaxLifes * 30+10, 4);

        }
        
    }
    
}
