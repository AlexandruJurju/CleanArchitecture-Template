﻿using Application.Abstractions.Outbox;
using Application.Abstractions.Persistence;
using Application.Abstractions.Time;
using Domain.Abstractions;
using Domain.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Outbox;

public class ProcessOutboxMessagesJob(
    ISender sender,
    IDateTimeProvider dateTimeProvider,
    IApplicationDbContext applicationDbContext,
    ILogger<ProcessOutboxMessagesJob> logger)
    : IProcessOutboxMessagesJob
{
    private const int BATCH_SIZE = 1000;

    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public async Task ProcessAsync()
    {
        logger.LogInformation("Starting to process outbox messages");

        // Use a transaction scope for atomicity
        await using IDbContextTransaction transaction = await applicationDbContext.Database.BeginTransactionAsync();

        List<OutboxMessage> outboxMessages = await applicationDbContext.OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .Take(BATCH_SIZE)
            .OrderBy(m => m.OccurredOnUtc)
            .ToListAsync();

        if (outboxMessages.Count == 0)
        {
            logger.LogInformation("No outbox messages to process");
            return;
        }

        try
        {
            foreach (OutboxMessage outboxMessage in outboxMessages)
            {
                Exception? exception = null;

                try
                {
                    IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                        outboxMessage.Content,
                        JsonSerializerSettings)!;

                    await sender.Send(domainEvent);
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Exception while processing outbox message {MessageId}",
                        outboxMessage.Id
                    );
                    exception = ex;
                }

                // Update message status (whether success or failed)
                outboxMessage.ProcessedOnUtc = dateTimeProvider.UtcNow;
                outboxMessage.Error = exception?.ToString();
            }

            await applicationDbContext.SaveChangesAsync();

            await transaction.CommitAsync().ConfigureAwait(false);
        }
        catch
        {
            await transaction.RollbackAsync().ConfigureAwait(false);
            throw;
        }
    }
}
