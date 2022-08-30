using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SoluWalter.BusinessLogic.Posts;
using SoluWalter.BusinessLogic.Users;
using SoluWalter.DataAccess.Posts;
using SoluWalter.DataAccess.Users;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoluWalter.Api.AutoMapperFile;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SoluWalter.Api
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
            services.Configure<DBSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("DBSettings:ConnectionString").Value;
                options.Database = Configuration.GetSection("DBSettings:Database").Value;
            });

            var appSettingsSection = Configuration.GetSection("APPSettings");
            services.Configure<APPSettings>(appSettingsSection);

            ////jwt
            //var appSettings = appSettingsSection.Get<APPSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(d =>
            //{
            //    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //  .AddJwtBearer(d =>
            //  {
            //      d.RequireHttpsMetadata = false;
            //      d.SaveToken = true;
            //      d.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateIssuerSigningKey = true,
            //          IssuerSigningKey = new SymmetricSecurityKey(key),
            //          ValidateIssuer = false,
            //          ValidateAudience = false
            //      };
            //  });

            ConfigureTokenAuthentication(services, appSettingsSection);

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostDataContext, PostDataContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserDataContext, UserDataContext>();            

            services.AddControllers();

            services.AddCors(cfg => cfg.AddPolicy("CorsPolicy",
               builder =>
               {
                   builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
               })
           );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoluWalter-Api", Version = "v1" });
            });

            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapperProfile());
            //});
            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
           
        }

        private void ConfigureTokenAuthentication(IServiceCollection services, IConfigurationSection appSettingsSection)
        {
            var appSettings = appSettingsSection.Get<APPSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoluWalter.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
             
        }
    }
}
