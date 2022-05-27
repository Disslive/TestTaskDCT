using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace TestTaskDCT.Services
{
    class Requests
    {
        protected string Get(string URL, List<Parameter> parameters = null)
        {
            var client = new RestClient(URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            if (parameters != null)
            {
               foreach(var param in parameters)
                {
                    request.AddParameter(param.Name, param.Value);
                }
            }
            IRestResponse response = client.Execute(request);
            JObject obj = JObject.Parse(response.Content);
            return obj["data"].ToString();
        }
    }
}
