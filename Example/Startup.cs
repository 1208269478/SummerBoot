using Example.Models;
using Example.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using SqlOnline.Utils;
using SummerBoot.Core;
using System;
using System.Linq;
using Example.DbFile;
using Microsoft.Data.Sqlite;

namespace Example
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
            services.AddSummerBoot();

            services.AddSummerBootCache(it =>
            {
                it.UseRedis("129.204.47.226,password=summerBoot");
            });

            services.AddSummerBootRepository(it =>
            {
                it.DbConnectionType = typeof(SqliteConnection);
                it.ConnectionString = "Data source=./DbFile/mydb.db";
            });

            services.AddControllers().AddSummerBootMvcExtention();

            //services.AddSbScoped<Engine>(typeof(TransactionalInterceptor));

            //services.AddSbScoped<ICar, Car>(typeof(TransactionalInterceptor));

            //using (var database = new Db())    //新增
            //{
            //    database.Database.EnsureCreated(); //如果没有创建数据库会自动创建，最为关键的一句代码
            //}


            //services.AddSbScoped<IAddOilService, AddOilService>(typeof(TransactionalInterceptor));

            //services.AddSbScoped<IWheel, WheelA>("A轮胎", typeof(TransactionalInterceptor));
            //services.AddSbScoped<IWheel, WheelB>("B轮胎", typeof(TransactionalInterceptor));

            services.AddSbScoped<IPersonService, PersonService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
