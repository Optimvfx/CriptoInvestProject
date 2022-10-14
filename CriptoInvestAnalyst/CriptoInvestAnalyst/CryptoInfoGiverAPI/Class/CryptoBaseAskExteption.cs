using System;

namespace CryptoInfoGiverSpace
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
