using FluentResults;
using MudBlazor;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.Services;

public class NotificationService : INotificationService
{
    private readonly ISnackbar _snackbar;

    public NotificationService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    public void ShowMessageFromResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            _snackbar.Add("Successfully saved!", Severity.Success);
        }
        else
        {
            _snackbar.Add(string.Join("", result.Errors.Select(x => $"<p>{x}</p>").ToList()),Severity.Error);
        }
    }
}
