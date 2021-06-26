using System.Runtime.Serialization;
using UnityEngine;

namespace ANTs.Template
{
    [System.Serializable]
    public class SerializableMask
    {
        [SerializeField]
        bool[] table;

        [SerializeField]
        Vector2Int size;

        public SerializableMask(Vector2Int size)
        {
            this.size = size;
            table = new bool[size.x * size.y];
        }

        public bool this[int i, int j]
        {
            get
            {
                CheckBound(i, j);
                return table[i * size.x + j];
            }
            set
            {
                CheckBound(i, j);
                table[i * size.x + j] = value;
            }
        }

        private void CheckBound(int i, int j)
        {
            if (i < 0 || i >= size.x || j < 0 || j >= size.y)
                throw new UnityException("Array out of bound");
        }
    }
}