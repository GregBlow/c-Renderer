using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Collections;
using System.Collections.Generic;

namespace renderer
{
	public class Polygon
	{
		List<Point3> coordinateList;
		List<Point> list2D;

		public Polygon (List<renderer.Point3> coordinateList)
		{
			this.coordinateList = coordinateList;
			this.list2D = StripZ (this.coordinateList);
		}

		private List<Point> StripZ(List<Point3> list3)
		{
			List<Point> retList = new List<Point>();
			foreach (renderer.Point3 element in list3) 
			{
				retList.Add(element.StripZ());
			}

			return retList;

		}


		public void Draw2D(Pen p, Graphics g)
		{
			foreach (Point element in this.list2D) {
				g.DrawLine (p, element, list2D[(list2D.IndexOf(element)+1)%list2D.Count]);
			}

		}
	}


}

