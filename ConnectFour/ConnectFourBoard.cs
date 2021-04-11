using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BoardGameFramework;

namespace ConnectFour
{
    public class ConnectFourBoard : Board
    {
        public ConnectFourBoard(int width, int length) : base(width, length)
        {

        }

        public List<int> OpenColumns // determines which columns are still open
        {
            get
            {
                List<int> oc = new List<int>();
                for (int i = 0; i < this.Width; i++)
                {
                    if (this.Squares[i, 0].Occupant == null)
                    {
                        oc.Add(i);
                    }
                }

                return oc;
            }
        }

        public override bool ExecuteMove(Move move, Player player)
        {
            var targetColumn = (move as ConnectFourMove).TargetColumn;
            for (var y = Length - 1; y >= 0; y--)
            {
                if (Squares[targetColumn, y].Occupant == null)
                {
                    Squares[targetColumn, y].Occupant =
                        new ConnectFourPiece(player,
                            (player as ConnectFourPlayer).Token, Squares[targetColumn, y]);
                    return true;
                }
            }
            return false;
        }

        public override void Render()
        {
            Console.WriteLine("     [C1][C2][C3][C4][C5][C6][C7]");
            Console.WriteLine("---------------------------------");
            for (var y = 0; y < Length; y++)
            {
                Console.Write("[R" + (y + 1) + "]|");
                for (var x = 0; x < Width; x++) Console.Write(Squares[x, y].Render());

                Console.Write("\n");
            }
            Console.WriteLine("---------------------------------");
            
        }

        public int[,] MatrixFormat()
        {
            int[,] matrix = new int[Width, Length];
            for (int y = 0; y < Length; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Squares[x, y].Occupant == null)
                    {
                        matrix[x, y] = 0;
                    }
                    else if ((Squares[x, y].Occupant.Owner as ConnectFourPlayer).Color == "Yellow")
                    {
                        matrix[x, y] = 1;
                    }
                    else if ((Squares[x, y].Occupant.Owner as ConnectFourPlayer).Color == "Red")
                    {
                        matrix[x, y] = 2;
                    }
                }
            }

            return matrix;
        }

        public void RenderMatrix()
        {
            int[,] m = MatrixFormat();
            for (int y = 0; y < Length; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(m[x, y]);
                }
                Console.Write("\n");
            }
        }

    }
}
