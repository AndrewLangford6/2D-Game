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
using System.Threading;

namespace _2D_Game
{
    public partial class MainScreen : UserControl
    {
        //list to hold blocks
        List<Block> blocksList = new List<Block>();

        //soundplayers for sounds
        SoundPlayer playerJump = new SoundPlayer(Properties.Resources.Jump);
        SoundPlayer playerFall = new SoundPlayer(Properties.Resources.Fall);

        //random generator
        Random randGen = new Random();

        //bools for keypresses
        Boolean leftArrowDown, rightArrowDown, upArrowDown;

        //drawbrush for blocks
        SolidBrush brush = new SolidBrush(Color.White);

        //all variables used
        public static int gravity, jumpSpeed, counter, counter2, groovin, score, randWidth, blockOff, hSize;


        public static bool left, right;

        //create the hero
        Hero hero;


        public MainScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            //create the default starting blocks

            Block b1 = new Block(100, 250, 20, 100);
            Block b2 = new Block(0, 400, 20, 800);
            Block b3 = new Block(300, 100, 20, 100);

            blocksList.Add(b1);
            blocksList.Add(b2);
            blocksList.Add(b3);


            //initialize variables

            gravity = -30;
            jumpSpeed = 0;

            groovin = 4;

            randWidth = 10;

            hSize = 30;

            //create the hero
            hero = new Hero(this.Width / 2 - hSize / 2, 400, hSize);

        }

        //what happens when you lose
        public void end()
        {
            playerFall.Play();
            timer1.Enabled = false;
            Thread.Sleep(1500);


            Form f = this.FindForm();
            f.Controls.Remove(this);

            MenuScreen mes = new MenuScreen();
            f.Controls.Add(mes);
            mes.Focus();
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
                    playerJump.Play();
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
            }
        }
        public void gameLoop_Tick(object sender, EventArgs e)
        {

            //when you die, end the game
            if (hero.y + hero.size >= this.Height)
            {
                end();
            }

            //constants such as falling and counters
            hero.Fall();
            counter++;
            counter2++;


            //if you press the up key jump
            if (upArrowDown)
            {
                hero.Jump();
            }

            //create the new platforms
            if (counter == 25)
            {

                randWidth = randGen.Next(0, this.Width - 99);
                blockOff = randGen.Next(1, 3);

                if (blockOff == 1)
                {
                    Block b1 = new Block(randWidth, -30, 20, 100);
                    blocksList.Add(b1);
                    counter = 0;
                }
                else
                {
                    Block b1 = new Block(randWidth, -30, 20, 100);
                    blocksList.Add(b1);
                    counter = 0;
                }
            }

            //increase speed at which the screen moves
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

                //check if the hero touches the blocks
                if (heroRec.IntersectsWith(blockRec))
                {
                    if (hero.x + hero.size <= bonk.x &&
                    hero.x >= bonk.x + bonk.w)
                    {

                    }
                    else if (hero.x + hero.size >= bonk.x &&
                        hero.x <= bonk.x + bonk.w && hero.y < bonk.y)
                    {
                        hero.y = bonk.y - hero.size + 1;

                        //reset the variables after a jump
                        upArrowDown = false;
                        gravity = -30;
                        jumpSpeed = 0;
                        hero.x = hero.x;
                    }
                    else
                    {
                    }
                }
            }

            //calculate the score and update it
            score = score + groovin / 4;
            scoreLabel.Text = Convert.ToString(score);

            //refresh the screen
            Refresh();
        }
        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw the hero and the boxes to the screen

            e.Graphics.FillRectangle(hero.heroBrush, hero.x, hero.y, hero.size, hero.size);

            foreach (Block b in blocksList)
            {
                e.Graphics.FillRectangle(brush, b.x, b.y, b.w, b.h);
            }

        }
    }
}
