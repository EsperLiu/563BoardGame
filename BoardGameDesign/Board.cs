using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGameDesign
{

    public class  Board
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public Square[,] Squares { get; set; }

        public Board(int width, int length)
        {
            Width = width;
            Length = length;
            Squares = new Square[Width, Length];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Length; y++)
                {
                    Squares[x, y] = (new Square(x, y));
                }
            }
        }

        public void Render()
        {
            Console.WriteLine("     [C1][C2][C3][C4][C5][C6][C7]");
            Console.WriteLine("---------------------------------");
            for (int y = 0; y < Length; y++)
            {
                Console.Write("[R"+(y+1).ToString()+"]|");
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(Squares[x, y].Render());
                }

                Console.Write("\n");
            }
            Console.WriteLine("---------------------------------");
        }

        public Square[,] PositionSnapshot()
        {
             return Squares.Clone() as Square[,];
        }
    }
}