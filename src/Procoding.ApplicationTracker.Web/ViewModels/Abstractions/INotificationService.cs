using FluentResults;

namespace Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

public interface INotificationService
{
    public void ShowMessageFromResult<T>(Result<T> result);
}
