using TestTaskDCT.Models;

namespace TestTaskDCT.Services
{
    class Calculations
    {
        public double ConvertCurrency(string startCurrencyId, string endCurrencyId, double startCurrencyAmount)
        {
            Requests requests = new Requests();
            Rate startCurrencyRate = requests.GetRate(startCurrencyId);
            Rate endCurrencyRate = requests.GetRate(endCurrencyId);
            if(startCurrencyRate!=null && endCurrencyRate != null)
            {
                double USD = startCurrencyAmount * startCurrencyRate.RateUSD;
                return USD / endCurrencyRate.RateUSD;
            }
            return 0;
        }
    }
}
