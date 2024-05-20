using System.ComponentModel;

namespace Procoding.ApplicationTracker.Web.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;


    public virtual void OnPropertyChanged(string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    protected virtual void SetPropertyValue<TType>(ref TType property, TType value, string? propertyName = null)
    {
        if ((property == null && value == null) || property?.Equals(value) == true)
        {
            return;
        }

        property = value;
        OnPropertyChanged(propertyName!);
    }

    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading;
        protected set
        {

            if (_isLoading == value)
            {
                return;
            }

            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }


    public async Task LoadingWhileProcessing(Func<Task> action)
    {
        IsLoading = true;
        await action.Invoke();
        IsLoading = false;
    }

    public async Task<TResult> LoadingWhileProcessing<TResult>(Func<Task<TResult>> action)
    {
        IsLoading = true;
        var result = await action.Invoke();
        IsLoading = false;
        return result;
    }

}