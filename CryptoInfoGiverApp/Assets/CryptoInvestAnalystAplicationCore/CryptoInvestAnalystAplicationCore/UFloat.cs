using System;

namespace CryptoInvestAnalystAplicationCore
{
    public class UFloat
    {
        public const float MaximalValue = float.MaxValue / 2;

        public readonly float Value;

        public UFloat(float value)
        {
            Value = Math.Min(Math.Max(value, 0), MaximalValue);
        }

        public static implicit operator UFloat(float value) => new UFloat(value);
        public static implicit operator float(UFloat value) => value.Value;
    }
}