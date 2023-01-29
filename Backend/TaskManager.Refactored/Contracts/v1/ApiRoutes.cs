namespace TaskManager.Refactored.Contracts.v1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;
    public static class Task
    {
        public const string Get = Base + "/task/{taskId}";
        public const string GetAll = $"{Base}/task";
        public const string Create = $"{Base}/task";
    }
    public static class Account
    {
        public const string Login = $"{Base}/account/login";
    }
}