using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project
{
    public static  class Images
    {
        public readonly static ImageSource Background = LoadImage("background.png");
        public readonly static ImageSource Brick = LoadImage("brick.png");
        public readonly static ImageSource FlowerStem = LoadImage("flowerstem.png");
        public readonly static ImageSource FlowerTopOld = LoadImage("flowertopold.png");
        public readonly static ImageSource FlowerTopYoung = LoadImage("flowertopyoung.png");
        public readonly static ImageSource Ground = LoadImage("ground.png");
        public readonly static ImageSource Character = LoadImage("character.png");
        public readonly static ImageSource LeafLeft = LoadImage("leafleft.png");
        public readonly static ImageSource LeafRight = LoadImage("leafright.png");
        public readonly static ImageSource Projectile = LoadImage("projectile.png");
        public readonly static ImageSource Worm = LoadImage("worm.png");
        public readonly static ImageSource Worm2 = LoadImage("worm2.png");



        private static ImageSource LoadImage(string filename)
        {
            return new BitmapImage(new Uri($"Assets/{filename}", UriKind.Relative));
        }
    }
}
