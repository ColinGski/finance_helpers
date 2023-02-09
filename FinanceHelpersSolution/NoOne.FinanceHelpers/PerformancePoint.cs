namespace NoOne.FinanceHelpers
{
    public class PerformancePoint
    {
        public IPortfolioValuation Valuation { get; set; }

        public decimal AccumulatedCashFlow { get; set; }

        public decimal Performance { get; set; }
    }
}