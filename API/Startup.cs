using AutoMapper;
using Blog.DTO.Validators;
using Blog_BLL.Contracts;
using Blog_BLL.Services;
using Blog_DAL.Contacts;
using Blog_DAL.Data;
using Blog_DAL.Models;
using Blog_DAL.Repositories;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API
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
                    .AddScoped<IRoleService, RoleService>()
                    .AddScoped<ICommentService, CommentService>();

            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TagDTOValidator>());
            services.AddFluentValidationRulesToSwagger();

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

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blog API",
                    Description = "An ASP.NET Core Web API for managing blogs",
                    TermsOfService = new Uri("https://blog.com/terms"),
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
