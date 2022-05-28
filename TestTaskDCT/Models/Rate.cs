namespace TestTaskDCT.Models
{
    public class Rate
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public char CurrencySymbol { get; set; }
        public string Type { get; set; }
        public double RateUSD { get; set; }
    }
}
