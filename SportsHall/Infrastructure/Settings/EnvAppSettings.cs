using SportsHall.Services.Interfaces;

namespace SportsHall.Infrastructure.Settings
{
    public class EnvAppSettings : IAppSettings
    {
        // Main App
        public string ApplicationPort
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("APPLICATION_PORT") ?? "3000";
                Console.WriteLine($"APPLICATION_PORT: {value}");
                return value;
            }
        }

        public string ContainerPort
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("CONTAINER_PORT") ?? "8080";
                Console.WriteLine($"CONTAINER_PORT: {value}");
                return value;
            }
        }

        public string DatabasePort
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "1433";
                Console.WriteLine($"DATABASE_PORT: {value}");
                return value;
            }
        }

        // Database
        public string DatabaseServerName
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("DATABASE_SERVER_NAME") ?? "localhost";
                Console.WriteLine($"DATABASE_SERVER_NAME: {value}");
                return value;
            }
        }

        public string ExportDatabasePort
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("EXPORT_DATABASE_PORT") ?? "5000";
                Console.WriteLine($"EXPORT_DATABASE_PORT: {value}");
                return value;
            }
        }

        public string UserName
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("USER_NAME") ?? "sa";
                Console.WriteLine($"USER_NAME: {value}");
                return value;
            }
        }

        public string UserPassword
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("USER_PASSWORD") ?? "fakePassw0rd";
                Console.WriteLine($"USER_PASSWORD: {value}");
                return value;
            }
        }

        public string MssqlVersion
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("MSSQL_VERSION") ?? "2022-latest";
                Console.WriteLine($"MSSQL_VERSION: {value}");
                return value;
            }
        }

        // Superuser
        public string DbSuperUser
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("DB_SUPERUSER") ?? "admin_user";
                Console.WriteLine($"DB_SUPERUSER: {value}");
                return value;
            }
        }

        public string DbSuperUserPassword
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("DB_SUPERUSER_PASSWORD") ?? "StrongAdminPassw0rd!";
                Console.WriteLine($"DB_SUPERUSER_PASSWORD: {value}");
                return value;
            }
        }

        public string EnvironmentMode
        {
            get
            {
                var value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {value}");
                return value;
            }
        }
    }
}