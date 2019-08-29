using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using AntilopaApi.Data;
using AntilopaApi.Infrastructure;
using AntilopaApi.Services;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag.Generation.Processors.Security;
using NSwag;
using System.Net;

namespace AntilopaApi
{
  public class Startup
  {
    // public Startup(IConfiguration configuration)
    // {
    //     Configuration = configuration;
    // }
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      if (env.IsDevelopment())
      {
        // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
        builder.AddUserSecrets<Startup>();
      }

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var conStr = Configuration.GetConnectionString("default");
      Console.WriteLine($"ENV_CONNECTION_STRING: {conStr}");
      services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("default")));



      // ===== Add Jwt Authentication ========
      var key = Encoding.UTF8.GetBytes(Configuration["JwtKey"]);
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
          ValidateAudience = false
        };
      });

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddScoped<CarService>();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      // Register the Swagger services
      services.AddSwaggerDocument(document =>
      {
        document.Title = "cars management API";
        document.Description = "Swagger UI for testing";
        // Add an authenticate button to Swagger for JWT tokens
        document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
        document.AddSecurity("JWT Token", Enumerable.Empty<string>(),
            new OpenApiSecurityScheme()
            {
              Type = OpenApiSecuritySchemeType.ApiKey,
              Name = nameof(Authorization),
              In = OpenApiSecurityApiKeyLocation.Header,
              Description = "Copy this into the value field: Bearer {token}"
            }
        );
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.ConfigureCustomExceptionMiddleware();
      if (env.IsDevelopment())
      {
        // app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      // Register the Swagger generator and the Swagger UI middlewares
      app.UseOpenApi();
      app.UseSwaggerUi3();

      app.UseHttpsRedirection();
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
