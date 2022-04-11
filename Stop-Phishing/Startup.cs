using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stop_Phishing.DAL;
using Stop_Phishing.DAL.Interfaces;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.Helpers.Token;
using Stop_Phishing.Models;
using Stop_Phishing.Services;

namespace Stop_Phishing
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration["Postgresql:ConnectionString"]));

            services.AddIdentity<User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Course>, GenericRepository<Course>>();
            services.AddTransient<IGenericRepository<Lesson>, GenericRepository<Lesson>>();
            services.AddTransient<IGenericRepository<Test>, GenericRepository<Test>>();
            services.AddTransient<IGenericRepository<Question>, GenericRepository<Question>>();
            services.AddTransient<IGenericRepository<Answer>, GenericRepository<Answer>>();
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ITestService, TestService>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey =
                            true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                        IssuerSigningKey = new SymmetricSecurityKey(key), // Add the secret key to our Jwt encryption
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Stop_Phishing", Version = "v1"});
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stop_Phishing v1"));
            }

            app.UseHttpsRedirection();
           // app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/Images")),
                RequestPath = new PathString("/Images")
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}