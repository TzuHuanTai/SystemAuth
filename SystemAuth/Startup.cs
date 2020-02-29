using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SystemAuth.Filters;
using SystemAuth.ViewModels;
using SystemAuth.Models.SQLite;

namespace SystemAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
		
		public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //----驗證(AddAuthentication)Json Web Token----//
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Id,
                    //RoleClaimType = JwtClaimTypes.RoleId, //停止在jwt加入角色資訊，統一用id(帳號)判斷
                    //RoleClaimType = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Actort,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],      //Token頒發機構
                    ValidAudience = Configuration["Jwt:Audience"],  //Token頒發給誰
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Token簽名祕鑰
                };
                #region 必要時可使用事件do something
                //options.Events = new JwtBearerEvents()
                //{
                //    OnTokenValidated = context =>
                //    {
                //        context.HttpContext.User.Claims();
                //    }
                //};
                #endregion
            });

            //----抓封包資訊、client IP需要註冊HttpContext功能----//
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //----連接DB，原本ConnectString移到appsettings.json----//
            //services.AddDbContext<SystemAuthContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("AuthDB"))
            //);
            services.AddDbContext<SystemAuthContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("systemAuth"))
            );

            //----加入cross-origin-request-sharing----//
            services.AddCors(options=>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .WithExposedHeaders("Content-Disposition"); // content-disposition is *exposed* (and allowed because of AllowAnyHeader)
                    });
            });

            //----權限(AddAuthorization)，設定Attribute放在Action上做篩選----//
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdministratorUser", policy => {
            //        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim(JwtClaimTypes.Role, "1");
            //    });
            //    //options.AddPolicy("GeneralUser", policy => policy.RequireClaim(JwtClaimTypes.Role, "2"));
            //});    

            //註冊認證，讓所有API Method可做權限控管
            services.AddMvc(Configuration =>
            {
                //AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                //                .RequireAuthenticatedUser()
                //                .Build();
                //Configuration.Filters.Add(new AuthorizeFilter(policy));

                //全域註冊Filter，靠AuthorizationFilter驗證身分權限                
                //Configuration.Filters.Add(new AuthorizationFilter());

                //全域註冊Filter，ServiceFilterAttribute方式會被解析要用dependency injection，這樣就可在filter使用db功能
                Configuration.Filters.Add(new ServiceFilterAttribute(typeof(AuthorizationFilter)));
            });

            //----Filter----//
            //註冊，若只註冊需自行在controll加上標籤[ServiceFilter(typeof(AuthorizationFilter))]
            //AddSingleto failed: AddSingleton呼叫不會new, AddTransient、AddScoped呼叫方式會new
            services.AddScoped<AuthorizationFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //----網域需要在指定條件----//
            app.UseCors("AllowAllOrigins");

            //----需要驗證JWT權限----//
            app.UseAuthentication();

            //----個別Controller註冊Middleware Filter，驗證身分權限----//
            //app.UseMiddleware<xxxxFilter>();
            //app.UseMiddleware<>

            //----請求進入MVC，放在所有流程最後面----//
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
