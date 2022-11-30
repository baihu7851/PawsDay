using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace PawsDay.Services.LineBot
{
    public class LineBotQnAService
    {
        public string Endpoint { get; set; }
        public string SubscriptionKey { get; set; }

        public LineBotQnAService(Uri Endpoint, string EndpointKey)
        {
            this.Endpoint = Endpoint.ToString();
            this.SubscriptionKey = EndpointKey;
        }


        public response GetAnswer(string query)
        {
            //傳入query字串，去azure搜尋相關答案
            try
            {
                var res = new response();
                query = query.Trim();

                WebClient wc = new WebClient();
                wc.Headers.Clear();
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                wc.Headers.Add("Authorization", $"EndpointKey {SubscriptionKey}");

                string JSON = "{'question':'" + query + "'}";
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(JSON);
                byte[] result = wc.UploadData(Endpoint, byteArray);
                var ret = System.Text.Encoding.UTF8.GetString(result);

                res = Newtonsoft.Json.JsonConvert.DeserializeObject<response>(ret);
               

                return res;
            }
            catch (WebException ex)
            {
                string responseString;
                using (Stream stream = ex.Response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
                throw new Exception(responseString, ex);
            }



        }

        public class response
        {
            
            public List<qnresponse> answers { get; set; }

    }

        public class qnresponse
        {
            public List<string> questions { get; set; }
            public string answer { get; set; }

        }
            
    }
}
