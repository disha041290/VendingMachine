using VendingMachine.Domain;

public class CoinIdentifier
{
    public static CoinType Identify(Coin coin)
    {
        return (coin.Weight, coin.Size) switch
        {
            (5.0m, 21.21m) => CoinType.Nickel,
            (2.268m, 17.91m) => CoinType.Dime,
            (5.670m, 24.26m) => CoinType.Quarter,
            _ => CoinType.Invalid
        };
    }

    public static decimal GetValue(CoinType type) => type switch
    {
        CoinType.Nickel => 0.05m,
        CoinType.Dime => 0.10m,
        CoinType.Quarter => 0.25m,
        _ => 0.00m
    };
}