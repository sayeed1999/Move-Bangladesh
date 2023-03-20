using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RideSharing.API", Version = "v1" });
            });


            // Configure additional services here, such as database context, authentication, authorization, etc.

            #region database init
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["AppSettings:ConnectionString"].ToString()));
            #endregion

            //services.AddScoped<ApplicationDbContext>();

            #region repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<ICustomerRatingRepository, CustomerRatingRepository>();
            services.AddScoped<IDriverRatingRepository, DriverRatingRepository>();
            services.AddScoped<ICabRepository, CabRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
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

            // Disable automatic 400 response

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            // Identity Core

            services.AddIdentity<User, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });


            // Jwt Authentication

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

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Layer v1"));
            }

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
