using System;
using BoardGameFramework;

namespace ConnectFour
{
    public static class Utils
    {
        public static int FindConnected(int[,] matrix, int id, int dx, int dy)
        {
            int longest = 0;

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x, y] == id)
                    {
                        int counter = 1;
                        bool hasNext = true;
                        while (hasNext)
                        {
                            try
                            {
                                int xNew = x + dx * counter;
                                int yNew = y + dy * counter;
                                if (matrix[xNew, yNew] == id)
                                {
                                    counter++;
                                }
                                else
                                {
                                    hasNext = false;
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                hasNext = false;
                            }
                        }
                        if (counter > longest)
                        {
                            longest = counter;
                        }
                    }
                }
            }
            return longest;
        }

        public static int[,] CopyBoardMatrix(int[,] matrix)
        {
            int[,] copy = new int[matrix.GetLength(0),matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }

        public static string TakeStringInput(bool allowCommand)
        {
            string s = Console.ReadLine();
            if (s.StartsWith("$"))
            {
                // handle command here.
                return "";
            }
            return s;
        }
    }
}