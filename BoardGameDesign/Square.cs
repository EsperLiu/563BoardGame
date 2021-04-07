﻿using System;
using System.Security.Cryptography.X509Certificates;

namespace BoardGameDesign
{
    public class Square
    {
        private static int _idCount = 1;

        public Piece Occupant { get; set; }
        public int Id { get; }
        public int X { get; set; }
        public int Y { get; set; }

        public Square(int x, int y)
        {
            Occupant = null;
            X = x;
            Y = y;
            Id = _idCount;
            _idCount += 1;
        }

        public string Render()
        {
            return $"[{(Occupant != null ? Occupant.Token : '　')}]";
        }

        public override string ToString()
        {
            return $"({X},{Y})[{Id}]";
        }
    }
}