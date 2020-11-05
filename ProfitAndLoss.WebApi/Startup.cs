using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using ProfitAndLoss.Business;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using ProfitAndLoss.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace ProfitAndLoss.WebApi
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
            var filename = Configuration.GetValue<string>("FirebaseCredentials");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(filename),
            });
            // Stop self referenceing loop
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            #region dbContext
            services.AddDbContextPool<DataContext>(
                //options => options.UseMySql(Configuration.GetConnectionString("MySqlDbConnection"))
                options => options.UseSqlServer(ConnectionString.CNN)
                );
            #endregion dbcontext
            services.AddScoped<IdentityServices>();
            //services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<IActorServices, ActorServices>();

            #region Registration services
            Global.InitServices(services);
            //services.AddScoped<IMemberService, MemberService>();
            //services.AddScoped<IBrandService, BrandService>();
            //services.AddScoped<IStoreService, StoreService>();

            #endregion Registration services
            services.AddIdentityCore<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            }).AddRoles<AppRole>()
            .AddDefaultTokenProviders()
            .AddSignInManager()
            .AddEntityFrameworkStores<DataContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
          services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JWT.ISSUER,
                    ValidAudience = JWT.AUDIENCE,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.Default.GetBytes(JWT.SECRET_KEY)),
                    ClockSkew = TimeSpan.Zero
                };
            }).AddCookie();
            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            //    config.AddPolicy(Policies.User, Policies.UserPolicy());
            //});
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new HypensNamingStrategy(),
                };
            });

            services.AddSwaggerGenNewtonsoftSupport();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "Build by Team Profit And Loss",
                    TermsOfService = new Uri("https://example.com/terms"),
                });

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                var requirement = new OpenApiSecurityRequirement();
                requirement[new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                }] = new string[] { };
                c.AddSecurityRequirement(requirement);
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddAutoMapper(typeof(Startup));
            // add mapper
            Global.Init();
            services.AddRouting(options => options.LowercaseUrls = true);

            //// add json config
            //services.AddNCacheDistributedCache(configuration =>
            //{
            //    configuration.CacheName = "demoClusteredCache";
            //    configuration.EnableLogs = true;
            //    configuration.ExceptionsEnabled = true;
            //});
            // add Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();

                    });
                //options.AddPolicy(name: "MyPolicy",
                //    builder =>
                //    {
                //        builder.AllowAnyOrigin()
                //              .AllowAnyMethod()
                //              .AllowAnyHeader();
                //    });
            });
      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

            }
            app.UseExceptionHandler("/error");
            //app.UseCors(builder =>
            //builder.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader());
            //app.UseCors(builder =>
            //builder.WithOrigins("https://localhost:44369")
            //.WithMethods("POST", "PUT", "DELETE")
            ////.WithMethods("GET", "POST", "PUT", "DELETE")
            //.AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My APi V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
