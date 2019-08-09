using CheckersBot.Game;
using CheckersBot.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CheckersBot
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.TryAddSingleton<ISerializer, JsonSerializerWrapper>();
            services.TryAddSingleton<IGetNextMoves, GetNextMoves>();
            services.TryAddSingleton<IGetMoveWeight, GetMoveWeight>();
            services.TryAddSingleton<IPredictionBuilder, PredictionBuilder>();
            services.TryAddSingleton<INextMovesCalculator, NextMovesCalculator>();
            services.TryAddSingleton<IPossibleBeatsCalc, PossibleBeatsCalc>();
            services.TryAddSingleton<IPossibleMovesCalc, PossibleMovesCalc>();
            services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
