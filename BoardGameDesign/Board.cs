using System;

namespace BoardGameFramework
{
    public abstract class Board
    {
        protected Board(int width, int length)
        {
            Width = width;
            Length = length;
            Squares = new Square[Width, Length];

            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Length; y++)
                Squares[x, y] = new Square(x, y);
        }

        public int Length { get; set; }
        public int Width { get; set; }
        public Square[,] Squares { get; set; }

        public abstract bool ExecuteMove(Move move, Player player);
        public abstract void Render();
    }
}