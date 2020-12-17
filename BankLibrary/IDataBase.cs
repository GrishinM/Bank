namespace BankLibrary
{
    public interface IDataBase
    {
        Card GetCardById(int id);

        Card GetCardByNumber(long number);

        void ApplyChanges(Card card);
    }
}