namespace Cart.Api.Test;

public class SaleCalcuatorTests
{
    [Fact]
    public void DefaultSaleCalcuatorTest()
    {
        ISaleCalcuator saleCalcuator = new DefaultSaleCalcuator();

        // Act
        var result = saleCalcuator.Calcuate(new SaleContext {
            Price = 10.0m,
        });

        // Assert
        Assert.Equal(10.0m, result);
    }

    [Fact]
    public void GraduateSaleCalcuatorTest()
    {
        ISaleCalcuator saleCalcuator = new GraduateSaleCalcuator();

        // Act
        var result = saleCalcuator.Calcuate(new SaleContext {
            Price = 10.0m,
            IsGraduate = true,
        });

        // Assert
        Assert.Equal(7.0m, result);
    }

    [Fact]
    public void FiveDaysSaleCalcuatorTest()
    {
        ISaleCalcuator saleCalcuator = new FiveDaysSaleCalcuator();

        // Act
        var result = saleCalcuator.Calcuate(new SaleContext {
            Price = 10.0m,
            StartDate = DateTimeOffset.UtcNow.Date.AddDays(-5),
        });

        // Assert
        Assert.Equal(8.0m, result);
    }
}