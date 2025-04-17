﻿using Api.ExceptionHandler;
using Application.Users.GetById;
using Domain.Abstractions.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId:guid}", async (
                [FromRoute] Guid userId,
                ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(userId);

                Result<UserResponse> result = await sender.Send(query, cancellationToken);

                return result.Match(
                    Results.Ok,
                    CustomResults.Problem
                );
            })
            .WithName("GetById")
            .WithTags(Tags.Users)
            .WithOpenApi()
            .Produces<UserResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
