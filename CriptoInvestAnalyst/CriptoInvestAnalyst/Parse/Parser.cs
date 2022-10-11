using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CryptoInvestAnalyst
{
    public class Parser
    {
        public string GetRequest(string adderss, Dictionary<string, string> parameters)
        {
            HttpClient client = new HttpClient();

            try
            {
                Uri uri = new Uri(adderss);

                FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

                foreach(var kvp in (IEnumerable<KeyValuePair<string,string>>) parameters)
                    Console.WriteLine(kvp);

                var answer = client.PostAsync(adderss, content).Result.Content.ReadAsStringAsync().Result;

                return answer.ToString();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
