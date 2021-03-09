using System;
using System.Linq;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Migrations;

namespace MigrationLaunch
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            var flag = true;

            while (flag)
            {
                Console.WriteLine("-------------------------------------------------------------------------------\nUpdate database? [UP/DOWN]\n-------------------------------------------------------------------------------");
                var upDown = Console.ReadLine();

                if (upDown.ToLower() == "down")
                {

                }

                if (upDown.ToLower() == "up")
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        UpdateDatabase(scope.ServiceProvider);
                    }
                }

                Console.WriteLine("-------------------------------------------------------------------------------\nEnd? [Y/N]\n-------------------------------------------------------------------------------");
                var yesNot = Console.ReadLine();

                if (yesNot.ToLower() == "y")
                {
                    flag = false;
                }

                Console.Clear();
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString("User ID=di;Password=0000;Host=localhost;Port=5432;Database=MasterManageDB;")
                    .ScanIn(typeof(InitMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        private static void DropDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateDown(1);
        }
    }
}
