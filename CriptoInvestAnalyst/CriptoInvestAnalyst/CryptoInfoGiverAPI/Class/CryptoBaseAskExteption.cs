using System;

namespace CryptoInvestAnalyst
{
    public class CryptoBaseAskExteption : Exception
    {
        public readonly Exception Exception;

        public CryptoBaseAskExteption(Exception exception)
        {
            Exception = exception;
        }
    }
}
