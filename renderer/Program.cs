using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using renderer;

public class MainForm : System.Windows.Forms.Form
{
	private System.ComponentModel.IContainer components;
	private List<Point> grid = new List<Point>();
	private int increment = 0;
	private Point lastClick = new Point ();
	private int dimension = 15;
	private Point testLineRoot;
	private Line testLine1;
	private Random rand = new Random ();

	private Point testLine2Root;
	private Line testLine2;


	private List<renderer.Node> nodeList = new List<renderer.Node>();

	public MainForm()
	{
		InitializeComponent();
		PopulateGrid (grid);

		testLineRoot = new Point (200, 200);
		testLine2Root = new Point (400, 400);
		testLine1 = new Line (testLineRoot, Math.PI/3, this);
		testLine2 = new Line (testLine2Root, Math.PI / 3 * 4, this);

	}


	private void PopulateGrid(List<Point> gridList)
	{
		for (int i = 0; i < dimension; i++)
		{
			for (int o = 0; o < dimension; o++)
			{
				Point newPoint = new Point ();
				newPoint.X = i;
				newPoint.Y = o;
				gridList.Add (newPoint);

			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container ();
		this.timer = new System.Windows.Forms.Timer (this.components);
		this.timer.Enabled = true;
		this.timer.Interval = 100;
		this.timer.Tick += new System.EventHandler (this.timer_Tick);

		this.MouseDown += new System.Windows.Forms.MouseEventHandler (this.Form_MouseDown);

		this.AutoScaleBaseSize = new System.Drawing.Size (6, 15);
		this.ClientSize = new System.Drawing.Size(640,480);
		this.Name = "MainForm";
		this.Text = "Animation Demo";
		this.Load += new System.EventHandler(this.MainForm_Load);
		this.Paint += new System.Windows.Forms.PaintEventHandler (this.MainForm_Paint);
	}




	static void Main(){
		Application.Run(new MainForm());
	}

	private System.Windows.Forms.Timer timer;

	private void MainForm_Load(object sender, System.EventArgs e)
	{
		this.BackColor = Color.FromArgb (255, 255, 255, 255);
	}

	private void Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
	{
		lastClick.X = e.X;
		lastClick.Y = e.Y;
		Console.Write (String.Concat(String.Concat(String.Concat(lastClick.X, ", "), lastClick.Y),"\n"));
		//CreateNode (e);
		this.timer.Enabled = !this.timer.Enabled;
	}


	private void CycleIncrement ()
	{
		if (increment < dimension*2) {
			increment = increment + 1;
		} 
		else 
		{
			increment = 0;
		}
	}



	private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
	{
		UpdateGraphics (CreateGraphics());
	}

	private void timer_Tick(object sender, System.EventArgs e)
	{
		UpdateGraphics (CreateGraphics());
		//CycleIncrement ();
	}

	private List<Point> CreateMainArea(MainForm rootForm)
	{
		List<Point> retList = new List<Point>();
		Point centre;
		centre = new Point (rootForm.Width / 2, rootForm.Height / 2);
		retList.Add (centre);
		return retList;

	}

	private renderer.Polygon CreateTestPolygon()
	{
		List<renderer.Point3> coordList = new List<renderer.Point3>();
		coordList.Add (new renderer.Point3 (5, 5, 0));
		coordList.Add (new renderer.Point3 (5, 25, 0));
		coordList.Add (new renderer.Point3 (25, 25, 0));
		coordList.Add (new renderer.Point3 (25, 5, 0));
		return new renderer.Polygon (coordList);
	}

	private void LineTest(Line l, Graphics g, double rotationRate)
	{
		Pen testPen = new Pen (Color.Red);
		l.Angle += rotationRate;
		l.Angle = l.Angle % (Math.PI * 2);
		l.DrawLine (g, testPen);

	}

	private void ToggleTimer()
	{
	}

	private void UpdateGraphics (Graphics g)
	{
		g.Clear(Color.FromArgb (255, 255, 255, 255));
		g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		Pen p = new Pen (new SolidBrush (Color.Black));
		SolidBrush r = new SolidBrush (Color.Red);
		renderer.Polygon demoObject = CreateTestPolygon ();
		demoObject.Draw2D (p, g);
		LineTest (testLine1, g, 0);
		LineTest (testLine2, g, Math.PI/100);
		try
		{
			Console.Write(testLine1.FindLineLineIntersection (testLine2));
			testLine1.DrawPoint(g, testLine1.FindLineLineIntersection (testLine2), new SolidBrush(Color.Purple));
		}
		catch (ArgumentException e) {
			Console.WriteLine ("Invalid arguments");
		}

		foreach (Node element in nodeList) 
		{
			element.Draw(g);
		}
	}

	private void CreateNode(System.Windows.Forms.MouseEventArgs e)
	{
		if (nodeList.Count == 0) {
			this.nodeList.Add (new Node (e.X, e.Y, this));
		} else {
			this.nodeList.Add (new Node (e.X, e.Y, this));
		}

	}
}

