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

        Boolean leftArrowDown, rightArrowDown, upArrowDown;

        int hSize = 30;
        public static bool jumping = false;
        public static int gravity, jumpSpeed, counter;


        Hero hero;

        public MainScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            hero = new Hero(Width / 2 - hSize / 2, 400, hSize);

            gravity = -20;
            jumpSpeed = 4;
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
                    upArrowDown = false;
                    break;
            }
        }
        public void gameLoop_Tick(object sender, EventArgs e)
        {

            hero.Fall();
            counter++;

            if (leftArrowDown)
            {
                hero.Move("left");
            }

            if (rightArrowDown)
            {
                hero.Move("right");
            }


            if (upArrowDown )
            {

                hero.Jump();
            }
            if (!upArrowDown)
            {
                gravity = -20;
            }
            if (hero.y + hero.size > 400)
            {
                hero.y = 400 - hero.size;
            }

            Refresh();
        }
        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(hero.heroBrush, hero.x, hero.y, hero.size, hero.size);
        }
    }
}
