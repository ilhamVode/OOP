using Xunit;
using Lab1;

public class CoinStorageTests
{
    [Fact]
    public void AddCoins_NewCoins_IncreaseNominals_IncreaseSum()
    {
        // Arrange
        var storage = new CoinStorage();
        var initialSum = storage.GetSumAllCoins();

        // Act
        storage.AddCoins(0.2m, 10);
        storage.AddCoins(0.5m, 40);
        var finalSum = storage.GetSumAllCoins();
        var coinNominals = storage.GetCoinNominals();

        // Assert
        Assert.Contains(0.2m, coinNominals);
        Assert.Contains(0.5m, coinNominals);
        Assert.Equal(2, coinNominals.Count);
        Assert.True(initialSum < finalSum);
    }

    [Fact]
    public void AddCoins_ToExistingNominal_IncreasesCount()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(0.2m, 10);
        var initialCount = storage.GetCoinCount(0.2m);

        // Act
        storage.AddCoins(0.2m, 50);
        var finalCount = storage.GetCoinCount(0.2m);

        // Assert
        Assert.Equal(50, finalCount - initialCount);
    }

    [Fact]
    public void GetCoinCount_AddCoins_IncreaseCountCoin()
    {
        // Arrange
        var storage = new CoinStorage();

        // Act
        storage.AddCoins(0.2m, 10);
        var countAfterFirstAdd = storage.GetCoinCount(0.2m);

        storage.AddCoins(0.2m, 50);
        var countAfterSecondAdd = storage.GetCoinCount(0.2m);

        // Assert
        Assert.Equal(10, countAfterFirstAdd);
        Assert.Equal(60, countAfterSecondAdd);
        Assert.True(countAfterFirstAdd < countAfterSecondAdd);
    }

    [Fact]
    public void RemoveCoins_ExistingCoins_DecreaseSum_NotExistingNominals()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(2m, 10);
        storage.AddCoins(1m, 30);
        var sumBeforeRemove = storage.GetSumAllCoins();

        // Act
        storage.RemoveCoins(2m, 10);
        storage.RemoveCoins(1m, 29);
        var sumAfterRemove = storage.GetSumAllCoins();

        // Assert
        Assert.Equal(0, storage.GetCoinCount(2m));
        Assert.Equal(1, storage.GetCoinCount(1m));
        Assert.True(sumBeforeRemove > sumAfterRemove);
    }

    [Fact]
    public void RemoveCoins_NotExistingCoins_UnchangedSum_ReturnFalse()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(2m, 10);
        storage.AddCoins(1m, 30);
        var sumBeforeRemove = storage.GetSumAllCoins();

        // Act
        var removeResult1 = storage.RemoveCoins(4m, 10);
        var removeResult2 = storage.RemoveCoins(6m, 29);
        var sumAfterRemove = storage.GetSumAllCoins();

        // Assert
        Assert.False(removeResult1);
        Assert.False(removeResult2);
        Assert.True(sumBeforeRemove == sumAfterRemove);
    }

    [Fact]
    public void GetCoinNominals_EmptyStorage_ReturnsEmptyList()
    {
        // Arrange
        var storage = new CoinStorage();

        // Act
        var nominals = storage.GetCoinNominals();

        // Assert
        Assert.Empty(nominals);
    }

    [Fact]
    public void GetCoinNominals_WithCoins_ReturnsAllNominals()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(0.5m, 10);
        storage.AddCoins(1m, 5);

        // Act
        var nominals = storage.GetCoinNominals();

        // Assert
        Assert.Contains(0.5m, nominals);
        Assert.Contains(1m, nominals);
        Assert.Equal(2, nominals.Count);
    }

    [Fact]
    public void GetCoinNominals_AddRemoveCoins_ReturnsUpdatedNominals()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(0.2m, 5);
        storage.AddCoins(0.5m, 10);
        storage.RemoveCoins(0.2m, 5);

        // Act
        var nominals = storage.GetCoinNominals();

        // Assert
        Assert.Contains(0.5m, nominals);
        Assert.Contains(0.2m, nominals);
        Assert.Equal(2, nominals.Count);
    }

    [Fact]
    public void GetSumAllCoins_EmptyStorage_ReturnsZero()
    {
        // Arrange
        var storage = new CoinStorage();

        // Act
        var totalSum = storage.GetSumAllCoins();

        // Assert
        Assert.Equal(0m, totalSum);
    }

    [Fact]
    public void GetSumAllCoins_WithCoins_ReturnsCorrectSum()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(0.2m, 5);
        storage.AddCoins(0.5m, 4);

        // Act
        var totalSum = storage.GetSumAllCoins();

        // Assert
        Assert.Equal(3.0m, totalSum);
    }

    [Fact]
    public void GetSumAllCoins_AfterRemovingCoins_DecreasesSum()
    {
        // Arrange
        var storage = new CoinStorage();
        storage.AddCoins(1m, 5);
        storage.AddCoins(2m, 3);
        var sumBeforeRemove = storage.GetSumAllCoins();

        // Act
        storage.RemoveCoins(2m, 2);
        var sumAfterRemove = storage.GetSumAllCoins();

        // Assert
        Assert.True(sumAfterRemove < sumBeforeRemove);
        Assert.Equal(7m, sumAfterRemove);
    }
}