using API.Extensions;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Thứ tự không quan trọng
            //Thêm lúc code controller Product
            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });*/ // Thêm bên extension
            //Mongo
            services.RegisterMongoDbRepositories();

            services.AddControllers();
            //Sqlite
            services.AddDbContext<StoreContext>(
                x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            //Nhận dạng người dùng
            services.AddDbContext<AppIdentityDbContext>(
                x => { 
                    x.UseSqlite(_config.GetConnectionString("IdentityConnection")); 
                });
            //SqlServer
            //services.AddDbContext<StoreContext>(
            //  x => x.UseSqlServer(_config.GetConnectionString("SqlServer")));

            //Repo
            services.AddScoped<IProductRepository, ProductRepository>();

            //Extension
            services.AddApplicationServices();
            //Dịch vụ nhận dạng
            services.AddIdentityServices(_config);

            services.AddSwaggerDocumentation();
            //
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            // Loại tổng quát T
            //services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            //services.AddAutoMapper(typeof(MappingProfiles));

            /*services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });*/ // Thêm bên extension
            //Dịch vụ giỏ hàng
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Thứ tự là quan trọng
        {
            app.UseMiddleware<ExceptionMiddleware>();
            /*app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1");
                //c.RoutePrefix = ""; // Thêm ngoài
            });*/ // Thêm bên extension

            /*if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

            }*/

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            //Cors
            app.UseCors("CorsPolicy");

            //Identity web token
            app.UseAuthentication();
            app.UseAuthorization();

            //Extension
            app.UseSwaggerDocmentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
