﻿namespace Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid GetUserId();
}
