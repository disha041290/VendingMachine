using VendingMachine.Domain;
public interface IVendingMachineRepository
{
    void InsertCoin(Coin coin);
    string GetDisplay();
    string SelectProduct(string productName);
    List<Coin> CoinReturn { get; }
    void Reset();
}