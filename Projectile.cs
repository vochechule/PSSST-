using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public enum DirectionForProjectile { Up, Down, Left, Right }

    public class Projectile
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public Projectile(int row, int col, int xVelocity, int yVelocity)
        {
            Row = row;
            Col = col;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }

        public void Move()
        {
            Row += YVelocity;
            Col += XVelocity;
        }
    }
}