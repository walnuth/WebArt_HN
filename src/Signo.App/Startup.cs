using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Signo.App.Configurations;
using Signo.App.Services;
using Signo.Data.Context;


namespace Signo.App
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }





        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MeuDbContext>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection")
                     ));

            services.ResolveDependencies();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvcConfiguration();

            services.AddIdentityConfiguration(Configuration);
            services.AddControllersWithViews();
            services.AddRazorPages();
           
            services.Configure<AuthMessageSenderOptions>(Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                //parenteses significa que cabe UM ELEMENTO
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                //chaves significa que cabe uma LISTA
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
                //chaves significa que cabe uma LISTA
            };



            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>

            {
                //endpoints.MapControllerRoute(
                //    "PorAreas",
                //    "{area: exists}/{controller}/{action}/{id?}",

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            }


                );

        }
    }
}
