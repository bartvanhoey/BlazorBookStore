using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using BookStore.Web.Authentication;
using BookStore.Web.Services.Users;
using Blazored.LocalStorage;
using BookStore.Web.Services.BookStore;
using BookStore.Model;
using BookStore.Web.Handlers;

namespace BookStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            services.AddTransient<ValidateHeaderHandler>();

            services.AddHttpClient<IBookStoreService<Publisher>, BookStoreService<Publisher>>()
                .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IBookStoreService<Author>, BookStoreService<Author>>()
                .AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddBlazoredLocalStorage();

            services.AddAuthorization(options => {
                options.AddPolicy("SeniorEmployee", policy => 
                policy.RequireClaim("IsEmployedBefore1990", "true"));
            });

            services.AddHttpClient<IUserService, UserService>(options =>
            {
                options.BaseAddress = new Uri("https://localhost:52317/api/");
                options.DefaultRequestHeaders.Add("User-Agent", "BlazorBookStore");
            });

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
