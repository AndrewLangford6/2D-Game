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
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        

        private void startButton_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            f.Controls.Remove(this);

            MainScreen ms = new MainScreen();
            f.Controls.Add(ms);
            ms.Focus();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            //close the program
            Application.Exit();
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {

            //display the score after you lose
            string scoreO = Convert.ToString(MainScreen.score);
            scoreLabel.Text = "Previous Score:   " + scoreO;
        }
    }
}
