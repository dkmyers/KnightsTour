using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheKnightsTour
{
    public partial class Form1 : Form
    {
        private Rectangle RectDraw;
        private board knight;
        public Form1()
        {
            InitializeComponent();
            RectDraw.Width = 100;
            RectDraw.Height = 100;
        }

        //Each frame:
        //Draw chess board

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Dispose();
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black); //Exterior
            Brush b = new SolidBrush(Color.Black); //Interior

            //Draw chess board background

            for (int i = 0; i < 8; i++)
            {
                RectDraw.Y = i * 100;
                for (int j = 0; j < 8; j++)
                {
                    RectDraw.X = j * 100;
                    g.DrawRectangle(p, RectDraw);
                    if ((i + j) % 2 == 0)
                    {
                        g.FillRectangle(b, RectDraw);
                    }
                }
            }

            //Draw Knight's Tour
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //if board[i][j].CheckOpen: DrawText(board[i][j].getTag()
                }
            }


        }


        private void NextMoveButton_Click(object sender, EventArgs e)
        {
            
        }
    }//End Class Form1

    //Square object contains info about one chess square
    public class square
    {
        int x;
        int y;
        int open;
        char tag;
        //Constructor with given X, Y coordinates
        //The coordinates are pixel locations of top-left corner of a square
        //Each square is 100x100 pixels in size
        public square(int givenY, int givenX)
        {
            this.y = givenY;
            this.x = givenX;
            this.open = 1;
        }

        //Changes the tag stored at this square, so Paint event knows what to paint
        public void setTag(char givenChar)
        {
            this.tag = givenChar;
        }

        //sets the 'open' variable to 0, as the Knight has passed through this location before
        void close()
        {

        }

        //Used in board object to find open squares
        public int getOpen()
        {
            return this.open;
        }

        //destructor
        ~square()
        {

        }
    }//End Class Square

    //board object manages 8x8 grid of square objects
    //Board is instantiated and handled in form1 so form1_Paint knows which spots to paint on
    //When Button is pushed, board determines next 
    public class board
    {
        //
        private square[][] theBoard;
        square currentPosition;
        board()
        {
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    theBoard[y][x] = new square(y * 100, x * 100);
                }
            }
        }

        ~board()
        {

        }

        public int checkSquare(int givenY, int givenX)
        {
            return theBoard[givenY][givenX].getOpen();
        }
    }//End class Board
}//End namespace
