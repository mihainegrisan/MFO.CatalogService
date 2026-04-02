using MFO.CatalogService.Application.Common.Interfaces;
using MFO.Contracts.Shared;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Persistence;

public class AuditableDbContextBase : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContextProvider _userContextProvider;

    public AuditableDbContextBase(
        DbContextOptions options,
        IDateTimeProvider dateTimeProvider,
        IUserContextProvider userContextProvider)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        _userContextProvider = userContextProvider;
    }

    protected void ApplyAuditing()
    {
        var entries = ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = _dateTimeProvider.UtcNow;
                entry.Entity.CreatedBy = _userContextProvider.UserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedDate = _dateTimeProvider.UtcNow;
                entry.Entity.LastModifiedBy = _userContextProvider.UserId;
            }
        }
    }
}