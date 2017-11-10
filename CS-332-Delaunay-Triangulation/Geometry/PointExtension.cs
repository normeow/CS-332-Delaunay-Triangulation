using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CS_332_Delaunay_Triangulation.Geometry
{
    public static class PointExtension
    {
        public static bool LessThen(this PointF point, PointF other)
        {
            return RadiusVector(point) < RadiusVector(other);
        }

        public static bool GreaterThen(this PointF point, PointF other)
        {
            return RadiusVector(point) > RadiusVector(other);
        }

        public static double RadiusVector(this PointF point)
        {
            return Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
        }

        public static double DotProduct(this PointF a, PointF b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static PointF Substract(this PointF point, PointF other)
        {
            return new PointF(point.X - other.X, point.Y - other.Y);
        }

        public static PointF Add(this PointF point, PointF other)
        {
            return new PointF(point.X + other.X, point.Y + other.Y);
        }

        public static PointF Multiply(this PointF point, float x)
        {
            return new PointF(point.X * x, point.Y * x);
        }

    }

}
