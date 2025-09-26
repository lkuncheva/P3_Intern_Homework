using System.Text;

namespace _04._02
{
    public class GenericList<T> where T : IComparable<T>
    {
        private T[] elements;
        private int count;
        private const int DEFAULT_CAPACITY = 4;

        public int Count
        {
            get { return this.count; }
        }

        public int Capacity
        {
            get { return this.elements.Length; }
        }

        public GenericList(int capacity = DEFAULT_CAPACITY)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("Capacity cannot be negative.");
            }

            this.elements = new T[capacity];
            this.count = 0;
        }

        public void Add(T element)
        {
            if (this.count >= this.elements.Length)
            {
                this.Grow();
            }

            this.elements[this.count] = element;
            this.count++;
        }

        public T Access(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new IndexOutOfRangeException("Index is outside the valid range of the list.");
            }

            return this.elements[index];
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.count)
            {
                throw new IndexOutOfRangeException("Index is outside the valid range of the list.");
            }

            for (int i = index; i < this.count - 1; i++)
            {
                this.elements[i] = this.elements[i + 1];
            }

            this.elements[this.count - 1] = default(T);
            this.count--;
        }

        public void InsertAt(T element, int index)
        {
            if (index < 0 || index > this.count)
            {
                throw new IndexOutOfRangeException("Index is outside the valid range for insertion.");
            }

            if (this.count >= this.elements.Length)
            {
                this.Grow();
            }

            for (int i = this.count; i > index; i--)
            {
                this.elements[i] = this.elements[i - 1];
            }

            this.elements[index] = element;
            this.count++;
        }

        public void Clear()
        {
            this.count = 0;

            for (int i = 0; i < this.elements.Length; i++)
            {
                this.elements[i] = default(T);
            }
        }

        public int FindByValue(T value)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (object.Equals(this.elements[i], value))
                {
                    return i;
                }
            }

            return -1;
        }

        public T Min()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("The list is empty. Cannot find the minimum element.");
            }

            T currentMin = this.elements[0];
            for (int i = 1; i < this.count; i++)
            {
                if (currentMin.CompareTo(this.elements[i]) > 0)
                {
                    currentMin = this.elements[i];
                }
            }

            return currentMin;
        }

        public T Max()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("The list is empty. Cannot find the maximum element.");
            }

            T currentMax = this.elements[0];
            for (int i = 1; i < this.count; i++)
            {
                if (currentMax.CompareTo(this.elements[i]) < 0)
                {
                    currentMax = this.elements[i];
                }
            }

            return currentMax;
        }

        public override string ToString()
        {
            if (this.count == 0)
            {
                return "GenericList<T> is empty.";
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("List elements: [");

            for (int i = 0; i < this.count; i++)
            {
                sb.Append(this.elements[i].ToString());
                if (i < this.count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("]");

            return sb.ToString();
        }

        private void Grow()
        {
            int newCapacity = this.elements.Length == 0 ? DEFAULT_CAPACITY : this.elements.Length * 2;

            T[] newElements = new T[newCapacity];

            Array.Copy(this.elements, newElements, this.count);

            this.elements = newElements;

            Console.WriteLine($"\n--> List grew! New capacity: {this.elements.Length}");
        }
    }
}