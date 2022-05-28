using TestTaskDCT.Models;

namespace TestTaskDCT.Services
{
    class Calculations
    {
        public double ConvertCurrency(string startCurrencyId, string endCurrencyId, double startCurrencyAmount)
        {
            Requests requests = new Requests();
            Rate startCurrencyRate = requests.GetRates(startCurrencyId);
            Rate endCurrencyRate = requests.GetRates(endCurrencyId);
            double USD = startCurrencyAmount * startCurrencyRate.RateUSD;
            return USD / endCurrencyRate.RateUSD;
        }
    }
}
