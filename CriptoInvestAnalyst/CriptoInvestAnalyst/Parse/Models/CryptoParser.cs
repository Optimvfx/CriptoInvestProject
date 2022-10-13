using Extenstions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoInvestAnalyst
{
    public class CryptoParser
    {
        private readonly List<char> _spleatChar = new List<char>(new[] { '{', '}', '[', ']' });
        private readonly List<char> _removeChar = new List<char>(new[] { ','});

        private readonly ICryptoInfoGiver _cryptoInfoGiver;

        public CryptoParser(ICryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
        }

        public  List<string> Parse(string from)
        {
            var result = new List<string>();

            from = from.Replace(" ", "").Replace("\n", "").Replace("\t", " ").Replace(((char)(13)).ToString(), "");

            for (int i = 0; i < from.Length; i++)
            {
                var currentString = new StringBuilder();

                while ((_spleatChar.Contains(from[i]) || _removeChar.Contains(from[i])) == false && i < from.Length)
                {
                    currentString.Append(from[i]);
                    i++;
                }

                if (currentString.Length > 0)
                    result.Add(currentString.ToString());

                if (_spleatChar.Contains(from[i]))
                    result.Add(from[i].ToString());
            }

            return result;
        }

        public double ReadPrice(string parsedAnswerPriceInfo, Crypto comperable)
        {
            const int _compareResultOffsetX = 4;

            try
            {
                StringBuilder compareResult = new StringBuilder();

                for (int i = _compareResultOffsetX + _cryptoInfoGiver.CryptoToApiName(comperable).Length - 1; i < parsedAnswerPriceInfo.Length; i++)
                {
                    if (parsedAnswerPriceInfo[i].IsMatch() == false)
                        break;

                    if (parsedAnswerPriceInfo[i] == CryptoInfoGiver.ApiFloatDotChar)
                        compareResult.Append(CryptoInfoGiver.FloatDotChar);
                    else
                        compareResult.Append(parsedAnswerPriceInfo[i]);
                }

                return double.Parse(compareResult.ToString());
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }
    }
}
