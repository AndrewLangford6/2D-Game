using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;

namespace _2D_Game
{
    class Hero
    {
        //initialize
        public SolidBrush heroBrush;
        public int x, y, size;
        Random randGen = new Random();


        //create a constructor
        public Hero(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;

            int randValue = randGen.Next(1, 3);

            //generate a character that is Red or Orange
            if (randValue == 1)
            {
                heroBrush = new SolidBrush(Color.Red);
            }
            else if (randValue == 2)
            {
                heroBrush = new SolidBrush(Color.Orange);
            }
        }

        //making the character jump
        public void Jump()
        {
            
            MainScreen.gravity++;

                if (MainScreen.gravity > 0)
                {
                     MainScreen.jumpSpeed++;

            }

                y = y + MainScreen.gravity;
        }

        //making the character fall at a passive rate
        public void Fall()
        {
            y = y + MainScreen.jumpSpeed;
            MainScreen.jumpSpeed++;
        }

        

        
    }
}
