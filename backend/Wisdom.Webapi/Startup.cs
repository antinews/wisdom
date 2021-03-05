using AutoMapper;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Wisdom.Webapi.Auth;
using Wisdom.Webapi.Auth.AuthContext;
using Wisdom.Webapi.Entities.Context;
using Wisdom.Webapi.Extensions.CustomException;

namespace Wisdom.Webapi
{
    public class Startup
    {

        public static ILoggerRepository repository { get; set; }
        public Startup(IConfiguration configuration)
        {
            // 加载日志配置文件
            repository = LogManager.CreateRepository("WisdomLogRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // 跨域
            services.AddCors(options =>
                options.AddPolicy("*",
                    builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                ));
            //services.AddResponseCaching();
            
            // 异常过滤
            services.AddControllers(config => config.Filters.Add<GlobalExceptionFilter>())
                // Newtonsoft
                .AddNewtonsoftJson(
                //    options =>
                //{
                //    //修改属性名称的序列化方式，首字母小写
                //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //    //修改时间的序列化方式
                //    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
                //}
                );

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingsSection); // 配置映射
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>(); // 获取
            services.AddJwtBearerAuthentication(appSettings); // 登记


            services.Configure<RouteOptions>(options => options.LowercaseUrls = true); // 允许小写路由
            services.AddAutoMapper(typeof(Startup)); // AutoMapper依赖注入

            // 解决中文被编码
            services.Configure<WebEncoderOptions>(options =>
               options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            );

            //services.AddDbContext<WisdomDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //);


            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WisdomCloud4.0 WebApi", Version = "1.0.0" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            if (env.IsDevelopment())
            {
                Configurations.DbContext.IsDevelopment = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                Configurations.DbContext.IsDevelopment = false;
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors("*"); // 跨域
            //app.ConfigureCustomExceptionMiddleware(); // 异常中间件
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            AuthContextService.Configure(httpContextAccessor);

            Configurations.AppSettings.SetAppSetting(Configuration.GetSection("AppSettings"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0.0/swagger.json", "WisdomCloud4.0 WebApi");
            });
        }
    }
}
