using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_332_Delaunay_Triangulation.Geometry
{
    public class Edge : IComparable<Edge>
    {
        public PointF Origin { get; private set; }
        public PointF Destination { get; private set; }

        public Edge(PointF origin, PointF destination)
        {
            this.Origin = origin;
            this.Destination = destination;
        }

        public int CompareTo(Edge other)
        {
            if (Origin.LessThen(other.Origin)) return -1;
            if (Origin.GreaterThen(other.Origin)) return 1;
            if (Destination.LessThen(other.Destination)) return -1;
            if (Destination.GreaterThen(other.Destination)) return 1;
            return 0;
        }

        public void Rotate()
        {
            // rotate on 90
            var mid = (Origin.Add(Destination)).Multiply(0.5f);
            var v = Destination.Substract(Origin);
            var n = new PointF(v.Y, -v.X);
            Origin = mid.Substract(n.Multiply(0.5f));
            Destination = mid.Add(n.Multiply(0.5f));
        }

        public void Flip()
        {
            Rotate();
            Rotate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns>paramter of intersection equation. NaN if no intersection</returns>
        public double Intersect(Edge other)
        {
            var t = Double.NaN;
            PointF a = Origin;
            PointF b = Destination;
            PointF c = other.Origin;
            PointF d = other.Destination;
            PointF n = new PointF(d.Substract(c).Y, c.Substract(d).X);

            var denom = n.DotProduct(b.Substract(a));
            if (denom != .0)
            {
                var num = n.DotProduct(a.Substract(c));
                t = -num / denom;
            }

            return t;
        }
    }
}
