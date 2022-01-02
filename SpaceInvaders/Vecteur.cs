using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur
    {

        
        public double X { get; set; }
        public double Y { get; set; }

        public Vecteur(double aX, double aY)
        {
            X = aX;
            Y = aY;
           
        }
        public Vecteur()
        {
            X = 0;
            Y = 0;
        }

        public double Norme()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public Vecteur Addition(Vecteur vect2)
        {
            return new Vecteur(X + vect2.Y, Y + vect2.Y);
        }
        public Vecteur Soustraction(Vecteur vect2)
        {
            return new Vecteur(X - vect2.X, Y - vect2.Y);
        }
        public Vecteur Inverse()
        {
            return new Vecteur(-X, -Y);
        }
        public Vecteur multiplicationReel(double a)
        {
            return new Vecteur(X * a, Y * a);
        }


       

    }
}
