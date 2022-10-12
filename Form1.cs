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
        List<square> nextMoves = new List<square>();
        int movesTaken = 0;
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
            Pen knightsTourPen = new Pen(Color.Black,10); //Draws path of knight in knight's tour
            Brush knightsTourBrush = new SolidBrush(Color.Blue);
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
                            g.FillRectangle(knightsTourBrush, (xLoop * 100) + 10, (yLoop * 100) + 10, 80, 80);  
                        
                    }
                }
            }


        }


        private void NextMoveButton_Click(object sender, EventArgs e)
        {
            int nextX, nextY;
            square targetNextMove;
            if (knight.initialized)
            {
                nextMoves = knight.checkNextMoves(knight.currentPosition.getY(), knight.currentPosition.getX());
                targetNextMove = knight.quantifyNextMoves(nextMoves);
                knight.currentPosition = knight.theBoard[targetNextMove.getY(), targetNextMove.getX()];
                knight.theBoard[knight.currentPosition.getY(), knight.currentPosition.getX()].close();
            }
            else
            {
                
                try
                {
                    //Attempt to read user input for starting position
                    nextY = Convert.ToInt32(yTextBox.Text);
                    nextX = Convert.ToInt32(xTextBox.Text);
                    //Ensure that these are valid inputs
                    if (nextY < 0)
                    {
                        nextY = 0;
                    }

                    if (nextY > 7)
                    {
                        nextY = 7;
                    }

                    if (nextX < 0)
                    {
                        nextY = 0;
                    }

                    if (nextX > 7)
                    {
                        nextX = 7;
                    }
                }
                catch
                {
                    //If no inputs are read, default to Y,X = 0,3
                    nextY = 0;
                    nextX = 3;
                }
                knight.currentPosition = knight.theBoard[nextY, nextX];
                knight.theBoard[knight.currentPosition.getY(), knight.currentPosition.getX()].close();
                
                knight.initialized = true;
            }
            movesTaken += 1;
            stepsLabel.Text = $"Number of Moves Taken: {movesTaken}";
            this.Refresh();
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
        public square currentPosition;
        public bool initialized = false;
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

            return validMoves;
        }

        //Given a list of squares, returns the index of the one that has the fewest possible next-moves
            //Warnsdorff's Rule: The Knight's Tour can (generally) be completed by finding the next-move that has the fewest next-moves from that point
        public square quantifyNextMoves(List<square> nextMoves)
        {
            square chosenNextMove = nextMoves[0];
            List<int> nextMoveNextMoves = new List<int>();
            int indexOfChosenNextMove = 0;
            //For each square in nextMoves:
                //Run checkNextMoves() on that square, keeping track of the next-move that has the fewest next-moves
                //Return the element in nextMoves with the fewest nextMoves
            for (int i = 0; i < nextMoves.Count; i++)
            {
                nextMoveNextMoves.Add(checkNextMoves(nextMoves[i].getY(), nextMoves[i].getX()).Count);
                //Check if this is (so far) the nextMove with the fewest nextMoves
                if(nextMoveNextMoves[i] < nextMoveNextMoves[indexOfChosenNextMove])
                {
                    indexOfChosenNextMove = i;
                    chosenNextMove = nextMoves[i];
                }
            }

            return chosenNextMove;
        }

    }//End class Board
}//End namespace
