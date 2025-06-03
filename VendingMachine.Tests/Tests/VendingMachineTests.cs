using Xunit;

public class VendingMachineTests
{
    private readonly VendingMachineRepository _vendingMachine = new();

    [Fact]
    public void Insert_Valid_Quarter_Updates_Amount()
    {
        var coin = new Coin(5.670m, 24.26m);
        _vendingMachine.InsertCoin(coin);
        Assert.Equal("$0.25", _vendingMachine.GetDisplay());
    }

    [Fact]
    public void Default_Display_Is_InsertCoin()
    {
        Assert.Equal("INSERT COIN", _vendingMachine.GetDisplay());
    }

    [Fact]
    public void Select_Product_With_Enough_Money_Dispenses()
    {
        _vendingMachine.InsertCoin(new Coin(5.670m, 24.26m)); // 0.25
        _vendingMachine.InsertCoin(new Coin(5.670m, 24.26m)); // 0.25

        var result = _vendingMachine.SelectProduct("chips");
        Assert.Equal("Dispensed: chips", result);
        Assert.Equal("THANK YOU", _vendingMachine.GetDisplay());
        Assert.Equal("INSERT COIN", _vendingMachine.GetDisplay());
    }

    [Fact]
    public void Select_Product_Without_Enough_Money_Shows_Price()
    {
        _vendingMachine.InsertCoin(new Coin(2.268m, 17.91m)); // 0.10
        var result = _vendingMachine.SelectProduct("candy");
        Assert.Null(result);
        Assert.Equal("PRICE: $0.65", _vendingMachine.GetDisplay());
        Assert.Equal("$0.10", _vendingMachine.GetDisplay());
    }
}
