﻿using System;
using ActionStreetMap.Core;

namespace ActionStreetMap.Osm.Index.Spatial
{
    public interface IEnvelop
    {
        int MinPointLatitude { get; }
        int MinPointLongitude { get; }
        int MaxPointLatitude { get; }
        int MaxPointLongitude { get; }

        long Area { get; }
        long Margin { get; }

        void Extend(IEnvelop by);
        void Extend(int scaledLatitude, int scaledLongitude);

        bool Intersects(IEnvelop b);
        bool Contains(IEnvelop b);
    }

    [Serializable]
    public class Envelop : IEnvelop
    {
        public int MinPointLatitude { get; set; }
        public int MinPointLongitude { get; set; }
        public int MaxPointLatitude { get; set; }
        public int MaxPointLongitude { get; set; }

        public Envelop() : this(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue) { }

        public Envelop(int minPointLatitude, int minPointLongitude,
            int maxPointLatitude, int maxPointLongitude)
        {
            MinPointLatitude = minPointLatitude;
            MinPointLongitude = minPointLongitude;
            MaxPointLatitude = maxPointLatitude;
            MaxPointLongitude = maxPointLongitude;
        }

        public Envelop(GeoCoordinate minPoint, GeoCoordinate maxPoint)
        {
            MinPointLatitude = (int)(minPoint.Latitude * Utils.ScaleFactor);
            MinPointLongitude = (int)(minPoint.Longitude * Utils.ScaleFactor);

            MaxPointLatitude = (int)(maxPoint.Latitude * Utils.ScaleFactor);
            MaxPointLongitude = (int)(maxPoint.Longitude * Utils.ScaleFactor);
        }

        public long Area { get { return ((long)(MaxPointLongitude - MinPointLongitude)) * (MaxPointLatitude - MinPointLatitude); } }
        public long Margin { get { return ((long)(MaxPointLongitude - MinPointLongitude)) + (MaxPointLatitude - MinPointLatitude); } }

        public void Extend(IEnvelop by)
        {
            MinPointLatitude = Math.Min(MinPointLatitude, by.MinPointLatitude);
            MinPointLongitude = Math.Min(MinPointLongitude, by.MinPointLongitude);

            MaxPointLatitude = Math.Max(MaxPointLatitude, by.MaxPointLatitude);
            MaxPointLongitude = Math.Max(MaxPointLongitude, by.MaxPointLongitude);
        }

        public void Extend(int scaledLatitude, int scaleddLongitude)
        {
            MinPointLatitude = Math.Min(MinPointLatitude, scaledLatitude);
            MinPointLongitude = Math.Min(MinPointLongitude, scaleddLongitude);

            MaxPointLatitude = Math.Max(MaxPointLatitude, scaledLatitude);
            MaxPointLongitude = Math.Max(MaxPointLongitude, scaleddLongitude);
        }

        public bool Intersects(IEnvelop b)
        {
            return b.MinPointLongitude <= MaxPointLongitude &&
                   b.MinPointLatitude <= MaxPointLatitude &&
                   b.MaxPointLongitude >= MinPointLongitude &&
                   b.MaxPointLatitude >= MinPointLatitude;
        }

        public bool Contains(IEnvelop b)
        {
            return MinPointLongitude <= b.MinPointLongitude &&
                   MinPointLatitude <= b.MinPointLatitude &&
                   b.MaxPointLongitude <= MaxPointLongitude &&
                   b.MaxPointLatitude <= MaxPointLatitude;
        }
    }

    [Serializable]
    public class PointEnvelop : IEnvelop
    {
        private readonly int _pointLatitude;
        private readonly int _pointLongitude;

        public PointEnvelop(int pointLatitude, int pointLongitude)
        {
            _pointLatitude = pointLatitude;
            _pointLongitude = pointLongitude;
        }

        public PointEnvelop(GeoCoordinate point)
        {
            _pointLatitude = (int)(point.Latitude * Utils.ScaleFactor);
            _pointLongitude = (int)(point.Longitude * Utils.ScaleFactor);
        }

        public int MinPointLatitude { get { return _pointLatitude; } }
        public int MinPointLongitude { get { return _pointLongitude; } }
        public int MaxPointLatitude { get { return _pointLatitude; } }
        public int MaxPointLongitude { get { return _pointLongitude; } }

        public long Area { get { return 0; } }
        public long Margin { get { return 0; } }

        public void Extend(IEnvelop @by)
        {
            throw new NotImplementedException();
        }

        public void Extend(int scaledLatitude, int scaledLongitude)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(IEnvelop b)
        {
            throw new NotImplementedException();
        }

        public bool Contains(IEnvelop b)
        {
            throw new NotImplementedException();
        }
    }
}