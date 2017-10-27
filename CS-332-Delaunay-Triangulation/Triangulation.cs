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
            var frontier = new List<Edge>();
            frontier.Add(FindFirstEdge());
            
            while (frontier.Count != 0)
            {
                // find and exctract min edge
                var minEdge = frontier.Min();
                var point = FindMate(minEdge);
                if (point != null)
                {
                    // add new edges to frontier. But if frontier already contains it -- then delete. It's dead edge.

                    // add triangle in triangulaton result
                }
                // delete edge from frontier
                frontier.Remove(minEdge);
            }
            return res;
        }

        /// <summary>
        /// Find first edge following to Jarvis gift-wrapping algorithm
        /// </summary>
        /// <returns></returns>
        private Edge FindFirstEdge()
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

            return new Edge(startPoint, endPoint);


        }

        private double CosBetween(Point a, Point b)
        {
            var dotProduct = a.X * b.X + a.Y * b.Y;
            var lenA = a.RadiusVector();
            var lenB = b.RadiusVector();
            return dotProduct / (lenA * lenB);
        }

        private Point FindMate(Edge edge)
        {
            throw new NotImplementedException();
        }
       
        public class Edge: IComparable<Edge>
        {
            Point a;
            Point b;

            public Edge(Point a, Point b)
            {
                this.a = a;
                this.b = b;
            }

            public int CompareTo(Edge other)
            {
                if (a.LessThen(other.a)) return -1;
                if (a.GreaterThen(other.a)) return 1;
                if (b.LessThen(other.b)) return -1;
                if (b.GreaterThen(other.b)) return 1;
                return 0;
            }
        }

        public struct Triangle
        {
            private Point a, b, c;
        }
        

    }
}
