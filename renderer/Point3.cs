using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace renderer
{
	public class Point3
	{
		public int X;
		public int Y;
		public int Z;
		public Point3 (int x, int y, int z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
		public String ToString()
		{
			return String.Concat (String.Concat (String.Concat(this.X,", "), String.Concat(this.Y, ", ")), Z);
		}
		public Point StripZ()
		{
			return new Point (this.X, this.Y);
		}
	}
}

