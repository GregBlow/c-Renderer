using System;
using System.Drawing;

namespace renderer
{
	public class Line
	{
		private Random rand = new Random ();
		Point p1;
		double angle;

		double addComp;
		double multComp;

		private Point yAxisIntercept;
		private Point xAxisIntercept;

		private Point lowerEdgeIntercept;
		private Point rightEdgeIntercept;

		MainForm parent;

		public Line (Point point, double angle, MainForm parent)
		{
			this.p1 = point;
			this.angle = angle;
			this.parent = parent;

			this.CalculateLineEquation ();
		}

		private void CalculateLineEquation(){
			multComp = Math.Tan (angle);
			addComp = this.p1.X - this.p1.Y * multComp;

			xAxisIntercept = new Point((int)addComp, 0);
			CalculateYAxisIntercept ();
			CalculateLowerEdgeIntercept ();
			CalculateRightEdgeIntercept ();

		}

		private void CalculateLowerEdgeIntercept()
		{
			this.lowerEdgeIntercept = new Point ((int)((parent.ClientSize.Height * multComp) + addComp), parent.ClientSize.Height);
		}

		private void CalculateRightEdgeIntercept()
		{
			this.rightEdgeIntercept = new Point(parent.ClientSize.Width, (int)((parent.ClientSize.Width - addComp)/multComp));
		}

		private void CalculateYAxisIntercept()
		{
			this.yAxisIntercept = new Point(0, (int)((addComp * -1) / multComp)); 
		}

		public String ToString()
		{
			String retString = null;
			if (p1 != null && angle != null)
			{
				retString = String.Concat("X = ", String.Concat(String.Concat(String.Concat("Y * ", this.multComp)," + "), this.addComp));

			}
			return retString;
		}
		public double AddComp
		{
			get{ return this.addComp; }
		}
		public double MultComp
		{
			get{ return this.multComp; }
		}
		public double Angle
		{
			get{return this.angle;}
			set{
				this.angle = value;
				this.CalculateLineEquation();
			}
		}

		public void ReportData()
		{
			Point p1 = new Point(0,0);
			Point p2 = new Point(this.p1.X, this.p1.Y);

			bool lineCrossesTop = this.xAxisIntercept.X >= 0 && this.xAxisIntercept.X <= parent.ClientSize.Width;
			bool lineCrossesLeft = this.yAxisIntercept.Y >= 0 && this.yAxisIntercept.Y <= parent.ClientSize.Height;
			bool lineCrossesBottom = this.lowerEdgeIntercept.X >= 0 && this.lowerEdgeIntercept.X <= parent.ClientSize.Width;
			bool lineCrossesRight = this.rightEdgeIntercept.Y >= 0 && this.rightEdgeIntercept.Y <= parent.ClientSize.Height; 

			Console.Write ("p1: ");
			Console.Write (p1);
			Console.Write ("\n");
			Console.Write ("p2: ");
			Console.Write (p2);
			Console.Write ("\n\nLine crosses top: ");
			Console.Write (lineCrossesTop);
			Console.Write ("\n\nUpperEdgeIntercept: ");
			Console.Write (this.xAxisIntercept);
			Console.Write ("\n\nLine crosses bottom: ");
			Console.Write (lineCrossesBottom);
			Console.Write ("\n\nLowerEdgeIntercept: ");
			Console.Write (this.lowerEdgeIntercept);
			Console.Write ("\n\nLine crosses left: ");
			Console.Write (lineCrossesLeft);
			Console.Write ("\n\nLeftEdgeIntercept: ");
			Console.Write (this.yAxisIntercept);
			Console.Write ("\n\nLine crosses right: ");
			Console.Write (lineCrossesRight);
			Console.Write ("\n\nRightEdgeIntercept: ");
			Console.Write (this.rightEdgeIntercept);
			Console.Write ("\n");

		}

		public void DrawLine(Graphics g, Pen pen)
		{
			Point p1 = new Point(0,0);
			Point p2 = new Point(this.p1.X, this.p1.Y);
			pen.DashPattern = new float[]{ 2.0F, 2.0F, 1.0F, 3.0F };

			bool lineCrossesTop = this.xAxisIntercept.X >= 0 && this.xAxisIntercept.X <= parent.ClientSize.Width;
			bool lineCrossesLeft = this.yAxisIntercept.Y >= 0 && this.yAxisIntercept.Y <= parent.ClientSize.Height;
			bool lineCrossesBottom = this.lowerEdgeIntercept.X >= 0 && this.lowerEdgeIntercept.X <= parent.ClientSize.Width;
			bool lineCrossesRight = this.rightEdgeIntercept.Y >= 0 && this.rightEdgeIntercept.Y <= parent.ClientSize.Height; 

			if (lineCrossesTop) 
			{
				p1 = this.xAxisIntercept;
				if (lineCrossesLeft) {
					p2 = this.yAxisIntercept;
				} else if (lineCrossesBottom) {
					p2 = this.lowerEdgeIntercept;
				} else {
					p2 = this.rightEdgeIntercept;
				}

			} 
			else if (lineCrossesLeft) {

				p1 = this.yAxisIntercept;
				if (lineCrossesBottom) {
					p2 = this.lowerEdgeIntercept;
				} else {
					p2 = this.rightEdgeIntercept;
				}

			} 
			else
			{
				p1 = this.lowerEdgeIntercept;
				p2 = this.rightEdgeIntercept;
			}



			SolidBrush r = new SolidBrush (Color.Red);
			g.DrawLine (pen, p1, p2);
			g.FillRectangle (r, new Rectangle (p1.X-3, p1.Y-3, 7, 7));
			g.FillRectangle (new SolidBrush (Color.Blue), new Rectangle (p2.X-3, p2.Y-3, 7, 7));
			//g.FillRectangle (new SolidBrush(Color.Green), new Rectangle (this.p1.X - 3, this.p1.Y -3, 7, 7));
			DrawPoint (g, this.p1, new SolidBrush (Color.Green));
		}
		public void DrawLine(Graphics g)
		{
			Pen newPen = new Pen (new SolidBrush (Color.FromArgb (rand.Next ())));
			this.DrawLine (g, newPen);
		}
		private bool TestAngleEquivalency(double angle1, double angle2)
		{
			double epsilonValue = 1e-15;
			if (Math.Abs (angle1 - angle2) < epsilonValue || Math.Abs (angle1 - ((angle2 + Math.PI) % (Math.PI * 2))) < epsilonValue) {
				return true;
			} else {
				return false;
			}
		}
		public void DrawPoint(Graphics g, Point point, SolidBrush brush)
		{
			Rectangle drawRect = new Rectangle (point.X-3, point.Y-3, 7, 7);
			g.FillRectangle(brush, drawRect);
		}
		public Point FindLineLineIntersection(Line line2)
		{
			double epsilonValue = 1e-15;
			Point retPoint = new Point (0, 0);
			if (line2 != this) {
				Console.Write ("\n");
				if (TestAngleEquivalency (this.Angle, line2.Angle)) {
					throw new System.ArgumentException ("Lines are parallel");
						
				} else {
					double numerator = this.AddComp - line2.AddComp;
					double denominator = line2.MultComp - this.MultComp;
					double sharedY = numerator / denominator;
					double sharedX = sharedY * this.MultComp + this.AddComp;
					retPoint.X = (int)sharedX;
					retPoint.Y = (int)sharedY;
				}
			} else {
				throw new System.ArgumentException ("Comparison line is equal to calling line");
			}
			return retPoint;
		}
	}


}

