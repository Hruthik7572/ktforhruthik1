using Microsoft.EntityFrameworkCore;
using WebApplication1.BusinessLayer;
using WebApplication1.DataLayer;
using WebApplication1.Helper;
using WebApplication1.DataLayer;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddScoped<IUpdateStudentDetails, UpdateStudentDetails>();
            services.AddScoped<IStudentdata, Studentdata>();
            services.AddSingleton<ISqlConnectionContext, SqlConnectionContext>();
            services.AddDbContext<StudentDbContext>((sp, builder) =>
            builder.UseSqlServer(sp.GetRequiredService<ISqlConnectionContext>().GetConnectionString()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Display detailed exception page in development
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
