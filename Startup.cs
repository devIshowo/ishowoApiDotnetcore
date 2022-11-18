using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.Api.Net.Extra;
using ItCommerce.Business.Extra;
using ItCommerce.Api.Net.Models;
using DinkToPdf.Contracts;
using DinkToPdf;
using ItCommerce.Reporting.Reports;
using Microsoft.AspNetCore.Mvc.Razor;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using Microsoft.Extensions.Logging;
using Wkhtmltopdf.NetCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication5
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string _contentRootPath;


        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
            // add des services endpoint, scoped, httpcontext et authentica
          
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ISHOWO API", Version = "V1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standart Authorization header using the Bearer Sheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes("Le test de l'usage ddu token")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            //MvcOptions.EnableEndpointRouting = false;

            services.AddWkhtmltopdf("wkhtmltopdf");

      

            //postgresql
            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            services.AddDbContext<IT_COMMERCEEntities>(opts =>
                                                       opts
                                                      .UseLazyLoadingProxies()
                                                      .UseNpgsql(connectionString));
           
            

            //get user code
            //string userNameSection = ConfigObject.GetUserName();
            //var dbInstance = Configuration.GetSection("DbInstance").GetValue<string>("value", "");
            //ConfigObject.DbInstance = dbInstance;
            //ConfigObject.ConnString = ConfigObject.GetUrl(userNameSection, dbInstance);

            //invoice type
            ConfigObject.IsLicenseOk = false;
            ConfigObject.ConnString = connectionString;
            ConfigObject.InvoiceType = Configuration.GetSection("Printing").GetValue<string>("invoice_type", "LARGE");

            //api url

            ConfigObject.ApiUrl = Configuration.GetSection("ApiUrl").GetValue<string>("value", "http://localhost:5000");


            //DI for repository
            services.AddSingleton<IItCommerceRepository, ItCommerceRepository>();

            //DI for report destination path
            string destPath = Configuration.GetSection("FilePath").GetValue<string>("value", "");
            services.AddScoped<ReportObject>(_ => new ReportObject(@destPath));

            //DI for resources (logos) path
            string resourcePath = Configuration.GetSection("ResourcesPath").GetValue<string>("value", "");
            services.AddScoped<ResourceObject>(_ => new ResourceObject(@resourcePath));

            // Add converter to DI
            //var context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));


            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            //DI for template service
            // services.AddScoped<ITemplateService, TemplateService>();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander(_contentRootPath));
            });
            services.AddTransient<ITemplateService, TemplateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //SWAGGER
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ISHOWO API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();


            app.UseCors(builder =>
         builder.WithOrigins("*") //localhost:3001
                .AllowAnyHeader()
                .AllowAnyMethod()
         );
          

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapRazorPages();
            //});
            // use authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("default", "api/{controller}/{action}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Params", action = "Banks" }
            );

                endpoints.MapRazorPages();
            });

       
            app.UseMvc();
            app.UseStaticFiles();


        }


    }
}
