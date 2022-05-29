using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using TestTaskDCT.Models;
using System.Collections.ObjectModel;

namespace TestTaskDCT.Services
{
    public class Requests
    {

        // assets: "assets"
        // markets: "assets/{assetId}/markets"
        // rates: "rates/{assetId}"
        // graphPoints: "assets/{assetId}/history?interval={interval}"

        public ObservableCollection<GraphPoint> GetPoints(string assetId, string interval, List<RequestParameter> parameters = null)
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
            ObservableCollection<GraphPoint> points = JsonConvert.DeserializeObject<ObservableCollection<GraphPoint>>(responseData);
            return points;
        }

        public Rate GetRates(string assetId)
        {
            string URL = $"rates/{assetId}";
            string responseData;
            responseData = Get(URL);   
            Rate rate = JsonConvert.DeserializeObject<Rate>(responseData);
            return rate;
        }

        public ObservableCollection<Market> GetMarketsData(string assetId, List<RequestParameter> parameters = null)
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
            ObservableCollection<Market> markets = JsonConvert.DeserializeObject<ObservableCollection<Market>>(responseData);
            return markets;
        }

        public ObservableCollection<Asset> GetAssetsData(List<RequestParameter> parameters = null)
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
            ObservableCollection<Asset> assets = JsonConvert.DeserializeObject<ObservableCollection<Asset>>(responseData);
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
