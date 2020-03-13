using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game
{
    class Block
    {
        public int x, y, h, w;


        //create a constructor
        public Block(int _x, int _y, int _h, int _w)
        {
            x = _x;
            y = _y;
            h = _h;
            w = _w;


        }

        //allow the blocks to move to create the illusion of the character moving
        public void Move(string direction)
        {
            if (direction == "left")
            {
                x = x - 10;
            }

            if (direction == "right")
            {
                x = x + 10;
            }

        }

        //the blocks fall faster over time to create a challange
        public void Drift()
        {
            y = y + MainScreen.groovin;
        }
            
    }
}
