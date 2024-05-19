namespace Procoding.ApplicationTracker.Web.Services;

public static class UrlConstants
{
    public static class JobApplicationSources
    {

        public const string GET_ALL_URL = "job-application-sources";

        public static string GetOne(Guid id)
        {
            return $"{GET_ALL_URL}/{id}";
        }

        public static string UpdateUrl()
        {
            return $"{GET_ALL_URL}";
        }

        public static string InsertUrl()
        {
            return $"{GET_ALL_URL}";
        }
    }
}
