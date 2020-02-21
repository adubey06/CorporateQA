using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using CorporateQnA.Services.Mapper;
using CorporateQnA.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Authentication.Google;
using CorporateQnA.Helpers;
using CorporateQnA.Interfaces;

namespace CorporateQnA
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

            
            services.AddDbContextPool<AppDbContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
            });
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IQuestionServices, QuestionServices>();
            services.AddScoped<IAnswerServices,AnswerServices>();
            services.AddScoped<IQuestionActivityServices, QuestionActivityServices>();
            services.AddScoped<IAnswerActivityServices, AnswerActivityServices>();
            services.AddScoped<IQuestionActivityServices, QuestionActivityServices>();
            services.AddTransient<IDbConnection>((connection) => new SqlConnection(Configuration.GetConnectionString("DBConnection")));
            services.AddScoped<IRequestContext, RequestContext>();
            services.AddHttpContextAccessor();
            services.ConfigureApplicationCookie(opt => {
                opt.LoginPath = "/login";
            });
            services.AddAuthentication().AddCookie(opt=> {    
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "490082799116-c5jem6d5gfuokc500f6nhm7c4qfajdp9.apps.googleusercontent.com";
                options.ClientSecret = "_gpk67ROxx2OfYWczWxQ6uAR";
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/build/ClientApp/"))
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "categories",
                    pattern: "categories",
                    new { controller ="Home" , action = "Index" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "question",
                    pattern: "home",
                    new { controller = "Home", action = "Index" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "question",
                    pattern: "home/question/{id?}",
                    new { controller = "Home", action = "Index" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "users/{id?}",
                    new { controller = "Home", action = "Index" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "user-all-questions",
                    pattern: "users/{id?}/all-questions",
                    new { controller = "Home", action = "Index" }
                    );

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "user-question-detail",
                    pattern: "users/{id?}/all-questions/{qid?}",
                    new { controller = "Home", action = "Index" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "user-all-answers",
                    pattern: "users/{id?}/all-answers",
                    new { controller = "Home", action = "Index" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "user-answered-question-detail",
                    pattern: "users/{id?}/all-answers/{qid?}/{aid?}",
                    new { controller = "Home", action = "Index" }
                    );

            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "login",
                    new { controller = "Account", action = "Login" }
                    );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "register",
                    pattern: "register",
                    new { controller = "Account", action = "Register" }
                    );

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "logout",
                    pattern: "logout",
                    new { controller = "Account", action = "Logout" }
                    );

            });


          /*  app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "add-detail",
                    pattern: "/Account/add-detail",
                    new { controller = "Account", action = " AddDetail" }
                    );

            });*/
        }
    }
}