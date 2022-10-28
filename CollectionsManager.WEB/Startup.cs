using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CollectionManager.WEB.Extensions;

namespace CollectionManager.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureSqlContext(Configuration);

            services.ConfigureRepositoryManager();

            services.ConfigureImageStorage(Configuration);

            services.ConfigureUnitOfWork();

            services.ConfigureAutoMapper();

            services.ConfigureAuth(Configuration);

            services.ConfigureIdentity();

            services.AddAuthorization();

            services.AddControllersWithViews();

            services.ConfigureFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");
            // pattern: "{controller=Items}/{action=AddItemToCollection}/{collectionId=3c0ddc14-0dd0-4a6f-f313-08dab8ca9bf2}");
            //   pattern: "{controller=Collections}/{action=CreateCollection}/{id?}");
           // pattern: "{controller=Items}/{action=Details}/{itemId=dd5666af-8f8f-421a-043c-08dab15f4778}");

        });
        }
    }
}
