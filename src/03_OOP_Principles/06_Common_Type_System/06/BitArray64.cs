using System.Collections;

namespace _06;

public class BitArray64 : IEnumerable<int>, IEquatable<BitArray64>
{
    private ulong _value;

    public BitArray64(ulong initialValue = 0)
    {
        _value = initialValue;
    }

    public int this[int index]
    {
        get
        {
            if (index < 0 || index > 63)
            {
                throw new IndexOutOfRangeException("Index must be between 0 and 63.");
            }

            return (int)((_value >> index) & 1UL);
        }
        set
        {
            if (index < 0 || index > 63)
            {
                throw new IndexOutOfRangeException("Index must be between 0 and 63.");
            }
            if (value != 0 && value != 1)
            {
                throw new ArgumentException("Bit value must be 0 or 1.");
            }

            if (value == 1)
            {
                _value |= (1UL << index);
            }
            else
            {
                _value &= ~(1UL << index);
            }
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < 64; i++)
        {
            yield return (int)((_value >> i) & 1UL);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object obj)
    {
        if (obj is BitArray64 other)
        {
            return Equals(other);
        }

        return false;
    }

    public bool Equals(BitArray64 other)
    {
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        return _value == other._value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public static bool operator ==(BitArray64 arr1, BitArray64 arr2)
    {
        if (ReferenceEquals(arr1, arr2))
        {
            return true;
        }

        if (ReferenceEquals(arr1, null) || ReferenceEquals(arr2, null))
        {
            return false;
        }

        return arr1.Equals(arr2);
    }

    public static bool operator !=(BitArray64 arr1, BitArray64 arr2)
    {
        return !(arr1 == arr2);
    }

    public override string ToString()
    {
        string bitString = string.Join("", this.Reverse());
        return $"Bits (MSB->LSB): {bitString}\nValue (ulong): {_value}";
    }
}