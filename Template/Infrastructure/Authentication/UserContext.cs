﻿using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid GetUserId()
    {
        return httpContextAccessor
                   .HttpContext?
                   .User
                   .GetUserId()
               ?? throw new ApplicationException("User context is unavailable");
    }
}
