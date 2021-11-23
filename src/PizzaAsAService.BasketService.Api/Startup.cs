using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PizzaAsAService.BasketService.Api.Data;
using PizzaAsAService.BasketService.Api.Data.Interfaces;
using PizzaAsAService.BasketService.Api.Repositories;
using PizzaAsAService.BasketService.Api.Repositories.Interfaces;
using StackExchange.Redis;

namespace PizzaAsAService.BasketService.Api;

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
        services.AddSingleton(sp =>
        {
            var configuration =
                ConfigurationOptions.Parse(Configuration.GetConnectionString("RedisConnection"), true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddTransient<IBasketContext, BasketContext>();
        services.AddTransient<IBasketRepository, BasketRepository>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaAsAService.BasketService.Api", Version = "v1" });
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaAsAService.BasketService.Api v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}