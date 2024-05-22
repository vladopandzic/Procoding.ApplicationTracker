using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Procoding.ApplicationTracker.Web;

public class CustomObjectModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var valueAsString = valueProviderResult.FirstValue;
        if (valueAsString == null)
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        // Determine the appropriate type for the filter value based on the incoming data
        object? convertedValue = null;

        // Add your custom logic here to convert the string value to the appropriate type
        // For example, parse the string to DateTime, int, double, etc., based on some conditions
        // You can also handle special cases where the value is a complex object or another nullable object

        // Set the result of model binding
        bindingContext.Result = ModelBindingResult.Success(convertedValue);
        return Task.CompletedTask;
    }
}
