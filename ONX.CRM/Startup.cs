using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ONX.CRM.BLL.Models;
using ONX.CRM.Configuration;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.ServiceExtensions;
using ONX.CRM.ViewModel;

namespace ONX.CRM
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("msSql")));

            //Inject for DapperRepository
            services.AddTransient<IDbConnection, SqlConnection>
                (_ => new SqlConnection(Configuration.GetConnectionString("msSql")));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<Context>();

            services.AddControllersWithViews();

            services.AddMapper();
            services.AddRepositories();
            services.AddEntitiesServices();
            services.AddControllersWithViews();

            services.AddControllersWithViews()
                .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);

            services.Configure<SecurityOptions>(
                Configuration.GetSection(SecurityOptions.SectionTitle));

            services.AddScoped<RequestsListViewModel, RequestsListViewModel>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IServiceProvider serviceProvider, IOptions<SecurityOptions> securityOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            RoleInitializer.InitializeAsync(serviceProvider, securityOptions).Wait(); ;
        }

    }
}
