﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CS_332_Delaunay_Triangulation
{
    public static class PointExtension
    {
        public static bool LessThen(this Point point, Point other)
        {
            return RadiusVector(point) < RadiusVector(other);
        }

        public static bool GreaterThen(this Point point, Point other)
        {
            return RadiusVector(point) > RadiusVector(other);
        }

        public static double RadiusVector(this Point point)
        {
            return Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
        }
    }
}
