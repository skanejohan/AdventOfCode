using System;
using System.Collections.Generic;

namespace Y2022.Day13
{
    internal interface IValue : IComparable<IValue>
    {
        public bool TryGetAsInt(out int i);
        public bool TryGetAsValues(out List<IValue> values);
    }

    internal class IntValue : IValue
    {
        public IntValue(int value)
        {
            this.value = value;
        }

        public bool TryGetAsInt(out int i)
        {
            i = value;
            return true;
        }

        public bool TryGetAsValues(out List<IValue> values)
        {
            values = null;
            return false;
        }

        public int CompareTo(IValue? other)
        {
            if (other.TryGetAsInt(out var v))
            {
                return value.CompareTo(v);
            }
            return new ListValue(value).CompareTo(other);
        }

        public override string ToString()
        {
            return $"{value}";
        }

        private readonly int value;
    }

    internal class ListValue : IValue
    {
        public ListValue()
        {
            values = new List<IValue>();
        }

        public ListValue(int value)
        {
            values = new List<IValue> { new IntValue(value) };
        }

        public void Add(IValue value)
        {
            values.Add(value);
        }

        public bool TryGetAsInt(out int i)
        {
            i = 0;
            return false;
        }

        public bool TryGetAsValues(out List<IValue> values)
        {
            values = this.values;
            return true;
        }

        public int CompareTo(IValue? other)
        {
            if (other.TryGetAsInt(out var v))
            {
                return CompareTo(new ListValue(v));
            }

            other.TryGetAsValues(out var otherValues);

            int i = 0;
            while(i <= values.Count && i <= otherValues.Count)
            {
                if (i == values.Count && i < otherValues.Count)
                {
                    return -1;
                }
                if (i == otherValues.Count && i < values.Count)
                {
                    return 1;
                }
                if (i == otherValues.Count && i == values.Count)
                {
                    return 0;
                }
                var comp = values[i].CompareTo(otherValues[i]);
                if (comp != 0)
                {
                    return comp;
                }
                i++;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"[{string.Join(',', values)}]";
        }

        private readonly List<IValue> values;
    }
}
