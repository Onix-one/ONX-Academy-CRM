using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Services;
using ONX.CRM.DAL.Dapper.Repositories;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.EF.Repositories;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;
using ONX.CRM.WebAPI.Mapper;

namespace ONX.CRM.WebAPI
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

            services.AddScoped<IRepository<Student>, SqlStudentsRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IRepository<Course>, DapperCoursesRepository>();
            services.AddScoped<IDapperCourseService, DapperCourseService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ONX.CRM.WebAPI", Version = "v1" });
            });
            services.AddScoped<IStudentService, StudentService>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ONX.CRM.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
