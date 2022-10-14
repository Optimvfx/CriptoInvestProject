namespace CryptoInvestAnalystAplicationCore
{
    public struct Procent
    {
        public const float MaximalProcent = 100;

        public readonly float Value;

        public Procent(float value)
        {
            Value = value % MaximalProcent;
        }

        public static implicit operator Procent (float value) => new Procent (value);
        public static implicit operator float (Procent value) => value.Value;
    }
}