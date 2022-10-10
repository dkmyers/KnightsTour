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
        private board knight = new board(0, 3);
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
            
            Graphics g = e.Graphics;
            Pen backgroundPen = new Pen(Color.Black); //Exterior of boxes for chess board
            Brush backgroundBrush = new SolidBrush(Color.Black); //Interior of boxes in chess board
            Pen knightsTourPen = new Pen(Color.Red); //Draws path of knight in knight's tour
            Brush knightsTourBrush = new SolidBrush(Color.Red);
            //Draw chess board background

            for (int yBGDraw = 0; yBGDraw < 8; yBGDraw++)
            {
                RectDraw.Y = yBGDraw * 100;
                for (int xBGDraw = 0; xBGDraw < 8; xBGDraw++)
                {
                    RectDraw.X = xBGDraw * 100;
                    g.DrawRectangle(backgroundPen, RectDraw);
                    if ((yBGDraw + xBGDraw) % 2 == 0)
                    {
                        g.FillRectangle(backgroundBrush, RectDraw);
                    }
                }
            }

            //Draw Knight's Tour
            for (int yLoop = 0; yLoop < 8; yLoop++)
            {
                for (int xLoop = 0; xLoop < 8; xLoop++)
                {
                    if (!knight.checkSquare(yLoop,xLoop)) {
                        g.DrawRectangle(knightsTourPen, xLoop*100, yLoop*100, 100, 100);
                        g.FillRectangle(knightsTourBrush, xLoop * 100, yLoop * 100, 100, 100);
                     }
                }
            }


        }


        private void NextMoveButton_Click(object sender, EventArgs e)
        {
            knight.theBoard[0, 3].close();
            this.Refresh();
            knight.checkNextMoves(0, 3);
        }
    }//End Class Form1

    //Square object contains info about one chess square
    public class square
    {
        int x;
        int y;
        bool open;
        char tag;
        //Constructor with given Y, X coordinates, which denotes its location within the board's array
        public square(int givenY, int givenX)
        {
            this.y = givenY;
            this.x = givenX;
            this.open = true;
        }

        //Changes the tag stored at this square, so Paint event knows what to paint
        public void setTag(char givenChar)
        {
            this.tag = givenChar;
        }

        //sets the 'open' variable to 0, as the Knight has passed through this location before
        public void close()
        {
            this.open = false;
        }

        //Used in board object to find open squares
        public bool getOpen()
        {
            return this.open;
        }

        public int getY()
        {
            return this.y;
        }

        public int getX()
        {
            return this.x;
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
        public square[,] theBoard = new square[8, 8];
        square currentPosition;
        public board(int startY, int startX)
        {
            this.currentPosition = new square(startY, startX);
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Console.WriteLine($"Board constructor: {y} {x}");
                    theBoard[y, x] = new square(y, x);
                }
            }
        }

        ~board()
        {

        }

        public bool checkSquare(int givenY, int givenX)
        {
            return this.theBoard[givenY, givenX].getOpen();
        }

        //Given a Y and X value, determines if those are within the range of the chess board
        //Each comparison made in checkNextMoves() needed to ensure that the coordinates were valid for the board[][] object
        //so that functionality was put into this method
        bool checkIfValidSquare(int givenY, int givenX)
        {
            if (givenY > 7)
            {
                //Console.Write("Y > 7");
                return false;
            }

            if (givenY < 0)
            {
                //Console.Write("Y < 0");
                return false;
            }

            if (givenX > 7)
            {
                //Console.Write("X > 7");
                return false;
            }

            if (givenX < 0)
            {
                //Console.Write("X < 0");
                return false;
            }

            return true;
        }

        //From a given square, checks the 'open' value of all possible next moves the knight could take
        //Returns an array of the squares which are open
        public List<square> checkNextMoves(int givenY, int givenX)
        {
            List<square> validMoves = new List<square>();
            int tempY, tempX;

            //Check the two spots above the given square
            tempY = givenY - 2;
            tempX = givenX - 1;
            if(checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            tempX = givenX + 1;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            //Check the two spots below the given square
            tempY = givenY + 2;
            tempX = givenX - 1;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            tempX = givenX + 1;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            //check the two spots to the left of the given square
            tempY = givenY - 1;
            tempX = givenX - 2;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            tempY = givenY + 1;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            //check the two spots to the right of the given square
            tempY = givenY - 1;
            tempX = givenX + 2;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            tempY = givenY + 1;
            if (checkIfValidSquare(tempY, tempX) && theBoard[tempY, tempX].getOpen())
            {
                validMoves.Add(theBoard[tempY, tempX]);
            }

            Console.WriteLine($"{validMoves.Count()}");

            for(int i = 0; i < validMoves.Count(); i++)
            {
                Console.WriteLine($"{i}: {validMoves[i].getY()}, {validMoves[i].getX()}");
            }


           

            return validMoves;
        }

        //Given a list of squares, returns the index of the one that has the fewest possible next-moves
            //Warnsdorff's Rule: The Knight's Tour can (generally) be completed by finding the next-move that has the fewest next-moves from that point
        public square quantifyNextMoves(List<square> nextMoves)
        {
            square chosenNextMove = nextMoves[0];
            //For each square in nextMoves:
                //Run checkNextMoves() on that square, keeping track of the next-move that has the fewest next-moves
                //Return the element in nextMoves with the fewest nextMoves
            return chosenNextMove;
        }

    }//End class Board
}//End namespace
