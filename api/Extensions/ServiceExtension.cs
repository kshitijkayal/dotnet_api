using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication;

namespace api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<ApplicationDBContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IcommentRepository, CommentRepository>();

            return services;
        }
        
    }
}