using System.IO;
using ApiCore.Data;
using ApiCore.Interfaces;
using ApiCore.Interfaces.Acme;
using ApiCore.Interfaces.JokeGenerator;
using ApiCore.Interfaces.NineLetter;
using ApiCore.Services;
using ApiCore.Services.Acme;
using ApiCore.Services.JokeGenerator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using NineLetter.Web.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace ApiCore
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
			var appConfig = new NineLetterConfig();
            Configuration.GetSection("NineLetter").Bind(appConfig);

            _hostingEnvironment = env;

        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        private IHostingEnvironment _hostingEnvironment;
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
			
			services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .WithOrigins("http://localhost:8080");
                        //.AllowAnyOrigin()
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "This is a demo on how to test API end points against controllers",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Mathew Knott", Email = "mknott@test.ca", Url = "http://AcmeFunEvents.ca" }
                });
            });

            services.ConfigureSwaggerGen(options =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ApiCore.xml");
                options.IncludeXmlComments(xmlPath);
            });

            var physicalProvider = _hostingEnvironment.ContentRootFileProvider;
            services.AddSingleton(physicalProvider);
			
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<INineLetterService, NineLetterService>();

            services.AddTransient<IJokesFeed>(s => new JokesFeed("https://api.chucknorris.io/"));
            services.AddTransient<INamesFeed>(s => new NamesFeed("http://uinames.com/api/"));
            
            services.Configure<NineLetterConfig>(
                Configuration.GetSection("NineLetter"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            dbContext.Database.EnsureCreated();
			
			app.UseCors("VueCorsPolicy");

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acme Fun Events API");
            });
            app.UseMvc();
        }
    }
}
