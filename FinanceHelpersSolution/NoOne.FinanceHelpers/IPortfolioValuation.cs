namespace NoOne.FinanceHelpers
{
    public interface IPortfolioValuation
    {
        public DateTime ValuationDate { get; }

        public decimal PortfolioValue { get; }
    }      
}