namespace BankLibrary.Requests
{
    public class Transfer
    {
        public long CardToNumber { get; set; }

        public double Amount { get; set; }
    }
}