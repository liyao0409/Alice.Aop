using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alice.Aop.Di;
using TestClient.Services;
using TestClient.Interceptors;

namespace TestClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            services.AddLogging();
            services.AddMvc();
            //services.AddScoped<ITest, Test>();
            Alice.Aop.Di.AliceServiceCollection aliceCollection = new Alice.Aop.Di.AliceServiceCollection();
            aliceCollection.Populate(services);
            //aliceCollection.AddScoped<ITest, Test>(new[] { new MyInterceptor() });
            aliceCollection.AddScoped<ITest, Test>(serviceProvider => {
                return new Test();
            }, new[] { new MyInterceptor() });
            var aliceProvider = aliceCollection.BuildServiceProvider();
            return aliceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.ApplicationServices = 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
