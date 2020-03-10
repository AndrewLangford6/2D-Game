using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace _2D_Game
{
    public partial class MainScreen : UserControl
    {
        List<Block> blocksList = new List<Block>();

        SoundPlayer player = new SoundPlayer(Properties.Resources.Jump);
        SoundPlayer player2 = new SoundPlayer(Properties.Resources.Fall);

        Random randGen = new Random();

        Boolean leftArrowDown, rightArrowDown, upArrowDown;

        int hSize = 30;

        SolidBrush blue = new SolidBrush(Color.Blue);


        int newBoxCounter = 0;
        int patternAmount = 10;
        int boxSize = 20;
        int boxLeftX = 100;
        int boxRightX = 500;
        int boxGap = 200;
        int boxXOffset = 5;

        int blockOff;

        public static bool jumping = false;
        public static bool jumping2 = false;
        public static int gravity, jumpSpeed, counter, counter2, groovin, score;
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

            Block b1 = new Block(100, 250, 20, 100);
            Block b2 = new Block(0, 400, 20, 800);
            Block b3 = new Block(300, 100, 20, 100);
            Block b4 = new Block(200, -50, 20, 100);
            
            blocksList.Add(b1);
            blocksList.Add(b2);
            blocksList.Add(b3);
            blocksList.Add(b4);


            gravity = -30;
            jumpSpeed = 0;

            groovin = 4;
            
        }
        public void end()
        {
            player2.Play();
            timer1.Enabled = false;
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
                    player.Play();
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
            
            scoreLabel.Text = Convert.ToString(score);

            if (hero.y + hero.size >= this.Height)
            {
                end();
            }


            hero.Fall();
            counter++;
            counter2++;
            score = score + groovin/4;


            if (upArrowDown)
            {
                
                hero.Jump();
                this.BackColor = Color.Green;
                air = true;

            }
            if (!upArrowDown)
            {

            }

            if (counter == 25)
            {
                newBoxCounter++;

                boxLeftX += boxXOffset;
                patternAmount = randGen.Next(0, this.Width - 99);
                blockOff = randGen.Next(1, 3);

                if(blockOff == 1)
                {
                    Block b1 = new Block(patternAmount, -30, 20, 100);
                    blocksList.Add(b1);
                    counter = 0;
                }
                else
                {
                    Block b1 = new Block(patternAmount, -30, 20, 100);
                    blocksList.Add(b1);
                    counter = 0;
                }
                

               
            }

            if (counter2 == 200)
            {
                groovin++;
                counter2 = 0;
            }



            //collision

            foreach (Block bonk in blocksList)
            {
                Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);
                Rectangle blockRec = new Rectangle(bonk.x, bonk.y, bonk.w, bonk.h);

                bonk.Drift();

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


                if (heroRec.IntersectsWith(blockRec))
                {
                    if (hero.x + hero.size <= bonk.x &&
                    hero.x >= bonk.x + bonk.w)
                    {
                        this.BackColor = Color.Green;
                    }
                    else if (hero.x + hero.size >= bonk.x &&
                        hero.x <= bonk.x + bonk.w && hero.y < bonk.y)
                    {
                        //if (!air)
                        //{
                        hero.y = bonk.y - hero.size + 1;
                        this.BackColor = Color.Purple;
                        upArrowDown = false;
                        air = false;
                        gravity = -30;
                        jumpSpeed = 0;
                        hero.x = hero.x;
                        //}
                        //else if (air)
                        //{
                        //    hero.y = bonk.y + bonk.h;

                        //}

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
