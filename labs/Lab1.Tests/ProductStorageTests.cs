
using Xunit;
using Lab1;

public class ProductStorageTests
{
    [Fact]
    public void AddProduct_NewProduct_IncreaseCountProducts_ExistingProduct()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);

        // Act
        storage.AddProduct(product);

        // Assert
        Assert.Equal(1, storage.CountProducts());
        Assert.True(storage.HasProductByName("Fanta"));
    }

    [Fact]
    public void AddProduct_ExistingProduct_PriceUnchange_CountIncrease_CountProductsUnchange()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var existingProduct = new Product("Fanta", 0.4m, 5);

        // Act
        storage.AddProduct(product);
        storage.AddProduct(existingProduct);

        // Assert
        Assert.Equal(0.4m, product.Price);
        Assert.Equal(7, product.Count);
        Assert.Equal(1, storage.CountProducts());
    }

    [Fact]
    public void AddProduct_NotExistingProduct_CountProductsIncrease_NotEqualPrices()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var product2 = new Product("Cola", 0.7m, 5);

        // Act
        storage.AddProduct(product);
        storage.AddProduct(product2);

        // Assert
        Assert.Equal(2, storage.CountProducts());
        Assert.NotEqual(product.Price, product2.Price);
    }

    [Fact]
    public void RemoveProduct_ExistingProduct_DecreaseCountProducts_NotExistingProduct_ReturnTrue()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);

        // Act
        storage.AddProduct(product);
        bool result =storage.RemoveProduct(product, 2);

        // Assert
        Assert.Equal(0, product.Count);
        Console.WriteLine();
        Assert.True(result);
    }

    [Fact]

    public void RemoveProduct_NotExistingProduct_UnchangedCountProduct_ReturnFalse()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var product1 = new Product("Cola", 0.6m, 3);
        var product2 = new Product("Sprite", 0.4m, 7);
        // Act
        storage.AddProduct(product);
        storage.AddProduct(product1);
        storage.AddProduct(product2);
        bool result = storage.RemoveProduct(storage.GetProductByName("Добрый кола"), 10);

        // Assert
        Assert.Equal(3, storage.CountProducts());
        Assert.False(result);
    }

    [Fact]
    public void RemoveProduct_OverCountProduct_UnchangedCount_ReturnFalse()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);

        // Act
        storage.AddProduct(product);
        var result = storage.RemoveProduct(product, 3);

        // Assert
        Assert.Equal(2, product.Count);
        Assert.False(result);
    }

    [Fact]
    public void CountProducts_NewProducts_ReturnCount()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var product1 = new Product("Cola", 0.6m, 3);
        var product2 = new Product("Sprite", 0.7m, 7);
        var product3 = new Product("BonAqua", 20, 5);

        // Act
        storage.AddProduct(product);
        storage.AddProduct(product1);
        storage.AddProduct(product2);
        storage.AddProduct(product3);
        var result = storage.CountProducts();

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void CountProducts_ExistingProducts_ReturnCount()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var product1 = new Product("Cola", 0.6m, 3);
        var product2 = new Product("Sprite", 0.7m, 7);
        var product3 = new Product("BonAqua", 20m, 5);
        var copy_product = new Product("Fanta", 0.44m, 6);
        var copy_pproduct1 = new Product("Cola", 0.65m, 1);
        var copy_pproduct2 = new Product("Sprite", 0.77m, 3);
        var copy_pproduct3 = new Product("BonAqua", 200m, 10);

        // Act
        storage.AddProduct(product);
        storage.AddProduct(product1);
        storage.AddProduct(product2);
        storage.AddProduct(product3);
        storage.AddProduct(copy_product);
        storage.AddProduct(copy_pproduct1);
        storage.AddProduct(copy_pproduct2);
        storage.AddProduct(copy_pproduct3);
        var result = storage.CountProducts();

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void HasProductByName_NotRegistry_ReturnsTrue()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);

        // Act
        storage.AddProduct(product);
        var return1 = storage.HasProductByName(" FAnta");
        var return2 = storage.HasProductByName("FANTA ");
        var return3 = storage.HasProductByName(" fanta");
        var return4 = storage.HasProductByName("   fAntA   ");
        var return5 = storage.HasProductByName(" FaNTa ");

        // Assert
        Assert.True(return1);
        Assert.True(return2);   
        Assert.True(return3);
        Assert.True(return4);
        Assert.True(return5);
    }

    [Fact]
    public void HasProductByName_NotExisting_ReturnsFalse()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Cola", 0.4m, 2);

        // Act
        storage.AddProduct(product);
        var result1 = storage.HasProductByName(" ");
        var result2 = storage.HasProductByName("");
        var result3 = storage.HasProductByName("Добый кола");
        var result4 = storage.HasProductByName("Добрый орендж");

        // Assert
        Assert.False(result1);
        Assert.False(result2);
        Assert.False(result3);
        Assert.False(result4);
    }

    [Fact]
    public void GetProductByName_ExistingProduct_ReturnProduct()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var product1 = new Product("Cola", 0.6m, 3);


        // Act
        storage.AddProduct(product);
        storage.AddProduct(product1);
        var result1 = storage.GetProductByName("Fanta  ");
        var result2 = storage.GetProductByName("COLA  ");

        // Assert
        Assert.Equal(product, result1);
        Assert.Equal(product1, result2);
    }

    [Fact]
    public void GetProductByName_NotExistingProduct_ReturnNull()
    {
        // Arrange
        var storage = new ProductStorage();
        var product = new Product("Fanta", 0.4m, 2);
        var product1 = new Product("Cola", 0.6m, 3);


        // Act
        storage.AddProduct(product);
        storage.AddProduct(product1);
        var result1 = storage.GetProductByName("Добрый кола");
        var result2 = storage.GetProductByName("Добрый фанта");
        var result3 = storage.GetProductByName("Sprite");

        // Assert
        Assert.Null(result1);
        Assert.Null(result2);
        Assert.Null(result3);
    }
}
