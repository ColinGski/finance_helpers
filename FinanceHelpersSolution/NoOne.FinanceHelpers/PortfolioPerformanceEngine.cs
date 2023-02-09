namespace NoOne.FinanceHelpers
{
    public class PortfolioPerformanceEngine
    {
        public IEnumerable<PerformancePoint> ComputePortfolioPerformance(IEnumerable<IPortfolioValuation> historicalValuations, IEnumerable<ICashFlowOperation> cashflows)
        {
            var orderedValuations = (historicalValuations ?? new List<IPortfolioValuation>()).OrderBy(i => i.ValuationDate).ToList();
            var orderedCashFlow = new Queue<ICashFlowOperation>((cashflows ?? new List<ICashFlowOperation>()).OrderBy(i => i.OperationDate));

            var previousValue = (decimal)0;
            var accumulatedCashFlow = (decimal)0;

            var points = new List<PerformancePoint>();

            var initialInvestmentFound = false;

            foreach(var valuation in orderedValuations)
            {
                while (orderedCashFlow.Count > 0 && orderedCashFlow.Peek().OperationDate <= valuation.ValuationDate)
                {
                    accumulatedCashFlow += orderedCashFlow.Dequeue().OperationValue;
                }

                initialInvestmentFound = initialInvestmentFound || accumulatedCashFlow != 0;

                // Cannot compute performance until cashflow is different from 0
                if (!initialInvestmentFound && valuation.PortfolioValue != 0)
                {
                    throw new Exception("Portfolio has a value without initial investment. Cannot compute performance.");
                }

                points.Add(new PerformancePoint
                {
                    Valuation = valuation,
                    AccumulatedCashFlow = accumulatedCashFlow,
                    Performance = previousValue == 0 ? 0 : (((valuation.PortfolioValue - accumulatedCashFlow) / previousValue) - 1)
                });

                previousValue = valuation.PortfolioValue;
            }

            return points;
        }
    }
}