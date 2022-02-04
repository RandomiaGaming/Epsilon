using System;
namespace EpsilonEngine
{
    public struct Bounds
    {
        #region Constants
        public static readonly Bounds One = new Bounds(0, 0, 1, 1);
        #endregion
        #region Variables
        private float _minX;
        private float _minY;
        private float _maxX;
        private float _maxY;
        #endregion
        #region Properties
        public float MinX
        {
            get
            {
                return _minX;
            }
        }
        public float MinY
        {
            get
            {
                return _minY;
            }
        }
        public float MaxX
        {
            get
            {
                return _maxX;
            }
        }
        public float MaxY
        {
            get
            {
                return _maxY;
            }
        }
        public Vector Min
        {
            get
            {
                return new Vector(_minX, _minY);
            }
        }
        public Vector Max
        {
            get
            {
                return new Vector(_maxX, _maxY);
            }
        }
        public float Width
        {
            get
            {
                return _maxX - _minX + 1;
            }
        }
        public float Height
        {
            get
            {
                return _maxY - _minY + 1;
            }
        }
        public Vector Size
        {
            get
            {
                return new Vector(_maxX - _minX + 1f, _maxY - _minY + 1f);
            }
        }
        #endregion
        #region Constructors
        public Bounds(Vector min, Vector max)
        {
            if (min.X > max.X || min.Y > max.Y)
            {
                throw new Exception("Max must be greater than Min.");
            }
            _minX = min.X;
            _minY = min.Y;
            _maxX = max.X;
            _maxY = max.Y;
        }
        public Bounds(float minX, float minY, float maxX, float maxY)
        {
            if (minX > maxX)
            {
                throw new Exception("MaxX must be greater than MinX.");
            }
            if (minY > maxY)
            {
                throw new Exception("MaxY must be greater than MinY.");
            }
            _minX = minX;
            _minY = minY;
            _maxX = maxX;
            _maxY = maxY;
        }
        public Bounds(Microsoft.Xna.Framework.BoundingBox source)
        {
            _minX = source.Min.X;
            _minY = source.Min.Y;
            _maxX = source.Max.X;
            _maxY = source.Max.Y;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Bounds({_minX}, {_minY}, {_maxX}, {_maxY})";
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Bounds))
            {
                return false;
            }
            else
            {
                return this == (Bounds)obj;
            }
        }
        public static bool operator ==(Bounds a, Bounds b)
        {
            return (a._minX == b._minX) && (a._minY == b._minY) && (a._maxX == b._maxX) && (a._maxY == b._maxY);
        }
        public static bool operator !=(Bounds a, Bounds b)
        {
            return (a._minX != b._minX) || (a._minY != b._minY) || (a._maxX != b._maxX) || (a._maxY != b._maxY);
        }
        #endregion
        #region Methods
        public static bool Incapsulates(Bounds a, Vector b)
        {
            if (b.X >= a._minX && b.X <= a._maxX && b.Y >= a._minY && b.Y <= a._maxY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Incapsulates(Vector a)
        {
            return Incapsulates(this, a);
        }
        public static bool Incapsulates(Bounds a, Bounds b)
        {
            if (b._maxY <= a._maxY && b._minY >= a._minY && b._maxX <= a._maxX && b._minX >= a._minX)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Incapsulates(Bounds a)
        {
            return Incapsulates(this, a);
        }
        public static bool Overlaps(Bounds a, Bounds b)
        {
            if (a._maxX < b._minX || a._minX > b._maxX || a._maxY < b._minY || a._minY > b._maxY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Overlaps(Bounds a)
        {
            return Overlaps(this, a);
        }
        public static Microsoft.Xna.Framework.BoundingBox ToXNA(Bounds source)
        {
            return new Microsoft.Xna.Framework.BoundingBox(new Microsoft.Xna.Framework.Vector3(source._minX, source._minY, 0f), new Microsoft.Xna.Framework.Vector3(source._maxX, source._maxY, 0f));
        }
        public Microsoft.Xna.Framework.BoundingBox ToXNA()
        {
            return ToXNA(this);
        }
        public static Bounds FromXNA(Microsoft.Xna.Framework.BoundingBox source)
        {
            return new Bounds(source);
        }
        #endregion
    }
}