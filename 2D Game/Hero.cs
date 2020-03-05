using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_Game
{
    class Hero
    {



        public SolidBrush heroBrush;
        public int x, y, size;
        Random randGen = new Random();

        public Hero(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;

            int randValue = randGen.Next(1, 3);

            if (randValue == 1)
            {
                heroBrush = new SolidBrush(Color.Red);
            }
            else if (randValue == 2)
            {
                heroBrush = new SolidBrush(Color.Orange);
            }
        }
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

        public void Jump()
        {

            MainScreen.air = true;
                MainScreen.gravity++;

                if (MainScreen.gravity > 0)
                {
                     MainScreen.jumpSpeed++;

                }

                y = y + MainScreen.gravity;
        }


        public void Fall()
        {
            y = y + MainScreen.jumpSpeed;
            MainScreen.jumpSpeed++;
        }

        

        
    }
}
