using System;

namespace Main.Extension.Attributes
{
    [Serializable]
    public struct RangedFloat
    {
        public float Value;
        public float MinValue;
        public float MaxValue;

        public RangedFloat(float min, float max)
        {
            MinValue = min;
            MaxValue = max;
            Value = min;
        }

        public RangedFloat(float value, float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            if (value > maxValue)
                value = maxValue;
            if (value < minValue)
                value = minValue;
            Value = value;
        }
    }
}