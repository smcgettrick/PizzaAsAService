using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PizzaAsAService.MenuService.Api.Data;
using PizzaAsAService.MenuService.Api.Data.Interfaces;
using PizzaAsAService.MenuService.Api.Data.Repositories;
using PizzaAsAService.MenuService.Api.Data.Repositories.Interfaces;

namespace PizzaAsAService.MenuService.Api;

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
        services.Configure<MenuDatabaseSettings>(Configuration.GetSection(nameof(MenuDatabaseSettings)));

        services.AddSingleton<IMenuDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<MenuDatabaseSettings>>().Value);

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaAsAService.MenuService.Api", Version = "v1" });
        });

        services.AddTransient<IMenuDbContext, MenuDbDbContext>();
        services.AddTransient<IProductRepository, ProductRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaAsAService.MenuService.Api v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}