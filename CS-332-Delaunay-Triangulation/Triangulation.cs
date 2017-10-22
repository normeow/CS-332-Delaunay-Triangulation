using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_332_Delaunay_Triangulation
{
    class Triangulation
    {
        private List<Point> points;
        public List<Tuple<Point, Point>> Triangulate(List<Point> points)
        {
            this.points = points;
            var res = new List<Tuple<Point, Point>>();
            return res;
        }

        private Tuple<Point, Point> FindFirstEdge()
        {
            if (points?.Count == 0)
                return null;

            Point startPoint = points[0];

            // the most left point
            foreach (var point in points)
            {
                if (point.X < startPoint.X || (point.X == startPoint.X && point.Y < startPoint.Y))
                    startPoint = point;
            }

            // find the pint with max angle (min cos) 
            var minCos = 1.0;
            var endPoint = startPoint;
            
            var srtartVector = new Point(0, startPoint.Y + 10);
            Console.WriteLine("StartVector: {0}", srtartVector);
            foreach (var point in points)
            {
                if (point == startPoint)
                    continue;

                Console.WriteLine("Current vec: {0}", point);
                var cos = CosBetween(srtartVector, new Point(point.X - startPoint.X, point.Y - startPoint.Y));

                Console.WriteLine("Cos: {0}", cos);
                if (cos < minCos)
                {
                    minCos = cos;
                    endPoint = point;
                }
                Console.WriteLine();

            }

            return new Tuple<Point, Point>(startPoint, endPoint);


        }

        private double CosBetween(Point a, Point b)
        {
            var dotProduct = a.X * b.X + a.Y * b.Y;
            var lenA = Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));
            var lenB = Math.Sqrt(Math.Pow(b.X, 2) + Math.Pow(b.Y, 2));
            return dotProduct / (lenA * lenB);
        }

        


    }
}
