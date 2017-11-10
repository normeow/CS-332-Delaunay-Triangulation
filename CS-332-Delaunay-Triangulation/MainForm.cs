using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CS_332_Delaunay_Triangulation.Geometry;


namespace CS_332_Delaunay_Triangulation
{
    public partial class MainForm : Form
    {
        private List<PointF> points = new List<PointF>();
        private Triangulator triangulator = new Triangulator();
        private List<Triangulator.Triangle> triangulation;

        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PointF coordinates = ((MouseEventArgs)e).Location;
            var rightCoordinates = ImageCoordsToPoint(coordinates);
            points.Add(rightCoordinates);
            Console.WriteLine("Coords: {0}", rightCoordinates);
            triangulation = triangulator.Triangulate(points);
            
        
            PlotTriangulation();
        }

        private void ClearPictureBox()
        {
            var width = pictureBox1.Width;
            var height = pictureBox1.Height;
            pictureBox1.Image = new Bitmap(width, height);
        }

        private void PlotTriangulation()
        {
            ClearPictureBox();

            points.ForEach(point => PlotPoint(PointToImageCoords(point)));

            foreach (var triangle in triangulation)
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                g.DrawLine(Pens.Black, PointToImageCoords(triangle.A), PointToImageCoords(triangle.B));
                g.DrawLine(Pens.Black, PointToImageCoords(triangle.B), PointToImageCoords(triangle.C));
                g.DrawLine(Pens.Black, PointToImageCoords(triangle.C), PointToImageCoords(triangle.A));
                pictureBox1.Refresh();
            }
        }

        private PointF ImageCoordsToPoint(PointF coordinates)
        {
            return new PointF(coordinates.X, pictureBox1.Height - coordinates.Y);
        }

        private PointF PointToImageCoords(PointF point)
        {
            var height = pictureBox1.Height;
            return new PointF(point.X, height - point.Y);
        }

        private void PlotPoint(PointF point)
        {
            var pointRadius = 3;

            Graphics g = Graphics.FromImage(pictureBox1.Image);

            g.DrawEllipse(Pens.Red, point.X - pointRadius, point.Y - pointRadius, pointRadius*2, pointRadius*2);
            pictureBox1.Refresh();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearPictureBox();
            points = new List<PointF>();
        }
    }
}

