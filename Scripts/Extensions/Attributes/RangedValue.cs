using System;

namespace Main.Extension.Attributes
{
    [Serializable]
    public struct RangedValue<T> where T : struct, IComparable<T>
    {
        public T value;
        public T min;
        public T max;

        public RangedValue(T min, T max, T value)
        {
            this.min = min;
            this.max = max;
            this.value = Clamp(value, min, max);
        }

        public static T Clamp(T val, T min, T max) =>
            val.CompareTo(min) < 0 ? min : val.CompareTo(max) > 0 ? max : val;
    }
}