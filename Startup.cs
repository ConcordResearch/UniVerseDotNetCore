using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UniVerseDotNetCore.Middleware;


namespace UniVerseDotNetCore
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // ReSharper disable once UnusedMember.Global
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
            services.AddLogging(configure => configure.AddConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
           //loggerFactory.AddDebug(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ResponseLoggingMiddleware>();
            //app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }

   
}
