using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pixicity.Data;
using Pixicity.Data.Mappings.AutoMapper;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.AppSettings;
using Pixicity.Domain.Transversal;
using Pixicity.Service.Implementations;
using Pixicity.Service.Interfaces;
using Pixicity.Web.Middlewares;
using System;
using System.IO;
using System.Text;
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
        public Usuario Usuario { get; set; } = new Usuario();

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
            services.AddScoped<IWebService, WebService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ILogsService, LogsService>();

            services.AddDbContext<PixicityDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<KeysAppSettingsViewModel>(Configuration.GetSection("Keys"));

            // Configuración del JWT
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<ISeguridadService>();

                        var userName = context.Principal.Identity.Name;
                        var user = userService.GetUsuarioByUserName(userName);

                        if (user == null)
                            context.Fail("Unauthorized");

                        Usuario.UserName = user.UserName;
                        Usuario.Id = user.Id;

                        return Task.CompletedTask;
                    }
                };

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Keys:JWT"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IAppPrincipal>(provider =>
            {
                return new AppPrincipal(0, "Unknown", "", false, false);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pixicity! API",
                    Description = "Pixicity Social Community API",
                    TermsOfService = new Uri("https://pixicity.io/terminos-condiciones"),
                    Contact = new OpenApiContact
                    {
                        Name = "nesticle8bit",
                        Email = "contacto@pixicity.io",
                        Url = new Uri("https://pixicity.io/perfil/nesticle8bit"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(corsPolicyName);
            //app.UseHttpsRedirection();

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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pixicity! API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
