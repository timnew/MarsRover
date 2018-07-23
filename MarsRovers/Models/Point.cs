using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windy;
using System.Diagnostics.Contracts;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public struct Point : IEquatable<Point>
    {
        public static readonly Point Empty = new Point();

        #region Constructors

        public Point(int x = 0, int y = 0)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Properties

        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #region Mathmatic Operators

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator -(Point p)
        {
            return new Point(-p.X, -p.Y);
        }

        #endregion

        #region IEquatable<Point> Members

        public bool Equals(Point other)
        {
            return
                this.X == other.X &&
                this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return
                this.X.GetHashCode() ^
                this.Y.GetHashCode();

        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            return this.Equals((Point)obj);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return "({0},{1})".ApplyFormat(X, Y) ?? base.ToString(); // HACK to satisfy the code conract
        }

        #endregion
    }
}
