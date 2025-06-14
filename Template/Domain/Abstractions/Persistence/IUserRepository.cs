﻿using Domain.Users;

namespace Domain.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> UserWithEmailExists(string email, CancellationToken cancellationToken = default);

    void Add(User user);
}
