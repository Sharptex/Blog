using AutoMapper;
using Blog.DTO.Validators;
using Blog_BLL.Contracts;
using Blog_BLL.Services;
using Blog_DAL.Contacts;
using Blog_DAL.Data;
using Blog_DAL.Models;
using Blog_DAL.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog
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
            var mapperConfig = new MapperConfiguration(v => v.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            string connection = Configuration.GetConnectionString("DefaultConnectionSqlite");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection))
                    .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                    .AddScoped<IAccountService, AccountService>()
                    .AddScoped<ITagService, TagService>()
                    .AddScoped<IPostService, PostService>()
                    .AddScoped<IAccountService, AccountService>()
                    .AddScoped<IRoleService, RoleService>()
                    .AddScoped<ICommentService, CommentService>();

            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddControllersWithViews();
            //services.AddRazorPages();
            services.AddControllers();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TagDTOValidator>());

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/api/login";
                        options.AccessDeniedPath = "/api/accessdenied";
                    });

            services.AddAuthorization(opts => {

                opts.AddPolicy("AdminPolicy", policy => {
                    policy.RequireClaim("Role", "DefaultUser");
                    policy.RequireClaim("Role", "Admin");
                });

                opts.AddPolicy("ModeratorPolicy", policy => {
                    policy.RequireClaim("Role", "DefaultUser");
                    policy.RequireClaim("Role", "Moderator");
                });

                opts.AddPolicy("UserPolicy", policy => {
                    policy.RequireClaim("Role", "DefaultUser");
                });
            });

            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API WSVAP (WebSmartView)", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            ApplicationDbInitializer.SeedUsers(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/home/error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //app.("/login", async (HttpContext context) =>
            //{
            //    context.Response.ContentType = "text/html; charset=utf-8";
            //    // html-форма для ввода логина/пароля
            //    string loginForm = @"<!DOCTYPE html>
            //                        <html>
            //                        <head>
            //                            <meta charset='utf-8' />
            //                            <title>METANIT.COM</title>
            //                        </head>
            //                        <body>
            //                            <h2>Login Form</h2>
            //                            <form method='post'>
            //                                <p>
            //                                    <label>Email</label><br />
            //                                    <input name='email' />
            //                                </p>
            //                                <p>
            //                                    <label>Password</label><br />
            //                                    <input type='password' name='password' />
            //                                </p>
            //                                <input type='submit' value='Login' />
            //                            </form>
            //                        </body>
            //                        </html>";
            //    await context.Response.WriteAsync(loginForm);
            //});

            //app.UseStatusCodePages();
            //app.UseStatusCodePagesWithReExecute("/error/{0}");

            //// Обработчик для ошибки "страница не найдена"
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Page not found");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //                name: "default",
            //                pattern: "{controller=Home}/{action=Index}/{id?}");
            //    //endpoints.MapRazorPages();
            //});
        }
    }
}
