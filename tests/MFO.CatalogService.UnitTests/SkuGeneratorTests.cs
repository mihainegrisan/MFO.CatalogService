using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MFO.CatalogService.UnitTests;

[TestFixture]
public class SkuGeneratorTests
{
    private ISkuSequenceRepository _skuSequenceRepository;

    private const string CompanyCode = "QQQQ1";
    private const string CategoryCode = "WWWW1";
    private const string BrandCode = "EEEE1";

    [SetUp]
    public void Setup()
    {
        _skuSequenceRepository = Substitute.For<ISkuSequenceRepository>();
    }

    [Test]
    public async Task GenerateSku_ReturnsTheCorrectSKU()
    {
        // Arrange
        _skuSequenceRepository
            .GetNextNumberForSkuAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(1);

        var skuGenerator = new SkuGenerator(_skuSequenceRepository);

        // Act
        var sku = await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);

        // Assert
        await _skuSequenceRepository
            .Received(1)
            .GetNextNumberForSkuAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());

        Assert.That(sku, Is.EqualTo("QQQQ1-WWWW1-EEEE1-00001"));
    }

    [Test]
    public async Task GenerateSku_ShouldIncrementSequence()
    {
        // Arrange
        _skuSequenceRepository
            .GetNextNumberForSkuAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(1, 2, 3);

        var skuGenerator = new SkuGenerator(_skuSequenceRepository);

        // Act
        var sku1 = await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);
        var sku2 = await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);
        var sku3 = await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);

        // Assert
        Assert.That(sku1, Is.EqualTo("QQQQ1-WWWW1-EEEE1-00001"));
        Assert.That(sku2, Is.EqualTo("QQQQ1-WWWW1-EEEE1-00002"));
        Assert.That(sku3, Is.EqualTo("QQQQ1-WWWW1-EEEE1-00003"));
    }

    [Test]
    public async Task GenerateSku_ShouldPadNumbersCorrectly()
    {
        // Arrange
        _skuSequenceRepository
            .GetNextNumberForSkuAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(42);

        var skuGenerator = new SkuGenerator(_skuSequenceRepository);

        // Act
        var sku = await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);

        // Assert
        Assert.That(sku, Is.EqualTo("QQQQ1-WWWW1-EEEE1-00042"));
    }

    [Test]
    public async Task GenerateSku_ShouldProperlyPassParametersToRepository()
    {
        // Arrange
        _skuSequenceRepository
            .GetNextNumberForSkuAsync(CompanyCode, CategoryCode, BrandCode, Arg.Any<CancellationToken>())
            .Returns(1);

        var skuGenerator = new SkuGenerator(_skuSequenceRepository);

        // Act
        await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);

        // Assert
        await _skuSequenceRepository
            .Received(1)
            .GetNextNumberForSkuAsync(CompanyCode, CategoryCode, BrandCode, Arg.Any<CancellationToken>());
    }

    [Test]
    public void GenerateSku_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        _skuSequenceRepository
            .GetNextNumberForSkuAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception("DB error"));

        var skuGenerator = new SkuGenerator(_skuSequenceRepository);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () => await skuGenerator.GenerateSku(CompanyCode, CategoryCode, BrandCode, CancellationToken.None));
    }
}
