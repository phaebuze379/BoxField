using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{

    class Box
    {
        public Color colour;
        public int x, y, size;
       

        public Box(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public Box(int _x, int _y, int _size, Color _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            colour = _colour;
        }

        public void move(int speedY)
        {
            y += speedY;

        }


        public Boolean Collision(Box b)
        {
            Rectangle boxRec = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle heroRec = new Rectangle(x, y, size, size);

            return boxRec.IntersectsWith(heroRec);

        }
    }
}
