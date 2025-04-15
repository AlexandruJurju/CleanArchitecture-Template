﻿using Domain.Users;

namespace Application.Users.Register;

public sealed class UserRegisteredDomainEventHandler
{
    public Task Handle(UserRegisteredDomainEvent command, CancellationToken cancellationToken)
    {
        // TODO: Send an email verification link, etc.
        return Task.CompletedTask;
    }
}
