using System;
using System.Collections;
using System.Collections.Generic;

namespace EpsilonEngine.EpsilonEngine.Core.Data_Types
{
    public sealed class QuedList<T> : IEnumerable<T>, IEnumerable, IEnumerator
    {
        private List<T> _baseCollection = new List<T>();
        private List<T> _addQueCollection = new List<T>();
        private List<int> _removeQueCollection = new List<int>();

        public object Current => throw new NotImplementedException();

        public void Add(T item)
        {
            foreach (T potentialDuplicate in _addQueCollection)
            {
                if (potentialDuplicate.Equals(item))
                {
                    throw new Exception("item could not be added because it is a duplicate.");
                }
            }
            _addQueCollection.Add(item);
        }
        public int Add(object value)
        {
            if (value is null && typeof(T).IsValueType)
            {
                throw new Exception("value cannot be null.");
            }
            if (!typeof(T).IsAssignableFrom(value.GetType()))
            {
                throw new Exception("value cannot be cast to T.");
            }
            _addQueCollection.Add((T)value);
            return -1;
        }
        public void Clear()
        {
            _removeQueCollection = new List<int>();
            for (int i = 0; i < _baseCollection.Count; i++)
            {
                _removeQueCollection.Add(i);
            }
        }
        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _baseCollection.GetEnumerator();
        }
        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _baseCollection.GetEnumerator();
        }
    }
}
