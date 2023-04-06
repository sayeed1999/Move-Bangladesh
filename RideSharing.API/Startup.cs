using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RideSharing.Entity;
using RideSharing.Infrastructure;
using RideSharing.Repository;
using RideSharing.Service;
using System.Text;

namespace RideSharing.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Inject AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });

            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddControllers();

            #region open api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RideSharing.AuthAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                      },
                       new List<string>()
                    }
                });
            });
            #endregion

            // Configure additional services here, such as database context, authentication, authorization, etc.

            // This registers a DbContext subclass called ApplicationDbContext as a scoped service in the ASP.NET Core application service provider
            // (a.k.a. the dependency injection container). The context is configured to use the SQL Server database provider and will read the
            // connection string from ASP.NET Core configuration. Ref: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
            #region database init
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["AppSettings:ConnectionString"].ToString()));
            #endregion

            #region repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion

            #region services
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<ICustomerRatingService, CustomerRatingService>();
            services.AddScoped<IDriverRatingService, DriverRatingService>();
            services.AddScoped<ICabService, CabService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITripService, TripService>();
            #endregion

            #region disable automatic 400 response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region jwt authentication
            var key = Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtSecretKey"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Layer v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.WithOrigins(Configuration["AppSettings:ClientUrl"].ToString())
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
