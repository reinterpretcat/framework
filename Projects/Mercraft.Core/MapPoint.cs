﻿using System;

namespace Mercraft.Core
{
    public struct MapPoint
    {
        public float X;
        public float Y;

        // TODO make it like point in 3D?
        public float Elevation;

        public MapPoint(float x, float y, float elevation) : this(x, y)
        {
            Elevation = elevation;
        }

        public MapPoint(float x, float y): this()
        {
            X = x;
            Y = y;
        }

        public float DistanceTo(MapPoint point)
        {
            return (float) Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2));

            // NOTE should we ignore elevation here?
            //+ Math.Pow(point.Elevation - Elevation, 2));
        }

        public float DistanceTo(float x, float y)
        {
            return (float)Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}):{2:F1}", X, Y, Elevation);
        }
    }
}
