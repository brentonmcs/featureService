using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Serilog;
using ServiceStack.Redis;
using WH.FeatureService.Api.Common;
using WH.FeatureService.Api.Services;

namespace WH.FeatureService.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            
            #if DNXCORE50
                .WriteTo.TextWriter(Console.Out)
            #else
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .WriteTo.ColoredConsole()
            #endif

            .CreateLogger();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<IFeatureSetRepository, FeatureSetRepository>();
            
            services.AddInstance(typeof(IRedisClientsManager), new RedisManagerPool("127.0.0.1:6379"));
            services.AddTransient<IMongoConnector,MongoConnector>();
            services.AddTransient<ICache, RedisCache>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            loggerFactory.AddConsole();

            loggerFactory.MinimumLevel = LogLevel.Debug;
                        
            app.UseMiddleware<ErrorLoggingMiddleware>();
            
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
        }
    }
}
