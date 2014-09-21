﻿using Mercraft.Models.Utils;
using Mercraft.Models.Utils.Geometry;

namespace Mercraft.Models.Roads
{
    public class RoadSegment
    {
        public Segment Left;
        public Segment Right;
        
        public RoadSegment(Segment left, Segment right)
        {
            Left = left;
            Right = right;
        }
    }
}