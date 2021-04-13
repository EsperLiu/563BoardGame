using BoardGameFramework;
using System;
using System.Collections.Generic;

namespace ConnectFour
{
    public class C4GreedyStrategy : IComputerStrategy
    {
        public Move GenerateMove(Board board, Player player)
        {
            Random rand = new Random();

            ConnectFourBoard gameBoard = board as ConnectFourBoard;

            int bestCount = 1;
            string selectedMove = "0";
            foreach (int possibleMove in gameBoard.OpenColumns)
            {
                int[,] imaginaryMatrix = Utils.CopyBoardMatrix(gameBoard.MatrixFormat());
                int result = TryMove(imaginaryMatrix, possibleMove, player.Id);
                if (result >= 4)
                {
                    selectedMove = possibleMove.ToString();
                    break;
                }
                else if (result > bestCount)
                {
                    bestCount = result;
                    selectedMove = possibleMove.ToString();
                }
                else if (result == bestCount)
                {
                    if (rand.NextDouble() < 0.1)
                    {
                        selectedMove = possibleMove.ToString();
                    }
                };
            }
            return new ConnectFourMove(selectedMove, player.Id);
        }

        private int TryMove(int[,] matrix, int targetColumn, int id)
        {
            for (int y = matrix.GetLength(1) - 1; y >= 0; y--)
            {
                if (matrix[targetColumn, y] == 0)
                {
                    matrix[targetColumn, y] = id;
                    break;
                }
            }

            List<(int, int)> directions = new List<(int, int)>
            {
                (1,0), (0,1), (1,1), (-1,1)
            };

            int singlePieceBest = 0;

            foreach (var direction in directions)
            {
                int n = Utils.FindConnected(matrix, id, direction.Item1, direction.Item2);
                if (n >= 4)
                {
                    return n; // Found a winning move, return immediately.
                }
                if (n > singlePieceBest)
                {
                    singlePieceBest = n;
                }
            }
            return singlePieceBest;
        }
    }
}