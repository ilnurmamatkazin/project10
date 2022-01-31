using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;
using IGeekFan.AspNetCore.RapiDoc;

namespace project10
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
      // services.AddControllers ()
      //     .AddNewtonsoftJson (opt =>
      //         opt.SerializerSettings.ContractResolver = new DefaultContractResolver ());

      services.AddCors();
      // services.AddMvc();
      services.AddControllers().AddNewtonsoftJson();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
        var filePath = Path.Combine(System.AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
        c.IncludeXmlComments(filePath, true);
        c.EnableAnnotations();
      });

      //   services.AddSwaggerGen(c =>
      //   {
      //     c.SwaggerDoc("v1", new OpenApiInfo
      //     {
      //       Version = "v1",
      //       Title = "Walking in Moscow",
      //       Description = "Проект учениц 10 класса: Маматказиной Арин и Александровой Марии. \n Создана геоинформационная система, которая сделает прогулки по Москве интереснее и увлекательнее.",
      //       TermsOfService = new Uri("https://example.com/terms"),
      //       Contact = new OpenApiContact
      //       {
      //         Name = "Shayne Boyer",
      //         Email = string.Empty,
      //         Url = new Uri("https://twitter.com/spboyer"),
      //       },
      //       License = new OpenApiLicense
      //       {
      //         Name = "Use under LICX",
      //         Url = new Uri("https://example.com/license"),
      //       }
      //     });

      //           // using System.Reflection;
      //           var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      //     c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
      //   });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        // app.UseSwagger();
        // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "project10 v1"));
      }

      // app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      // app.UseAuthorization();

      // app.UseMvc();

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      //   // Enable middleware to serve generated Swagger as a JSON endpoint
      //   app.UseSwagger();

      //   // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
      //   app.UseSwaggerUI(c =>
      //   {
      //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "project10 v1");
      //     c.InjectStylesheet("/swagger-ui/custom.css");
      //   });






      app.UseSwagger();

      app.UseRapiDocUI(c =>
      {
        c.RoutePrefix = ""; // serve the UI at root
        c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
        c.GenericRapiConfig = new GenericRapiConfig()
        {
          RenderStyle = "focused",
          Theme = "dark",//light,dark,focused
        };
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapSwagger("{documentName}/api-docs");
      });


    }
  }
}
