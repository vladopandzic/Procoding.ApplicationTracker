using FluentValidation;

namespace Procoding.ApplicationTracker.Web.Validators;

public class FluentValueValidator<T> : AbstractValidator<T>
{


    //public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    //{
    //    var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
    //    if (result.IsValid)
    //        return ["error"]
    //    return result.Errors.Select(e => e.ErrorMessage);
    //};
    public Func<object, string, IEnumerable<string>> ValidateValue => (model, propertyName) =>
    {
        //TODO: optimize to use only propertyName
        var result = Validate(ValidationContext<T>.CreateWithOptions((T)model, x => { }));
        if (result.IsValid)
            return [];
        return result.Errors.Select(e => e.ErrorMessage);
    };
}