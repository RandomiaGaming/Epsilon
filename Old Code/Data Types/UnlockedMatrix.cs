using System.Collections.Generic;

namespace EpsilonEngine
{
    public class UnlockedMatrix<T> where T : new()
    {
        private struct ElementLocationPair
        {
            public T element;
            public int x;
            public int y;
            public ElementLocationPair(T element, int x, int y)
            {
                this.element = element;
                this.x = x;
                this.y = y;
            }
        }
        private List<ElementLocationPair> buffer = new List<ElementLocationPair>();
        public UnlockedMatrix()
        {
            buffer = new List<ElementLocationPair>();
        }

        public void SetElement(int x, int y, T newElement)
        {
            for (int i = 0; i < buffer.Count; i++)
            {
                if (buffer[i].x == x && buffer[i].y == y)
                {
                    buffer.RemoveAt(i);
                    i--;
                }
            }

            if (newElement is not null)
            {
                buffer.Add(new ElementLocationPair(newElement, x, y));
            }
        }
        public T GetElement(int x, int y)
        {
            for (int i = 0; i < buffer.Count; i++)
            {
                if (buffer[i].x == x && buffer[i].y == y)
                {
                    return buffer[i].element;
                }
            }

            return default(T);
        }

        public void ClearBuffer()
        {
            buffer = new List<ElementLocationPair>();
        }
    }
}
