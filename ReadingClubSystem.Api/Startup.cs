using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadingClubSystem.Api.Data;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ReadingClubContext>(options =>
       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        // Swagger
        services.AddSwaggerGen();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(); // Habilitar Swagger

            // Habilitar la UI de Swagger en la ruta /swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reading Club API V1");
                c.RoutePrefix = string.Empty; // Swagger estará en la raíz del sitio
            });
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}
