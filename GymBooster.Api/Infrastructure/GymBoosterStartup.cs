using GymBooster.DatabaseAccess;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GymBooster.Api.Infrastructure
{
    public class GymBoosterStartup
    {
        public GymBoosterStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ServerConfig();
            Configuration.Bind(config);

            var trainingContext = new TrainingContext(config.MongoDb);
            var repo = new TrainingRepository(trainingContext);
            InitialDataProvider.AddData(trainingContext);
            services.AddSingleton<ITrainingRepository>(repo);
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("GymBoosterAPI", new OpenApiInfo
                {
                    Title = "GymBooster API",
                    Description = "GymBooster API",
                });
            });
        }
        
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/GymBoosterAPI/swagger.json", "GymBooster API");
            });
        }
    }


}
