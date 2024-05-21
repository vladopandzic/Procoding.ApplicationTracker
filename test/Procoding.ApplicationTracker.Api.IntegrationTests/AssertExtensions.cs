namespace Procoding.ApplicationTracker.Api.IntegrationTests;

public static class AssertExtensions
{
    public static void AssertError(List<KeyValuePair<string, string[]>> errors, string key, string expectedErrorMessage)
    {
        Assert.That(errors.Any(kvp => kvp.Key == key && kvp.Value.Contains(expectedErrorMessage)),$"Expected for {key} to contains message: {expectedErrorMessage}");
    }
}
