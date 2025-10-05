using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Infrastructure.Persistence;
using MFO.CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.IntegrationTests;

[TestFixture]
public class SkuSequenceRepositoryTests
{
    private CatalogDbContext _dbContext;
    private ISkuSequenceRepository _skuSequenceRepository;

    private const string CompanyCode = "QQQQ1";
    private const string CategoryCode = "WWWW1";
    private const string BrandCode = "EEEE1";

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB per test
            .Options;

        _dbContext = new CatalogDbContext(options);
        _skuSequenceRepository = new SkuSequenceRepository(_dbContext);
    }

    [Test]
    public async Task GetNextNumberForSkuAsync_ShouldGetTheCorrectNumber_IfNoEntriesExistForTheCompanyCategoryBrandCodeCombination()
    {
        // Act
        var nextNumberForSku = await _skuSequenceRepository.GetNextNumberForSkuAsync(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);

        // Assert
        Assert.That(nextNumberForSku, Is.EqualTo(1));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(1005)]
    public async Task GetNextNumberForSkuAsync_ShouldGetTheCorrectNumber(int lastNumber)
    {
        // Arrange
        var sku = new SkuSequence
        {
            SkuSequenceId = Guid.CreateVersion7(),
            Company = CompanyCode,
            Category = CategoryCode,
            Brand = BrandCode,
            LastNumber = lastNumber
        };

        await _dbContext.AddAsync(sku);
        await _dbContext.SaveChangesAsync();

        // Act
        var nextNumberForSku = await _skuSequenceRepository.GetNextNumberForSkuAsync(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);

        // Assert
        Assert.That(nextNumberForSku, Is.EqualTo(lastNumber + 1));
    }

    [Test]
    public async Task GetNextNumberForSkuAsync_ShouldGetTheCorrectNumber_IfMultipleCallsPerformedForTheSameCompanyCategoryBrandCodeCombination()
    {
        // Act
        var nextNumberForSku1 = await _skuSequenceRepository.GetNextNumberForSkuAsync(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);
        var nextNumberForSku2 = await _skuSequenceRepository.GetNextNumberForSkuAsync(CompanyCode, CategoryCode, BrandCode, CancellationToken.None);
        
        // Assert
        Assert.That(nextNumberForSku1, Is.EqualTo(1));
        Assert.That(nextNumberForSku2, Is.EqualTo(2));
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }
}
