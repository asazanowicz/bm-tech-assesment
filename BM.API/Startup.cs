using AutoMapper;
using BM.API.Mapping;
using BM.DataAccess.DbContexts;
using BM.Domain.Abstract;
using BM.Domain.Concrete;
using BM.Domain.ValidationAttributes;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace BM.API
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
            services.AddControllers(setupAction => { setupAction.ReturnHttpNotAcceptable = true; }).AddNewtonsoftJson(
                    setupAction =>
                    {
                        setupAction.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();
                    })
                .AddXmlDataContractSerializerFormatters();
            

            // Auto Mapper Configurations
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();

            services.AddDbContext<BMContext>(x => x.UseSqlite(Configuration.GetConnectionString("SqliteConnection"), 
                x => x.MigrationsAssembly("BM.API")));

            services.AddMvc(setup => { }).AddFluentValidation()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SlotModelValidator>()); ;

            services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc(
                    "BMAPISpecification", new OpenApiInfo
                    {
                        Title = "BM API",
                        Version = "1.0.0"
                    }
                );
            });
        }


        internal static IActionResult ProblemDetailsInvalidModelStateResponse(
            ProblemDetailsFactory problemDetailsFactory, ActionContext context)
        {
            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);
            ObjectResult result;
            if (problemDetails.Status == 400)
            {
                // For compatibility with 2.x, continue producing BadRequestObjectResult instances if the status code is 400.
                result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                result = new ObjectResult(problemDetails);
            }
            result.ContentTypes.Add("application/problem+json");
            result.ContentTypes.Add("application/problem+xml");

            return result;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });

            }

            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("/swagger/BMAPISpecification/swagger.json", "BM API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
