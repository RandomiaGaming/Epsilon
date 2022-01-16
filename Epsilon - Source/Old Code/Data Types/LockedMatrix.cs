using System;

namespace EpsilonEngine
{
    public class LockedMatrix<T> where T : new()
    {
        public readonly int sizeX = 0;
        public readonly int sizeY = 0;
        private T[] buffer = new T[0];
        public LockedMatrix(int sizeX, int sizeY)
        {
            if (sizeX <= 0 || sizeY <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            buffer = new T[sizeX * sizeY];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = default(T);
            }
        }
        public LockedMatrix(int sizeX, int sizeY, T fillElement)
        {
            if (sizeX <= 0 || sizeY <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            buffer = new T[sizeX * sizeY];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = fillElement;
            }
        }
        public LockedMatrix(int sizeX, int sixeY, T[] buffer)
        {
            if (sizeX <= 0 || sixeY <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.sizeX = sizeX;
            this.sizeY = sixeY;
            if (buffer is null)
            {
                throw new NullReferenceException();
            }
            if (buffer.Length != sizeX * sixeY)
            {
                throw new ArgumentException();
            }
            this.buffer = (T[])buffer.Clone();
        }

        public void SetElementUnsafe(int x, int y, T newElement)
        {
            buffer[(y * sizeX) + x] = newElement;
        }
        public T GetElementUnsafe(int x, int y)
        {
            return buffer[(y * sizeX) + x];
        }

        public void SetElement(int x, int y, T newElement)
        {
            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
            {
                throw new ArgumentOutOfRangeException();
            }
            buffer[(y * sizeX) + x] = newElement;
        }
        public T GetElement(int x, int y)
        {
            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
            {
                throw new ArgumentOutOfRangeException();
            }
            return buffer[(y * sizeX) + x];
        }

        public void SetBuffer(T[] newBuffer)
        {
            if (newBuffer.Length != sizeX * sizeY)
            {
                throw new ArgumentException();
            }
            buffer = (T[])newBuffer.Clone();
        }
        public T[] GetBuffer()
        {
            return (T[])buffer.Clone();
        }
    }
}
