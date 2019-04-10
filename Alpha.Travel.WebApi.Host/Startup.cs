namespace Alpha.Travel.WebApi.Host
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Filters;
    using FluentValidation;
    using MediatR;
    using Persistence;
    using AutoMapper;
    using Application.Customers.Queries;
    using Application.Customers.Validators;
    using Application.Destinations.QueryHandlers;
    using Application.Destinations.Validators;
    using Application.Destinations.Queries;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        private string XmlCommentsFilePath => Path.Combine(AppContext.BaseDirectory, "Alpha.Travel.WebApi.xml");

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = true;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddMediatR(typeof(GetDestinationPreviewQueryHandler).GetTypeInfo().Assembly);
            services.AddAutoMapper();

            var dbName = Guid.NewGuid().ToString();
            services.AddDbContext<AlphaTravelDbContext>(options =>
            {
                options.UseInMemoryDatabase(dbName);
            });

            services.AddOptions();
            services.Configure<ApiSettings>(settings => Configuration.GetSection(nameof(ApiSettings)).Bind(settings));
            services.Configure<AuthSettings>(settings => Configuration.GetSection(nameof(AuthSettings)).Bind(settings));

            services.AddMvcCore(options =>
            {
                options.Filters.AddGlobalExceptionFilters();
            })
            .AddApiExplorer()
            .AddFormatterMappings()
            .AddJsonFormatters()
            .AddDataAnnotations()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            })
            .AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(new string[] { JwtBearerDefaults.AuthenticationScheme }).RequireAuthenticatedUser().Build();
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSingleton<IResponseFactory, ResponseFactory>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            services.AddTransient<IValidator<GetDestinationsPreviewQuery>, GetDestinationsPreviewQueryValidator>();
            services.AddTransient<IValidator<GetDestinationPreviewQuery>, GetDestinationPreviewQueryValidator>();
            services.AddTransient<IValidator<GetCustomersPreviewQuery>, GetCustomersPreviewQueryValidator>();
            services.AddTransient<IValidator<GetCustomerPreviewQuery>, GetCustomerPreviewQueryValidator>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerIOperationFilter>();
                options.IncludeXmlComments(XmlCommentsFilePath);
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidIssuer = Configuration["AuthSettings:Issuer"],
            //        ValidateIssuer = true,
            //        ValidAudience = Configuration["AuthSettings:Audience"],
            //        ValidateAudience = false,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:SecurityKey"])),
            //        ValidateIssuerSigningKey = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.FromMinutes(0)
            //    };
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("AllowAllOrigins");
            app.UseMvc();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}