using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialGames.TechnicalTest.Api;
using SocialGames.TechnicalTest.ApiService.MIddleware;

namespace SocialGames.TechnicalTest.ApiService
{
    public class Startup
    {
        private static string ApiName = typeof(Startup).Assembly.GetName().Name;

        private GamesBootstrapper Bootstrapper { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddControllersAsServices();

            services.AddSwaggerGen();
        }

        public void ConfigureContainer(IServiceContainer container)
        {
            Bootstrapper = new GamesBootstrapper(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Bootstrapper.Run();

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
                });
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<RequestToResponseTimeLoggerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
