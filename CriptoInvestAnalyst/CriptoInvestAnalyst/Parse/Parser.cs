using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CryptoInvestAnalyst
{
    public class Parser
    {
        public string GetRequest(string adderss, Dictionary<string, string> parameters, bool jsonParse = false)
        {
            HttpClient client = new HttpClient();

            try
            {
                Uri uri = new Uri(adderss);

                FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

                var answer = client.PostAsync(adderss, content).Result.Content.ReadAsStringAsync().Result;

                if (jsonParse)
                    return JsonConvert.DeserializeObject(answer).ToString();


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
