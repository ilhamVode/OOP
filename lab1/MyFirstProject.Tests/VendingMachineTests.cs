using Xunit;
using MyFirstProject;

public class VendingMachineTests
{
    [Fact]
    public void BuyProduct_ExistingProduct_ReturnsEmptyChange()
    {
        // Arrange
        var machine = new VendingMachine();
        var product = new Product("Cola", 1.5m, 5);
        machine.AddProduct(product);
        machine.InsertCoinsToBuffer(1m, 1);
        machine.InsertCoinsToBuffer(0.5m, 1);

        // Act
        var change = machine.BuyProduct("Cola");

        // Assert
        Assert.NotNull(change);
        Assert.Empty(change);
    }

    [Fact]
    public void BuyProduct_ExistingProduct_WithChange_ReturnsCorrectChange()
    {
        // Arrange
        var machine = new VendingMachine();
        var product = new Product("Cola", 1.5m, 5);
        machine.AddProduct(product);
        machine.AddCoinsToVM(0.5m, 10);
        machine.InsertCoinsToBuffer(2m, 1);

        // Act
        var change = machine.BuyProduct("Cola");

        // Assert
        Assert.NotNull(change);
        Assert.Single(change);
        Assert.Equal(1, change[0.5m]);
    }

    [Fact]
    public void BuyProduct_NotExistingProduct_ReturnsNull()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.InsertCoinsToBuffer(1m, 2);

        // Act
        var change = machine.BuyProduct("Cola");

        // Assert
        Assert.Null(change);
    }

    [Fact]
    public void BuyProduct_NotEnoughMoney_ReturnsNull()
    {
        // Arrange
        var machine = new VendingMachine();
        var product = new Product("Cola", 2m, 5);
        machine.AddProduct(product);
        machine.InsertCoinsToBuffer(1m, 1);

        // Act
        var change = machine.BuyProduct("Cola");

        // Assert
        Assert.Null(change);
    }

    [Fact]
    public void BuyProduct_NotProduct_ReturnsNull()
    {
        // Arrange
        var machine = new VendingMachine();
        var product = new Product("Cola", 1m, 0);
        machine.AddProduct(product);
        machine.InsertCoinsToBuffer(1m, 1);

        // Act
        var change = machine.BuyProduct("Cola");

        // Assert
        Assert.Null(change);
    }

    [Fact]
    public void BuyProduct_NotChange_ReturnsNull()
    {
        // Arrange
        var machine = new VendingMachine();
        var product = new Product("Cola", 2m, 5);
        machine.AddProduct(product);
        machine.InsertCoinsToBuffer(5m, 1);

        // Act
        var change = machine.BuyProduct("Cola");

        // Assert
        Assert.Null(change);
    }

    [Fact]
    public void CancelOperation_WithCoinsInBuffer_ReturnsAllCoins()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.InsertCoinsToBuffer(0.5m, 3);
        machine.InsertCoinsToBuffer(1m, 2);

        // Act
        var returnedCoins = machine.CancelOperation();

        // Assert
        Assert.Equal(2, returnedCoins.Count);
        Assert.Equal(3, returnedCoins[0.5m]);
        Assert.Equal(2, returnedCoins[1m]);
    }

    [Fact]
    public void CancelOperation_EmptyBuffer_ReturnsEmptyDictionary()
    {
        // Arrange
        var machine = new VendingMachine();

        // Act
        var returnedCoins = machine.CancelOperation();

        // Assert
        Assert.NotNull(returnedCoins);
        Assert.Empty(returnedCoins);
    }

    [Fact]
    public void CancelOperation_ClearsBuffer()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.InsertCoinsToBuffer(1m, 2);

        // Act
        var firstReturn = machine.CancelOperation();
        var secondReturn = machine.CancelOperation(); // Буфер должен быть пуст

        // Assert
        Assert.NotEmpty(firstReturn);
        Assert.Empty(secondReturn);
    }

    [Fact]
    public void CollectMoney_WithCoinsInWallet_ReturnsAllCoins()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(0.5m, 10);
        machine.AddCoinsToVM(1m, 5);

        // Act
        var collectedMoney = machine.CollectMoney();

        // Assert
        Assert.Equal(2, collectedMoney.Count);
        Assert.Equal(10, collectedMoney[0.5m]);
        Assert.Equal(5, collectedMoney[1m]);
    }

    [Fact]
    public void CollectMoney_EmptyWallet_ReturnsEmptyDictionary()
    {
        // Arrange
        var machine = new VendingMachine();

        // Act
        var collectedMoney = machine.CollectMoney();

        // Assert
        Assert.NotNull(collectedMoney);
        Assert.Empty(collectedMoney);
    }

    [Fact]
    public void CollectMoney_ClearsWallet()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(1m, 3);

        // Act
        var firstCollection = machine.CollectMoney();
        var secondCollection = machine.CollectMoney();

        // Assert
        Assert.NotEmpty(firstCollection);
        Assert.Empty(secondCollection);
    }

    [Fact]
    public void TransferAllCoinsFromBufferToVendingWallet_WithCoinsInBuffer_MovesAllCoinsToWallet()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.InsertCoinsToBuffer(0.5m, 3);
        machine.InsertCoinsToBuffer(1m, 2);
        machine.InsertCoinsToBuffer(2m, 1);

        // Act
        machine.TransferAllCoinsFromBufferToVendingWallet();

        // Assert
        var collectedMoney = machine.CollectMoney();
        Assert.Equal(3, collectedMoney.Count);
        Assert.Equal(3, collectedMoney[0.5m]);
        Assert.Equal(2, collectedMoney[1m]);
        Assert.Equal(1, collectedMoney[2m]);
    }

    [Fact]
    public void TransferAllCoinsFromBufferToVendingWallet_EmptyBuffer_NoChanges()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(1m, 5); 
        var walletBefore = machine.CollectMoney(); 
        machine.AddCoinsToVM(1m, 5);

        // Act
        machine.TransferAllCoinsFromBufferToVendingWallet();

        // Assert
        var walletAfter = machine.CollectMoney();
        Assert.Equal(walletBefore[1m], walletAfter[1m]);
    }

    [Fact]
    public void GetChange_ZeroChange_ReturnsEmptyDictionary()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(1m, 10);

        // Act
        var change = machine.GetChange(0m);

        // Assert
        Assert.NotNull(change);
        Assert.Empty(change);
    }

    [Fact]
    public void GetChange_ChangeAvailable_ReturnsCorrectCoins()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(0.5m, 4); 
        machine.AddCoinsToVM(1m, 3); 
        machine.AddCoinsToVM(2m, 2); 

        // Act
        var change = machine.GetChange(3.5m);

        // Assert
        Assert.Equal(1, change[1m]);
        Assert.Equal(1, change[2m]);   
        Assert.Equal(1, change[0.5m]);   
    }

    [Fact]
    public void GetChange_ChangeFromAllNominals_ReturnsCoins()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(0.1m, 10);
        machine.AddCoinsToVM(0.5m, 5);  
        machine.InsertCoinsToBuffer(1m, 3);    
        machine.InsertCoinsToBuffer(2m, 2);    
        

        // Act
        var change = machine.GetChange(4.6m);

        // Assert
        Assert.Equal(3, change.Count);
        Assert.Equal(2, change[2m]);    
        Assert.Equal(1, change[0.5m]); 
        Assert.Equal(1, change[0.1m]); 
    }

    [Fact]
    public void GetChange_ChangeNotPossible_ReturnsEmptyDictionary()
    {
        // Arrange
        var machine = new VendingMachine();
        machine.AddCoinsToVM(2m, 5); 

        // Act
        var change = machine.GetChange(1m); 

        // Assert
        Assert.NotNull(change);
        Assert.Empty(change);
    }
}