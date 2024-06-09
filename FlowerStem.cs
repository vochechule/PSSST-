using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class FlowerStem
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Height { get; set; }
        public int HP { get; set; }

        public FlowerStem(int row, int col, int height, int hp)
        {
            Row = row;
            Col = col;
            Height = height;
            HP = hp;
        }

        public void Damage()
        {
            HP--;
        }
    }
}
