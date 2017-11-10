using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_332_Delaunay_Triangulation.Geometry;

namespace CS_332_Delaunay_Triangulation
{
    
class Triangulator
    {
        private List<PointF> points;
        public List<Triangle> Triangulate(List<PointF> points)
        {
            this.points = points;
            var res = new List<Triangle>();
            var frontier = new List<Edge> {FindFirstEdge()};

            while (frontier.Count != 0)
            {
                // find and exctract min edge
                var minEdge = frontier.Min();
                var point = FindMate(minEdge);
                if (point != null)
                {
                    // add new edges to frontier. But if frontier already contains it -- then delete. It's dead edge.
                    updateFrontier(frontier, (PointF)point, minEdge.Origin);
                    updateFrontier(frontier, minEdge.Destination, (PointF)point);
                    // add triangle in triangulaton result
                    res.Add(new Triangle(minEdge.Origin, minEdge.Destination, (PointF)point));
                }
                // delete edge from frontier
                frontier.Remove(minEdge);
            }
            return res;
        }

        private void updateFrontier(List<Edge> frontier, PointF a, PointF b)
        {
            var e = new Edge(a, b);
            if (frontier.Contains(e)) // doesnt work appropriate
            {
                frontier.Remove(e);
            }
            else
            {
                e.Flip();
                frontier.Add(e);
            }
        }

        /// <summary>
        /// Find first edge following to Jarvis gift-wrapping algorithm
        /// </summary>
        /// <returns></returns>
        private Edge FindFirstEdge()
        {
            if (points?.Count == 0)
                return null;

            PointF startPoint = points[0];

            // the most left point
            foreach (var point in points)
            {
                if (point.X < startPoint.X || (point.X == startPoint.X && point.Y < startPoint.Y))
                    startPoint = point;
            }

            // find the pint with max angle (min cos) 
            var minCos = 1.0;
            var endPoint = startPoint;
            
            var srtartVector = new PointF(0, startPoint.Y + 10);
            Console.WriteLine("StartVector: {0}", srtartVector);
            foreach (var point in points)
            {
                if (point == startPoint)
                    continue;

                Console.WriteLine("Current vec: {0}", point);
                var cos = CosBetween(srtartVector, new PointF(point.X - startPoint.X, point.Y - startPoint.Y));

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
        
        private double CosBetween(PointF a, PointF b)
        {
            var dotProduct = a.DotProduct(b);
            var lenA = a.RadiusVector();
            var lenB = b.RadiusVector();
            return dotProduct / (lenA * lenB);
        }

        private PointF? FindMate(Edge edge)
        {
            PointF? mate = null;
            // parameter for vectors intersection
            var t = Double.MaxValue;
            var bestT = t;
            // get normal to the edge
            Edge normal = new Edge(edge);
            normal.Rotate();
            foreach (var point in points)
            {
                if (IsRightPoint(edge, point))
                {
                    Edge g = new Edge(edge.Destination, point);
                    // normal to edge g
                    g.Rotate();
                    // find point of intersections 2 normals
                    t = -normal.Intersect(g); 
                    if (t < bestT)
                    {
                        bestT = t;
                        mate = point;
                    }
                }
            }

            return mate;
        }

        private bool IsRightPoint(Edge e, PointF point)
        {
            PointF a = e.Destination.Substract(e.Origin);
            PointF b = point.Substract(e.Origin);
            var sa = a.X * b.Y - b.X * a.Y;
            return sa > 0;
        }
       

        public class Triangle
        {
            public PointF A { get; private set; }
            public PointF B { get; private set; }
            public PointF C { get; private set; }

            public Triangle(PointF A, PointF B, PointF C)
            {
                this.A = A;
                this.B = B;
                this.C = C;
            }
        }
        

    }

}
