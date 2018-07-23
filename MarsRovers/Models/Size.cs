using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public struct Size : IEquatable<Size>
    {
        public static readonly Size Empty = new Size();

        public Size(int width, int height)
            : this()
        {
            this.Width = width;
            this.Height = height;
        }

        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        #region IEquatable<Size> Members

        public bool Equals(Size other)
        {
            return
                this.Width == other.Width &&
                this.Height == other.Height;
        }

        public override int GetHashCode()
        {
            return
                this.Height.GetHashCode() ^
                this.Width.GetHashCode();

        }

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;

            return this.Equals((Size)obj);
        }

        public static bool operator ==(Size a, Size b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Size a, Size b)
        {
            return !a.Equals(b);
        }

        #endregion
    }
}