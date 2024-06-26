﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Procoding.ApplicationTracker.Web.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<Result<TSuccess>> HandleResponseAsync<TSuccess>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response.IsSuccessStatusCode)
        {
            var successContent = await response.Content.ReadFromJsonAsync<TSuccess>(cancellationToken);
            return Result.Ok(successContent!);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return Result.Fail<TSuccess>(new Error("You are not authorized. You can try to login again"));
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result.Fail<TSuccess>(new Error($"You are not authorized to visit resource {response.RequestMessage?.RequestUri}"));
        }
        else
        {
            var errorContent = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);
            var errors = (errorContent as ProblemDetails)?.ToErrors() ?? new List<string>();
            return Result.Fail<TSuccess>(errors is null || errors.Count == 0 ? [$"Error happened. Status code:{response.StatusCode}"] : errors);
        }
    }
}