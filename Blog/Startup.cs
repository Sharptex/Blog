using AutoMapper;
using Blog.DTO.Validators;
using Blog_BLL.Contracts;
using Blog_BLL.Services;
using Blog_DAL.Contacts;
using Blog_DAL.Data;
using Blog_DAL.Models;
using Blog_DAL.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserViewModelValidator>());

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

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Home/Unauthorized";
                options.AccessDeniedPath = "/Home/AccessDenied";

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            ApplicationDbInitializer.SeedUsers(userManager);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/home/error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler("/Home/Error");
            app.UseMiddleware<UnhandledExceptionMiddleware>();

            app.UseMiddleware<HttpRequestBodyMiddleware>();
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
