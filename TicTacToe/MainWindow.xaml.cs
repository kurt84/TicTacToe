using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] board;
        private Label[,] labels;
        private int blockRow, blockCol;
        private bool playerMove, canLose, canWin, gameOver,secondMove, takeCenter;
        
        public MainWindow()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ResetGame()
        {
            board = new int[3, 3];
            labels = new Label[3, 3];
            playerMove = false;
            canWin = false;
            canLose = false;
            gameOver = false;
            secondMove = false;
            takeCenter = false;
            StatusBox.Text = "";

            labels[0, 0] = Grid1Label;
            labels[0, 1] = Grid2Label;
            labels[0, 2] = Grid3Label;
            labels[1, 0] = Grid4Label;
            labels[1, 1] = Grid5Label;
            labels[1, 2] = Grid6Label;
            labels[2, 0] = Grid7Label;
            labels[2, 1] = Grid8Label;
            labels[2, 2] = Grid9Label;

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    board[i, j] = 0;
                    labels[i, j].Content = null;
                }
            }

            MakeMove(0, 0);
            playerMove = true;
            secondMove = true;
        }


        private void Grid1Button(object sender, RoutedEventArgs e)
        {
            if (Grid1Label.Content == null && playerMove)
            {
                OMove(0, 0);
                Grid1Label.Content = "O";
            }
        }
        private void Grid2Button(object sender, RoutedEventArgs e)
        {
            if (Grid2Label.Content == null && playerMove)
            {
                OMove(0, 1);
                takeCenter = true;
                XMove();
            }
        }
        private void Grid3Button(object sender, RoutedEventArgs e)
        {
            if (Grid3Label.Content == null && playerMove)
            {
                OMove(0, 2);
                XMove();
            }
        }
        private void Grid4Button(object sender, RoutedEventArgs e)
        {
            if (Grid4Label.Content == null && playerMove)
            {
                OMove(1, 0);
                takeCenter = true;
                XMove();
            }
        }
        private void Grid5Button(object sender, RoutedEventArgs e)
        {
            if (Grid5Label.Content == null && playerMove)
            {
                OMove(1, 1);
                XMove();
            }
        }
        private void Grid6Button(object sender, RoutedEventArgs e)
        {
            if (Grid6Label.Content == null && playerMove)
            {
                OMove(1, 2);
                takeCenter = true;
                XMove();
            }
        }
        private void Grid7Button(object sender, RoutedEventArgs e)
        {
            if (Grid7Label.Content == null && playerMove)
            {
                OMove(2, 0);
                XMove();
            }
        }
        private void Grid8Button(object sender, RoutedEventArgs e)
        {
            if (Grid8Label.Content == null && playerMove)
            {
                OMove(2, 1);
                takeCenter = true;
                XMove();
            }
        }
        private void Grid9Button(object sender, RoutedEventArgs e)
        {
            if (Grid9Label.Content == null && playerMove)
            {
                OMove(2, 2);
                XMove();
            }
        }
        private void XMove()
        {
            int row = 0, col = 0, score = 0;
            if (secondMove && takeCenter)
            {
                score = 3;
                row = 1;
                col = 1;
                secondMove = false;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        int tempScore = 0;
                        tempScore += ScoreDiagonal(i,j);
                        tempScore += ScoreRow(i,j);
                        tempScore += ScoreColumn(i,j);
                        if (tempScore >= score)
                        {
                            score = tempScore;
                            row = i;
                            col = j;
                        }
                    }
                }
            }
            if (gameOver)
                DisplayWinner();
            else if (canLose)
                MakeMove(blockRow, blockCol);
            else
                MakeMove(row, col);
            playerMove = true;
            canLose = false;
        }
        private void OMove(int row, int col)
        {
            MakeMove(row, col);
            playerMove = false;
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void MakeMove(int row, int col)
        {
            if (playerMove && !gameOver)
            {
                board[row, col] = -1;
                labels[row, col].Content = "O";
            }
            else if (!playerMove && !gameOver)
            {
                board[row, col] = 1;
                labels[row, col].Content = "X";
            }
        }
        private void MakeWinningMove(int r, int c)
        {
            canWin = true;
            MakeMove(r, c);
            gameOver = true;
        }
        private void MakeBlockingMove(int r, int c)
        {
            canLose = true;
            blockRow = r;
            blockCol = c;
        }

        private int ScoreColumn(int row, int col)
        {
            int tempScore = board[0, col] + board[1, col] + board[2, col];
            if (tempScore == -2)
            {
                MakeBlockingMove(row, col);
            }
            else if (tempScore == 2)
            {
                MakeWinningMove(row, col);
            }
            return tempScore;
        }
        private int ScoreRow(int row, int col)
        {
            int tempScore = board[row,0] + board[row,1] + board[row,2];
            if (tempScore == -2)
            {
                MakeBlockingMove(row, col);
            }
            else if (tempScore == 2)
            {
                MakeWinningMove(row, col);
            }
            return tempScore;
        }
        private int ScoreDiagonal(int row, int col)
        {
            int tempScore = 0;
            if (row == 1 && col == 1)
            {
                int downSlope = board[0, 0] + board[2, 2];
                int upSlope = board[2, 0] + board[0, 2];
                if ((upSlope == 2) || (downSlope == 2))
                {
                    MakeWinningMove(row, col);
                    return 2;
                }
                else if ((upSlope == -2) || (downSlope == -2))
                {
                    MakeBlockingMove(row, col);
                    return -2;
                }
                else
                    return upSlope + downSlope;
            }
            else if (row == 1 || col == 1)
                return 0;
            else if (row == col)
            {
                tempScore = board[0, 0] + board[1, 1] + board[2, 2];
                if (tempScore == -2)
                {
                    MakeBlockingMove(row, col);
                }
                else if (tempScore == 2)
                {
                    MakeWinningMove(row, col);
                }
                return tempScore;
            }
            else
            {
                tempScore = board[col, row] + board[1, 1];
                if (tempScore == -2)
                {
                    MakeBlockingMove(row, col);
                }
                else if (tempScore == 2)
                {
                    MakeWinningMove(row, col);
                }
                return tempScore;
            }
        }
        private void DisplayWinner()
        {
            if (canWin)
                StatusBox.Text = "X Wins!";
            else
                StatusBox.Text = "Draw!";
        }
    }
}
