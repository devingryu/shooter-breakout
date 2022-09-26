using System;
namespace SBR
{
    public class XYZ
    {
        public int X;
        public int Y;
        public int Z;

        public XYZ(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public int this[int index]
        {
            get => index switch {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException()
            };
            set {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public XYZ copy(int? x = null, int? y = null, int? z = null)
            => new XYZ(x == null ? X : (int)x, y == null ? Y : (int)y, z == null ? Z : (int)z);
        public XYZ move(int x = 0, int y = 0, int z = 0)
            => new XYZ(X+x,Y+y,Z+z);
    }
}