namespace TestTaskDCT.Models
{
    public class Market
    {
        public string ExchangeId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }
        public double? VolumeUsd24Hr { get; set; }
        public double? PriceUSD { get; set; }
        public double? VolumePercent { get; set; }
    }
}
