﻿using Domain.ApiKeys;
using Domain.EmailTemplates;
using Domain.Infrastructure.Outbox;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<OutboxMessage> OutboxMessages { get; }
    
    DbSet<EmailVerificationToken> EmailVerificationTokens { get; }
    DbSet<EmailTemplate> EmailTemplates { get; }
    DbSet<ApiKey> ApiKeys { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DatabaseFacade Database { get; }
    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
