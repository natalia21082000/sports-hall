namespace SportsHall.Services.Interfaces
{
    public interface IAppSettings
    {
        // Main App
        string ApplicationPort { get; }
        string ContainerPort { get; }
        string DatabasePort { get; }

        // Database
        string DatabaseServerName { get; }
        string ExportDatabasePort { get; }
        string UserName { get; }
        string UserPassword { get; }
        string MssqlVersion { get; }

        // Superuser
        string DbSuperUser { get; }
        string DbSuperUserPassword { get; }

        string EnvironmentMode { get; }
    }
}
