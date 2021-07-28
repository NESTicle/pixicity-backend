using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pixicity.Data.Mappings.AutoMapper;
using Pixicity.Data.Models.Base;
using Pixicity.Domain.AppSettings;
using Pixicity.Service.Implementations;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pixicity.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string corsPolicyName = "CorsPixicityPolicy";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    builder => builder
                    .WithOrigins(Configuration.GetSection("AllowedCORS").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddScoped<IParametrosService, ParametrosService>();
            services.AddScoped<ISeguridadService, SeguridadService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddDbContext<PixicityDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(MappingProfile));


            services.Configure<KeysAppSettingsViewModel>(Configuration.GetSection("Keys"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(corsPolicyName);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/images"
            });
        }
    }
}
