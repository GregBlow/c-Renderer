using System;
using System.Drawing;

namespace renderer
{
	public class Node
	{
		private Random rand = new Random();
		public int X;
		public int Y;
		public double angle;
		public int id;
		public double multComp;
		public double addComp;
		private bool angleAssigned;
		private static int idCounter;
		private MainForm parent;
		private Line line;

		private int markerSquareWidth = 5;

		public Node (int x, int y, MainForm parent)
		{
			this.id = idCounter;
			idCounter++;
			this.X = x;
			this.Y = y;
			this.parent = parent;
			this.angle = (rand.NextDouble ())*2*Math.PI;
			this.multComp = (this.Y * (Math.Tan (this.angle)));

			Line line = new Line (new Point (this.X, this.Y), this.angle, this.parent);
			Console.Write ("\n");
			Console.Write(line.ToString ());
			Console.Write ("\n");

			DeriveEquation ();
			Console.Write ("\n");
			Console.Write ("Add. Comp.:");
			Console.Write (this.addComp);
			Console.Write ("\n");
			Console.Write ("Mult. Comp.:");
			Console.Write (this.multComp);
			Console.Write ("\n");
			Console.Write ("\n");
			Console.Write ("\n");
			Console.Write ((addComp * -1) / multComp);

		}
		public double MultComp
		{
			get{return this.multComp;}
		}
		public double AddComp
		{
			get{return this.addComp;}
		}
		public void Draw(Graphics g)
		{
			Pen p = new Pen (new SolidBrush (Color.Black));
			SolidBrush r = new SolidBrush (Color.Red);
			g.FillRectangle (r, new Rectangle (this.X-(markerSquareWidth/2), this.Y-(markerSquareWidth/2), markerSquareWidth, markerSquareWidth));
			DrawAngleLine (g, p);
		}

		private void DeriveEquation()
		{
			this.addComp = this.X - (this.Y * (Math.Tan (this.angle)));
		}

		private void DrawAngleLine(Graphics g, Pen pen)
		{
			Point p1 = new Point ();
			Point p2 = new Point ();

			double len = 16;

			double f1 = Math.Sin (this.angle) * len/2;
			double f2 = Math.Cos (this.angle) * len / 2;
			p1.X = (int)f1 + this.X;
			p1.Y = (int)f2 + this.Y;
			p2.X = this.X;
			p2.Y = this.Y;

			g.DrawLine (pen, p1, p2);
		}

	}
}

