using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DBTask.Core;
using Core.BackgroundQueue;
using System.Threading;
using DBTask.Entities;
using HugeArrayTask;
using ArrayTask;

namespace Main
{
    public class Startup
    {

        IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Database"), options => options.MigrationsAssembly("Main")));
            services.AddHostedService<BackgroundQueueService>();
            services.AddSingleton<BackgroundQueueService>();
            services.AddSingleton<BackgroundQueue>();
            services.AddScoped<DataBaseInitializer>();
            services.AddScoped<MaxFinder>();
            services.AddScoped<Multiplicator>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BackgroundQueue queue)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            Console.WriteLine(_env.ApplicationName);
            app.UseRouting();

            queue.Enqueue(async (CancellationToken cancellationToken, IServiceProvider services) =>
            {
                using (var scope = services.CreateScope())
                {
                    T service<T>() => scope.ServiceProvider.GetRequiredService<T>();
                    service<DataBaseInitializer>().Initialize();
                    #region Task 1,2
                    //SQL COMMAND
                    //select distinct pb.ProductId, max(pb.BasketId), max(b.CreatedOn)  from ProductsBaskets as pb
                    //                                                                  join Baskets as b on b.Id = pb.BasketId
                    //where b.CreatedOn > DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0)
                    //Group by pb.ProductId
                    //having  Count(*) <= 5
                    //select* from ProductsBaskets
                    // EF expression

                    //var ids = _context.ProductsBaskets
                    //.Include(pb => pb.Basket)
                    //.Where(pb => pb.Basket.CreatedOn.Year == DateTime.Now.Year)
                    //.GroupBy(p => p.ProductId).Select(p => new { Id = p.Key, Count = p.Count() })
                    //.Where(p => p.Count <= 5).Distinct().ToList();
                    #endregion

                    var max = service<MaxFinder>();
                    max.Execute();

                    var array = service<Multiplicator>();
                    array.Execute();
                }
            });

            app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
        }
    }
}
