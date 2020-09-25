using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        #region global variables
        Random num = new Random();
        int red = 255;
        int green = 255;
        int blue = 255;
                
        int xValue = 0;
        
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);
        SolidBrush pinkBrush = new SolidBrush(Color.Pink);

        List<Box> boxesLeft = new List<Box>();
        List<Box> boxesRight = new List<Box>();

        Box hero;
        int heroSpeed = 5;
        int heroSize = 30;
        int boxSpeedY = 3;
        int boxSpeedX = 10;

        int counter = 0;
        int speedCounter = 0;

        int i = 1;
        #endregion

        public GameScreen()
        {
            InitializeComponent();            
            OnStart();
        }

        public void makeBox()
        {
            red = num.Next(0, 256);
            green = num.Next(0, 256);
            blue = num.Next(0, 256);
            //xValue = num.Next(0, this.Width - 20);
            Box newBox = new Box(200+ xValue, 0, 20, Color.FromArgb(red, green, blue));
            boxesLeft.Add(newBox);
            //xValue = num.Next(0, this.Width - 20);
            Box newBox2 = new Box(350+ xValue, 0, 20, Color.FromArgb(red, green, blue));
            boxesRight.Add(newBox2);
        }

        public void OnStart()
        {
            
            //set game start values

            makeBox();

            hero = new Box(this.Width / 2 - heroSize / 2, 370, heroSize);
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

        private void gameLoop_Tick(object sender, EventArgs e)
        {            
            #region location
            // update location of all boxes (drop down screen)
            foreach (Box b in boxesLeft)
            {
                b.move(boxSpeedY);
            }
            
            foreach (Box b in boxesRight)
            {
                b.move(boxSpeedY);                
            }
            #endregion

            #region remove
            // remove box if it has gone of screen
            if (boxesLeft[0].y > this.Height - 25)
            {
                boxesLeft.RemoveAt(0);
              
            }
            if ( boxesRight[0].y > this.Height - 25)
            {
                boxesRight.RemoveAt(0);
            }
            #endregion

            #region add
            // add new box if it is time
            if (boxesLeft[boxesLeft.Count - 1].y > 21)
            {
                makeBox();
                xValue += boxSpeedX;
                counter++;
                speedCounter++;
            }
            int patternLength = num.Next(10, 45);
            if (counter == patternLength)
            {
                boxSpeedX = -boxSpeedX;
                counter = 0;
                speedCounter++;
            }
            #endregion

            foreach( Box b in boxesLeft)
            {
                if (b.x <= 0)
                {
                    boxSpeedX = -boxSpeedX;
                    counter = 0;
                }
            }
            foreach (Box b in boxesRight)
            {
                if (b.x >= this.Width - 20)
                {
                    boxSpeedX = -boxSpeedX;
                    counter = 0;
                }
            }

            label1.Text = "LEVEL: " + i ;

            #region speed
            if (speedCounter / i == 100)
            {
                i++;
                label1.Text = "LEVEL: " + i;
                boxSpeedY++;    
            }
            
            #endregion

            #region hero movement
            if (leftArrowDown == true)
            {
                hero.x -= heroSpeed;
            }
            if (rightArrowDown == true)
            {
                hero.x += heroSpeed;
            }
       
            #endregion movement

            #region collision

            foreach (Box b in boxesLeft.Union(boxesRight))
            {
                if (hero.Collision(b))
                {
                    gameLoop.Stop();




                    Form f = this.FindForm();
                    f.Controls.Remove(this);
                    GameOver go = new GameOver();
                    f.Controls.Add(go);

                }
            }

            #endregion

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(pinkBrush, hero.x, hero.y, hero.size, hero.size);
           

            //draw boxes to screen
            foreach (Box b in boxesLeft.Union(boxesRight))
            {
                boxBrush.Color = b.colour;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);

            }
        }
    }
}
