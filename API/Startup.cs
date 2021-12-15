using API.Configuration;
using API.Infrastracture;
using API.Models;
using API.Services;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtConfig = _configuration.GetSection("JwtConfig");
            services.Configure<JwtConfig>(jwtConfig);
            services.Configure<CloudinarySettings>(_configuration.GetSection("Cloudinary"));

            var secretKey = jwtConfig.Get<JwtConfig>();

            services.AddControllers();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(secretKey.Secret);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });

            services.AddDefaultIdentity<User>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IUserAccessorService, UserAccessorService>();
            services.AddScoped<IAuthManagementService, AuthManagementService>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IPostPhotoService, PostPhotoService>();
            services.AddScoped<IUserPhotoService, UserPhotoService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ILikesService, LikesService>();
            services.AddScoped<IShopPhotoService, ShopPhotoService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IOccasionService, OccasionService>();
            services.AddScoped<IOccasionPhotoService, OccasionPhotoService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupPhotoService, GroupPhotoService>();
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IGroupPostService, GroupPostService>();
            services.AddScoped<IOccasionPostService, OccasionPostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
