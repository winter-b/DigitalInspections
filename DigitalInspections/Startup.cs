using DigitalInspectionsWebApi.Helpers;
using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Repositories;
using DigitalInspectionsWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DigitalInspectionsWebApi
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
            services.AddControllersWithViews();


            services.AddControllers();

            services.AddSwaggerGen(c =>
             c.SwaggerDoc("v1", new OpenApiInfo { Title = "Digital Inspections API", Version = "v1", Description = "Digital inspections" }));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var postgresConnectionString = Configuration.GetConnectionString("PostgresConnection");
            Console.WriteLine(postgresConnectionString);

            services.AddSingleton<IAuthService>(
                new AuthService(
                    new AuthRepository (
                        new DigitalInspectionsDbContext(
                            new DbContextOptionsBuilder<DigitalInspectionsDbContext>()
                                .UseNpgsql(postgresConnectionString)
                                .Options)),
                    Configuration.GetValue<string>("Salt"),
                    Configuration.GetValue<string>("SigningKey"),
                    Configuration.GetValue<string>("RegistrationKey")));

            services.AddSingleton<IWorkService>(
                new WorkService(
                    new WorkRepository(
                        new DigitalInspectionsDbContext(
                            new DbContextOptionsBuilder<DigitalInspectionsDbContext>()
                            .UseNpgsql(postgresConnectionString)
                            .Options))));

            services.AddSingleton<IMachineService>(
                new MachineService(
                    new MachineRepository(
                        new DigitalInspectionsDbContext(
                            new DbContextOptionsBuilder<DigitalInspectionsDbContext>()
                            .UseNpgsql(postgresConnectionString)
                            .Options))));
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();


            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API V1");
                c.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
