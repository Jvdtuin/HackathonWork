using System;

namespace HackathonWork
{
    public class Point
    {
        private int x;
        private int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; } }

        public int Y { get { return y; } }

        internal double Distance(Point position)
        {
            int dx = x - position.X;
            int dy = y - position.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        internal double Distance(int x, int y)
        {
            int dx = this.x - x;
            int dy = this.y - y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}