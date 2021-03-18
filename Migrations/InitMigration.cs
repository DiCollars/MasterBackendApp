using FluentMigrator;

namespace Migrations
{
    [Migration(2021060304)]
    public class InitMigration : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Roles").Exists())
            {
                Create.Table("Roles")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("RoleName").AsString(12).NotNullable()
                  .WithColumn("AccessRight").AsInt32();

                Insert.IntoTable("Roles")
                  .Row(new { RoleName = "MASTER",       AccessRight = 1 })
                  .Row(new { RoleName = "OPERATOR",     AccessRight = 2 })
                  .Row(new { RoleName = "CLIENT",       AccessRight = 3 })
                  .Row(new { RoleName = "MASTER_DADDY", AccessRight = 4 })
                  .Row(new { RoleName = "ADMIN",        AccessRight = 5 });
            }

            if (!Schema.Table("LocationTypes").Exists())
            {
                Create.Table("LocationTypes")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("LocationName").AsString(50).NotNullable();

                Insert.IntoTable("LocationTypes")
                  .Row(new { LocationName = "Район" })
                  .Row(new { LocationName = "Сабрайон" })
                  .Row(new { LocationName = "Деревня" })
                  .Row(new { LocationName = "Лес" })
                  .Row(new { LocationName = "Гаражный кооператив" })
                  .Row(new { LocationName = "Посёлок городского типа" });
            }

            if (!Schema.Table("Specializations").Exists())
            {
                Create.Table("Specializations")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("SpecializationName").AsString(40).NotNullable()
                  .WithColumn("Icon").AsString(30).NotNullable();

                Insert.IntoTable("Specializations")
                  .Row(new { SpecializationName = "Грузоперевозки",            Icon = "fas fa-dolly-flatbed" })
                  .Row(new { SpecializationName = "Мебельщик",                 Icon = "fas fa-couch" })
                  .Row(new { SpecializationName = "Сантехник",                 Icon = "fas fa-faucet" })
                  .Row(new { SpecializationName = "Дракон",                    Icon = "fas fa-dragon" })
                  .Row(new { SpecializationName = "Строитель",                 Icon = "fas fa-home" })
                  .Row(new { SpecializationName = "Компьютерщик",              Icon = "fas fa-laptop" })
                  .Row(new { SpecializationName = "Электрик",                  Icon = "far fa-lightbulb" })
                  .Row(new { SpecializationName = "Аварийное открытие замков", Icon = "fas fa-lock-open" })
                  .Row(new { SpecializationName = "Маляр",                     Icon = "fas fa-paint-roller" });
            }

            if (!Schema.Table("Services").Exists())
            {
                Create.Table("Services")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Name").AsString(30).NotNullable()
                  .WithColumn("Cost").AsDecimal().NotNullable()
                  .WithColumn("Long").AsInt32().NotNullable()
                  .WithColumn("SpecializationId").AsInt32().NotNullable();

                Create.ForeignKey("fk_Services_SpecializationId_Specializations_Id")
                   .FromTable("Services").ForeignColumn("SpecializationId")
                   .ToTable("Specializations").PrimaryColumn("Id");

                Insert.IntoTable("Services")
                  .Row(new { Name = "Перевоз груза",         Cost = 5000,  Long = 2, SpecializationId = 1 })
                  .Row(new { Name = "Мебельные работы",      Cost = 4000,  Long = 1, SpecializationId = 2 })
                  .Row(new { Name = "Сантехнические работы", Cost = 8000,  Long = 2, SpecializationId = 3 })
                  .Row(new { Name = "Осада замков",          Cost = 30000, Long = 1, SpecializationId = 4 })
                  .Row(new { Name = "Строительные работы",   Cost = 12000, Long = 3, SpecializationId = 5 })
                  .Row(new { Name = "Компьютерные работы",   Cost = 8000,  Long = 1, SpecializationId = 6 })
                  .Row(new { Name = "Электронные работы",    Cost = 3000,  Long = 1, SpecializationId = 7 })
                  .Row(new { Name = "Открытие ваших замков", Cost = 10000, Long = 1, SpecializationId = 8 })
                  .Row(new { Name = "Малярные работы",       Cost = 6000,  Long = 3, SpecializationId = 9 });
            }

            if (!Schema.Table("Users").Exists())
            {
                Create.Table("Users")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("RoleId").AsInt32().NotNullable()
                  .WithColumn("Login").AsString(20).NotNullable().Unique()
                  .WithColumn("Password").AsString(60).NotNullable()
                  .WithColumn("LastName").AsString(22).NotNullable()
                  .WithColumn("FirstName").AsString(15).NotNullable()
                  .WithColumn("MiddleName").AsString(15);

                Create.ForeignKey("fk_Services_RoleTableId_Roles_Id")
                    .FromTable("Users").ForeignColumn("RoleId")
                    .ToTable("Roles").PrimaryColumn("Id");

                Insert.IntoTable("Users")
                  .Row(new
                  {
                      RoleId = 1,
                      Login = "master",
                      Password = "$2a$13$CttizhvgFMkAVKGROm08wulHGPvmoo/NQgR7mHXsk91NCN.19m3g6",
                      LastName = "null",
                      FirstName = "null",
                      MiddleName = "null"
                  })
                  .Row(new
                  {
                      RoleId = 2,
                      Login = "operator",
                      Password = "$2a$13$CttizhvgFMkAVKGROm08wulHGPvmoo/NQgR7mHXsk91NCN.19m3g6",
                      LastName = "null",
                      FirstName = "null",
                      MiddleName = "null"
                  })
                  .Row(new
                  {
                      RoleId = 3,
                      Login = "client",
                      Password = "$2a$13$CttizhvgFMkAVKGROm08wulHGPvmoo/NQgR7mHXsk91NCN.19m3g6",
                      LastName = "null",
                      FirstName = "null",
                      MiddleName = "null"
                  })
                  .Row(new
                  {
                      RoleId = 4,
                      Login = "daddy",
                      Password = "$2a$13$CttizhvgFMkAVKGROm08wulHGPvmoo/NQgR7mHXsk91NCN.19m3g6",
                      LastName = "null",
                      FirstName = "null",
                      MiddleName = "null"
                  })
                  .Row(new
                  {
                      RoleId = 5,
                      Login = "admin",
                      Password = "$2a$13$CttizhvgFMkAVKGROm08wulHGPvmoo/NQgR7mHXsk91NCN.19m3g6",
                      LastName = "null",
                      FirstName = "null",
                      MiddleName = "null"
                  });
            }

            if (!Schema.Table("Locations").Exists())
            {
                Create.Table("Locations")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("LocationId").AsInt32().Nullable()
                  .WithColumn("LocationTypeId").AsInt32().NotNullable()
                  .WithColumn("LocationName").AsString(50).NotNullable()
                  .WithColumn("Coordinates").AsString(15).Nullable();

                Create.ForeignKey("fk_Locations_LocationId_Locations_Id")
                    .FromTable("Locations").ForeignColumn("LocationId")
                    .ToTable("Locations").PrimaryColumn("Id");

                Create.ForeignKey("fk_Locations_LocationTypeId_LocationTypes_Id")
                    .FromTable("Locations").ForeignColumn("LocationTypeId")
                    .ToTable("LocationTypes").PrimaryColumn("Id");

                Insert.IntoTable("Locations")
                  .Row(new { LocationTypeId = 1, LocationName = "Железнодорожный" })
                  .Row(new { LocationTypeId = 1, LocationName = "Советский" })
                  .Row(new { LocationTypeId = 1, LocationName = "Центральный" })
                  .Row(new { LocationTypeId = 1, LocationName = "Коминтерновский" })
                  .Row(new { LocationTypeId = 5, LocationName = "Имени Жака-Де-Вакансана" })
                  .Row(new { LocationTypeId = 3, LocationName = "Ивановка" })
                  .Row(new { LocationTypeId = 2, LocationName = "Северный" })
                  .Row(new { LocationTypeId = 3, LocationName = "Кореспандедовка" });

                Insert.IntoTable("Locations")
                    .Row(new { LocationId = 2, LocationTypeId = 4, LocationName = "Проклятый", Coordinates = "51.6683 39.1919" });
            }

            if (!Schema.Table("Masters").Exists())
            {
                Create.Table("Masters")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("UserId").AsInt32().NotNullable()
                  .WithColumn("LocationId").AsInt32().NotNullable()
                  .WithColumn("SpecializationId").AsInt32().NotNullable();

                Create.ForeignKey("fk_Masters_UserId_Users_Id")
                    .FromTable("Masters").ForeignColumn("UserId")
                    .ToTable("Users").PrimaryColumn("Id");

                Create.ForeignKey("fk_Masters_LocationId_Locations_Id")
                    .FromTable("Masters").ForeignColumn("LocationId")
                    .ToTable("Locations").PrimaryColumn("Id");

                Create.ForeignKey("fk_Masters_SpecializationId_Specializations_Id")
                    .FromTable("Masters").ForeignColumn("SpecializationId")
                    .ToTable("Specializations").PrimaryColumn("Id");

                //Insert.IntoTable("Masters")
                //  .Row(new { Id = , });
            }

            if (!Schema.Table("Schedules").Exists())
            {
                Create.Table("Schedules")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("MasterId").AsInt32().NotNullable()
                  .WithColumn("WorkingHoursFrom").AsDateTime().NotNullable()
                  .WithColumn("WorkingHoursTo").AsDateTime().NotNullable()
                  .WithColumn("Status").AsString(15).NotNullable();

                Create.ForeignKey("fk_Schedules_MasterId_Masters_Id")
                    .FromTable("Schedules").ForeignColumn("MasterId")
                    .ToTable("Masters").PrimaryColumn("Id");

                //Insert.IntoTable("Schedules")
                //  .Row(new { Id = , });
            }

            if (!Schema.Table("Orders").Exists())
            {
                Create.Table("Orders")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("MasterId").AsInt32().Nullable()
                  .WithColumn("UserId").AsInt32().Nullable()
                  .WithColumn("ServiceId").AsInt32().NotNullable()
                  .WithColumn("Decription").AsString(120).NotNullable()
                  .WithColumn("StartDate").AsDateTime().NotNullable()
                  .WithColumn("EndDate").AsDateTime().NotNullable()
                  .WithColumn("Status").AsString(15).NotNullable()
                  .WithColumn("StatusColor").AsString(15).NotNullable()
                  .WithColumn("Address").AsString(60).NotNullable()
                  .WithColumn("Comment").AsString(120).NotNullable()
                  .WithColumn("Picture").AsString(70).NotNullable();

                Create.ForeignKey("fk_Orders_MasterId_Masters_Id")
                    .FromTable("Orders").ForeignColumn("MasterId")
                    .ToTable("Masters").PrimaryColumn("Id");

                Create.ForeignKey("fk_Orders_UserId_Users_Id")
                    .FromTable("Orders").ForeignColumn("UserId")
                    .ToTable("Users").PrimaryColumn("Id");

                Create.ForeignKey("fk_Orders_ServiceId_Services_Id")
                    .FromTable("Orders").ForeignColumn("ServiceId")
                    .ToTable("Services").PrimaryColumn("Id");

                //Insert.IntoTable("Orders")
                //  .Row(new { Id = , });
            }
        }

        public override void Down()
        {
            if (Schema.Table("Roles").Exists())
            {
                Delete.Table("Roles");
            }

            if (Schema.Table("LocationTypes").Exists())
            {
                Delete.Table("LocationTypes");
            }

            if (Schema.Table("Specializations").Exists())
            {
                Delete.Table("Specializations");
            }

            if (Schema.Table("Services").Exists())
            {
                Delete.Table("Services");
            }

            if (Schema.Table("Users").Exists())
            {
                Delete.Table("Users");
            }

            if (Schema.Table("Locations").Exists())
            {
                Delete.Table("Locations");
            }

            if (Schema.Table("Masters").Exists())
            {
                Delete.Table("Masters");
            }

            if (Schema.Table("Schedules").Exists())
            {
                Delete.Table("Schedules");
            }

            if (Schema.Table("Orders").Exists())
            {
                Delete.Table("Orders");
            }
        }
    }
}
