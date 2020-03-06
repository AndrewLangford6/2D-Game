using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D_Game
{
    public partial class MainScreen : UserControl
    {
        List<Block> blocksList = new List<Block>();

        Boolean leftArrowDown, rightArrowDown, upArrowDown;

        int hSize = 30;

        SolidBrush blue = new SolidBrush(Color.Blue);

        

        public static bool jumping = false;
        public static bool jumping2 = false;
        public static int gravity, jumpSpeed, counter;
        public static bool left, right, air;

        Hero hero;

        public MainScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            hero = new Hero(Width / 2 - hSize / 2, 400, hSize);

             Block b1 = new Block(400, 300, 20, 200);
            Block b2 = new Block(0, 415, 20, 800);
            Block b3 = new Block(200, 280, 20, 100);
           blocksList.Add(b1);
            blocksList.Add(b2);
            blocksList.Add(b3);

            gravity = -30;
            jumpSpeed = 0;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                   // upArrowDown = false;
                    break;
            }
        }
        public void gameLoop_Tick(object sender, EventArgs e)
        {

            hero.Fall();
            counter++;
            //hero.x  = this.Width / 2 - hero.size / 2;
            

            if (upArrowDown)
            {
                hero.Jump();
                air = true;

            }
            if (!upArrowDown)
            {

            }

            if (counter == 9)
            {


                

                

               

                counter = 0;
            }

            

                //collision

                foreach (Block bonk in blocksList)
            {
                Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);
                Rectangle blockRec = new Rectangle(bonk.x, bonk.y, bonk.w, bonk.h);

              

                if (leftArrowDown)
                {
                    //hero.Move("left");
                    bonk.Move("right");
                    left = true;
                    right = false;
                }

                if (rightArrowDown)
                {
                    //hero.Move("right");
                    bonk.Move("left");
                    right = true;
                    left = false;
                }

                if (upArrowDown)
                {
                   // bonk.y++;
                }


                
                if (heroRec.IntersectsWith(blockRec) && 
                    hero.x + hero.size<= bonk.x && 
                    hero.x  >= bonk.x + bonk.w)
                {
                    
                }
                else if (heroRec.IntersectsWith(blockRec) &&
                    hero.x + hero.size >= bonk.x &&
                    hero.x <= bonk.x + bonk.w && hero.y < bonk.y)
                {
                    if (!air)
                    {
                        hero.y = bonk.y - hero.size + 1;
                        this.BackColor = Color.Purple;
                        upArrowDown = false;
                        air = false;
                        gravity = -30;
                        jumpSpeed = 0;
                        hero.x = hero.x;
                    }
                    else if (air)
                    {
                        hero.y = bonk.y + bonk.h;
                        
                    }
                    
                }


                //else if (left && heroRec.IntersectsWith(blockRec))
                // {
                //     hero.x = bonk.x + bonk.w;
                // }
                // else if (right && heroRec.IntersectsWith(blockRec))
                // {
                //     hero.x = bonk.x - hero.size;
                // }
                else
                {
                    this.BackColor = Color.Green;
                }

                
                



            }

            Refresh();
        }
        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(hero.heroBrush, hero.x, hero.y, hero.size, hero.size);

            foreach (Block b in blocksList)
            {
                e.Graphics.FillRectangle(blue, b.x, b.y, b.w, b.h);
            }

        }
    }
}
