using System.Globalization;

public class VendingMachineRepository : IVendingMachineRepository
{
    private decimal _amount;
    private string _lastMessage;
    private bool _justDispensed;
    private bool _showPrice = false;
    private string _priceMessage = string.Empty;
    private readonly List<Coin> _coinReturn = new();

    public List<Coin> CoinReturn => _coinReturn;

    private static readonly List<Product> _products = new()
    {
        new Product("cola", 1.00m),
        new Product("chips", 0.50m),
        new Product("candy", 0.65m)
    };

    public void InsertCoin(Coin coin)
    {
        var type = CoinIdentifier.Identify(coin);
        var value = CoinIdentifier.GetValue(type);
        if (type == CoinType.Invalid)
        {
            _coinReturn.Add(coin);
            _lastMessage = "Coin Rejected";
            return;
        }
        _amount += value;
        _lastMessage = _amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
    }

    private bool _coinRejectedDisplayed = false;

    private bool _showThankYou = false;
    private bool _showCoinRejected = false;

    public string GetDisplay()
    {
        if (_showPrice)
        {
            _showPrice = false;  // show once
            return _priceMessage;
        }

        if (_showCoinRejected)
        {
            _showCoinRejected = false;
            return _amount == 0 ? "INSERT COIN" : _amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

        if (_showThankYou)
        {
            _showThankYou = false;
            return "THANK YOU";
        }

        if (_amount == 0)
        {
            return "INSERT COIN";
        }

        return _amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
    }


    public string SelectProduct(string productName)
    {
        var product = _products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        if (product == null) return "Invalid Product";

        if (_amount >= product.Price)
        {
            _amount -= product.Price;
            _showThankYou = true;   // show THANK YOU once
            return $"Dispensed: {product.Name}";
        }

        // Show price once
        _showPrice = true;
        _priceMessage = $"PRICE: {product.Price.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}";
        return null;
    }


    public void Reset()
    {
        _amount = 0;
        _lastMessage = "INSERT COIN";
        _coinReturn.Clear();
    }
}