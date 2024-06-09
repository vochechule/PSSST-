using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Worm
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public Worm(int row, int col, int xVelocity, int yVelocity)
        {
            Row = row;
            Col = col;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }

        public void Move()
        {
            Col += XVelocity;
        }
    }
}
