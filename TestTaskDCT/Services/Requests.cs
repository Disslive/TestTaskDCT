using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using TestTaskDCT.Models;

namespace TestTaskDCT.Services
{
    class Requests
    {

        // assets: "assets"
        // markets: "assets/{assetId}/markets"
        // rates: "rates/{assetId}"
        // graphPoints: "assets/{assetId}/history?interval={interval}"

        List<GraphPoint> GetPoints(string assetId, string interval, List<RequestParameter> parameters = null)
        {
            string URL = $"assets/{assetId}/history?interval={interval}";
            string responseData;
            if (parameters == null)
            {
                responseData = Get(URL);
            }
            else
            {
                responseData = Get(URL, parameters);
            }
            List<GraphPoint> points = JsonConvert.DeserializeObject<List<GraphPoint>>(responseData);
            return points;
        }

        List<Rate> GetRates(string assetId, List<RequestParameter> parameters = null)
        {
            string URL = $"rates/{assetId}";
            string responseData;
            if (parameters == null)
            {
                responseData = Get(URL);
            }
            else
            {
                responseData = Get(URL, parameters);
            }
            List<Rate> rates = JsonConvert.DeserializeObject<List<Rate>>(responseData);
            return rates;
        }

        List<Market> GetMarketsData(string assetId, List<RequestParameter> parameters = null)
        {
            string URL = $"assets/{assetId}/markets";
            string responseData;
            if(parameters == null)
            {
                responseData = Get(URL);
            }
            else
            {
                responseData = Get(URL, parameters);
            }
            List<Market> markets = JsonConvert.DeserializeObject<List<Market>>(responseData);
            return markets;
        }

        List<Asset> GetAssetsData(List<RequestParameter> parameters = null)
        {
            string URL = "assets";
            string responseData;
            if(parameters == null)
            {
                responseData = Get(URL);
            }
            else
            {
                responseData = Get(URL, parameters);
            }
            List<Asset> assets = JsonConvert.DeserializeObject<List<Asset>>(responseData);
            return assets;
        }


        protected string Get(string URL, List<RequestParameter> parameters = null)
        {
            string BaseURL = "https://api.coincap.io/v2/";
            URL = BaseURL + URL;
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
