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
            // ������־�����ļ�
            repository = LogManager.CreateRepository("WisdomLogRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // ����
            services.AddCors(options =>
                options.AddPolicy("*",
                    builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                ));
            //services.AddResponseCaching();
            
            // �쳣����
            services.AddControllers(config => config.Filters.Add<GlobalExceptionFilter>())
                // Newtonsoft
                .AddNewtonsoftJson(
                //    options =>
                //{
                //    //�޸��������Ƶ����л���ʽ������ĸСд
                //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //    //�޸�ʱ������л���ʽ
                //    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
                //}
                );

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingsSection); // ����ӳ��
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>(); // ��ȡ
            services.AddJwtBearerAuthentication(appSettings); // �Ǽ�


            services.Configure<RouteOptions>(options => options.LowercaseUrls = true); // ����Сд·��
            services.AddAutoMapper(typeof(Startup)); // AutoMapper����ע��

            // ������ı�����
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
            app.UseCors("*"); // ����
            //app.ConfigureCustomExceptionMiddleware(); // �쳣�м��
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
