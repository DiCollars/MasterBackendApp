using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CoordinationOfServiceDeliveryAPI.SwaggerStaff;
using RepositoryImplimentationDb.ContractsImplimentations;
using RepositoryContractsDb.Contracts;
using ServicesContracts.ServiceInterfaces;
using ServicesImplimentation.ServiceImplimentations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServicesContracts;

namespace CoordinationOfServiceDeliveryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddTransient<ISqlRepositoryBase, SqlRepositoryBase>(provider => new SqlRepositoryBase(connectionStrings.DefaultConnection));

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<ILocationTypeRepository, LocationTypeRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<ISpecializationRepository, SpecializationRepository>();

            services.AddTransient<IHashPasswordService, HashPasswordService>();
            services.AddTransient<IAuthUserService, AuthUserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<ILocationTypeService, LocationTypeService>();
            services.AddScoped<IAddressService, AddressService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COSDAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "пук пук"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
