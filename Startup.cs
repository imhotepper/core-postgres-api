using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_pg.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
    
namespace core_pg
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
          
            // Add framework services.
            var conStr = Configuration.GetConnectionString("DefaultConnection");
            var pgConn = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (!string.IsNullOrWhiteSpace(pgConn)) conStr = HerokuPGParser.ConnectionHelper.BuildExpectedConnectionString(pgConn);

            services.AddDbContext<DbCtx>(options =>options.UseNpgsql(conStr));

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //https://stackoverflow.com/questions/37780136/asp-core-migrate-ef-core-sql-db-on-startup
            var db = app.ApplicationServices.GetService<DbCtx>();
            db.Database.EnsureCreated();

            
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
