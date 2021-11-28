using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoew_project_ai
{
    public class CActor
    {
        public int X, Y;
        public int W, H;
        public Color clr;
        public int type;
        public Bitmap im;
    }
    class Move
    {
        public int row, col;
    };

    public partial class Form1 : Form
    {
        Bitmap off;

        CActor[,] B = new CActor[3, 3];
        //Timer tt = new Timer();

        public Form1()
        {
            this.Load += Form1_Load;
            this.ClientSize = new Size(300, 300);
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
        }
        int flage = 0;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {//////crate x/////
            if (flage == 0)
            {
                int ct1 = 0; int ct = 0;
                int col = e.X / B[0, 0].W;
                int row = e.Y / B[0, 0].H;
                if (row < 3 && col < 3)
                {

                    B[row, col].im = new Bitmap("download.png");
                    B[row, col].type = 2;


                }


                flage = 1;

                DrawDubb(this.CreateGraphics());
            }
            ///check if x or o win///////
            checkingwin();
            /////create o///////////
            if (flage == 1)
            {
                Move bestMove = findBestMove(B);
                // MessageBox.Show("" + bestMove.row+","+bestMove.col);

                B[bestMove.row, bestMove.col].im = new Bitmap("download.jpg");
                B[bestMove.row, bestMove.col].type = 1;
                DrawDubb(this.CreateGraphics());

                flage = 0;
            }
            ////////check if x or o win/////
            checkingwin();
            DrawDubb(this.CreateGraphics());

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        void Create()
        {
            int ax = 0;
            int ay = 0;
            int j;
            ///create the board////
            for (int r = 0; r < 3; r++)
            {
                ax = 0;
                for (int c = 0; c < 3; c++)
                {
                    CActor pnn = new CActor();
                    pnn.X = ax;
                    pnn.Y = ay;
                    pnn.W = 100;
                    pnn.H = 100;
                    pnn.type = 0;
                    ax += pnn.W;
                    B[r, c] = pnn;
                }
                ay += 100;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            Create();
            DrawDubb(this.CreateGraphics());

        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    g.DrawRectangle(new Pen(Color.Black, 5),
                                    B[r, c].X, B[r, c].Y, B[r, c].W, B[r, c].H);

                }

            }
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (B[r, c].type == 1)
                    {
                        g.DrawImage(B[r, c].im, B[r, c].X, B[r, c].Y, 100, 100);
                    }
                    if (B[r, c].type == 2)
                    {
                        g.DrawImage(B[r, c].im, B[r, c].X, B[r, c].Y, 100, 100);
                    }
                }

            }


        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        void checkingwin()
        {
            int flage = 0;
            // Checking for Rows for X or O victory. //////
            for (int row = 0; row < 3; row++)
            {
                if (B[row, 0].type == B[row, 1].type &&
                    B[row, 1].type == B[row, 2].type)
                {

                    if (B[row, 0].type == 1)
                    {
                        MessageBox.Show("the winer is o");
                        flage = 1;
                    }
                    else if (B[row, 0].type == 2)
                    {
                        MessageBox.Show("the winer is x");
                        flage = 1;
                    }
                }
            }

            // Checking for Columns for X or O victory. //////////
            for (int col = 0; col < 3; col++)
            {

                if (B[0, col].type == B[1, col].type &&
                    B[1, col].type == B[2, col].type)
                {

                    if (B[0, col].type == 1)
                    {
                        MessageBox.Show("the winer is o");
                        flage = 1;
                    }
                    else if (B[0, col].type == 2)
                    {
                        MessageBox.Show("the winer is x");
                        flage = 1;
                    }
                }
            }

            // Checking for Diagonals for X or O victory. ////////////
            if (B[0, 0].type == B[1, 1].type && B[1, 1].type == B[2, 2].type)
            {

                if (B[0, 0].type == 1)
                {
                    MessageBox.Show("the winer is o");
                    flage = 1;
                }
                else if (B[0, 0].type == 2)
                {
                    MessageBox.Show("the winer is x");
                    flage = 1;
                }
            }

            if (B[0, 2].type == B[1, 1].type && B[1, 1].type == B[2, 0].type)
            {

                if (B[0, 2].type == 1)
                {
                    MessageBox.Show("the winer is o");
                    flage = 1;
                }
                else if (B[0, 2].type == 2)
                {
                    MessageBox.Show("the winer is x");
                    flage = 1;
                }
            }
            int ct = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (B[i, j].type != 0)
                    { ct++; }
                }
            }
            if (ct == 9 && flage != 1)
            {
                flage = 1;
                MessageBox.Show("game is draw");
            }

            ////////////reset the game////////
            if (flage == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        B[i, j].type = 0;
                    }
                }
            }


        }

        static Boolean isMovesLeft(CActor[,] B)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (B[i, j].type == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static int evaluate(CActor[,] B)

        {
            // Checking for Rows for X or O victory. 
            for (int row = 0; row < 3; row++)
            {
                if (B[row, 0].type == B[row, 1].type &&
                    B[row, 1].type == B[row, 2].type)
                {
                    if (B[row, 0].type == 1)
                        return +10;
                    else if (B[row, 0].type == 2)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory. 
            for (int col = 0; col < 3; col++)
            {
                if (B[0, col].type == B[1, col].type &&
                    B[1, col].type == B[2, col].type)
                {
                    if (B[0, col].type == 1)
                        return +10;

                    else if (B[0, col].type == 2)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory. 
            if (B[0, 0].type == B[1, 1].type && B[1, 1].type == B[2, 2].type)
            {
                if (B[0, 0].type == 1)
                    return +10;
                else if (B[0, 0].type == 2)
                    return -10;
            }

            if (B[0, 2].type == B[1, 1].type && B[1, 1].type == B[2, 0].type)
            {
                if (B[0, 2].type == 1)
                    return +10;
                else if (B[0, 2].type == 2)
                    return -10;
            }

            // Else if none of them have won then return 0 
            return 0;
        }
        static int minimax(CActor[,] B, int depth, Boolean isMax)
        {
            int score = evaluate(B);

            //if max win return the score//
            if (score == 10)
            {
                return score;
            }
            //if min win return the score//
            if (score == -10)
            {
                return score;
            }
            // If there are no more moves so it is draw//////  

            if (isMovesLeft(B) == false)
            {
                return 0;
            }
            // If this maximizer's move 
            if (isMax)
            {
                int best = -1000;

                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty 
                        if (B[i, j].type == 0)
                        {
                            // Make the move 
                            B[i, j].type = 1;

                           //recursion// 
                            best = Math.Max(best, minimax(B,
                                            depth + 1, !isMax));

                            // Undo the move 
                            B[i, j].type = 0;
                        }
                    }
                }
                return best;
            }

            // If this minimizer's move 
            else
            {
                int best = 1000;

                // Traverse all cells 
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty 
                        if (B[i, j].type == 0)
                        {
                            // Make the move 
                            B[i, j].type = 2;
                            //recursion//
                            best = Math.Min(best, minimax(B,
                                            depth + 1, !isMax));

                            // Undo the move 
                            B[i, j].type = 0;
                        }
                    }
                }


                return best;

            }
        }
        static Move findBestMove(CActor[,] B)
        {
            int bestVal = -1000;
            Move bestMove = new Move();
            bestMove.row = -1;
            bestMove.col = -1;

            // Traverse all cells, evaluate minimax function 
            // for all empty cells. And return the cell 
            // with optimal value. 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (B[i, j].type == 0)
                    {
                        // Make the move 
                        B[i, j].type = 1;

                        // compute evaluation function for this 
                        // move. 
                        int moveVal = minimax(B, 0, false);

                        // Undo the move 
                        B[i, j].type = 0;

                       //if the  moveval is grater catch the pos//
                        if (moveVal > bestVal)
                        {
                            bestMove.row = i;
                            bestMove.col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }


            return bestMove;
        }




    }
}
