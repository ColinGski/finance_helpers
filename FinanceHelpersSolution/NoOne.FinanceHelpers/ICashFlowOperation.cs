namespace NoOne.FinanceHelpers
{
    public interface ICashFlowOperation
    {
        public DateTime OperationDate { get; }

        public decimal OperationValue { get; }
    }
}