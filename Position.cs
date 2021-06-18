using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfishIsland
{
    public class Position
    {
        private int x;
        public int X
        {
            get { return x; }
            set
            {
                if (value < 1)
                    x = 1;
                else if (value > 20)
                    x = 20;
                else
                    x = value;
            }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                if (value < 1)
                    y = 1;
                else if (value > 20)
                    y = 20;
                else
                    y = value;
            }
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsAround(Position position)
        {
            return Math.Abs(X - position.X) < 2 && Math.Abs(Y - position.Y) < 2;
        }

        public bool IsTogetherWith(Position position)
        {
            return X == position.X && Y == position.Y;
        }

        public void RandomMove()
        {
            X += Form1.random.Next(-1, 2);
            Y += Form1.random.Next(-1, 2);
        }

        public void MoveTo(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public static Position GetRandom()
        {
            return new Position(
                Form1.random.Next(0, 20),
                Form1.random.Next(0, 20));
        }
    }
}
