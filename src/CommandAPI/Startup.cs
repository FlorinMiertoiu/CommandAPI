using CommandAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using Newtonsoft.Json.Serialization;

namespace CommandAPI
{
    public class Startup
    {
		public IConfiguration Configuration {get;}
		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		
        public void ConfigureServices(IServiceCollection services)
        {
			var builder = new NpgsqlConnectionStringBuilder();
			builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
			builder.Username = Configuration["UserID"];
			builder.Password = Configuration["Password"];
			
			services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(builder.ConnectionString));
			
            services.AddControllers().AddNewtonsoftJson(s =>{
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>(); //links the interface with the class that implements the interface, to the client only the interface is visible
			
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                /*endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World");
                }
                );*/
            });
        }
    }
}