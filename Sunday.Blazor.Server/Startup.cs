using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using DevExpress.ExpressApp.Xpo;
using Sunday.Blazor.Server.Services;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.Identity.Web;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OData;
using DevExpress.ExpressApp.WebApi.Services;
using Sunday.WebApi.JWT;
using DevExpress.ExpressApp.Security.Authentication;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using DevExpress.ExpressApp.Core;
using Sunday.Module.BusinessObjects.Security;
using Shib.Common.Interfaces.Address;
using Nominatim.Entities;
using Shib.XAF.Address.BusinessObjects.NonPersistent;
using Shib.Common.YandexAPI.Entities;

namespace Sunday.Blazor.Server;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        //DevExpress.Xpo.Metadata.ReflectionClassInfo.SuppressSuspiciousMemberInheritanceCheck = true;
        //services.AddScoped<IAddressSearcher>(x =>
        //    new NominatimAddressSearcher(()=> new AddressEntity()));

        services.AddScoped<IAddressSearcher>(x =>
            new YandexAddressManager(
                new GeocoderClient("2e6c8a50-744d-48d7-832e-3fd44ce16701"), () => new AddressEntity()));

        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthenticationTokenProvider, JwtTokenProviderService>();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddXaf(Configuration, builder => {
            builder.UseApplication<SundayBlazorApplication>();
            builder.Modules
                .AddAuditTrailXpo()
                .AddCloningXpo()
                .AddConditionalAppearance()
                .AddDashboards(options => {
                    options.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
                })
                .AddFileAttachments()
                .AddOffice()
                .AddReports(options => {
                    options.EnableInplaceReports = true;
                    options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
                    options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
                })
                .AddStateMachine(options => {
                    options.StateMachineStorageType = typeof(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine);
                })
                .AddValidation(options => {
                    options.AllowValidationDetailsAccess = false;
                })
                .AddViewVariants()
                .Add<Sunday.Module.SundayModule>()
            	.Add<SundayBlazorModule>();
            builder.ObjectSpaceProviders
                .AddSecuredXpo((serviceProvider, options) => {

#if RELEASE
                    string connectionStringName = "ProdConnectionString";
#else
                    string connectionStringName = "DevConnectionString";
#endif

                    string connectionString = null;     

                    if (Configuration.GetConnectionString(connectionStringName) != null)
                        connectionString = Configuration.GetConnectionString(connectionStringName);

                    

#if EASYTEST
                    if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                    }
#endif
                    ArgumentNullException.ThrowIfNull(connectionString);
                    options.ConnectionString = connectionString;
                    options.ThreadSafe = true;
                    options.UseSharedDataStoreProvider = true;
                })
                .AddNonPersistent();
            builder.Security
                .UseIntegratedMode(options => {
                    options.RoleType = typeof(CustomPermissionPolicyRole);
                    // ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
                    // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
                    options.UserType = typeof(ApplicationUser);
                    // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
                    // If you use PermissionPolicyUser or a custom user type, comment out the following line:
                    options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
                    options.UseXpoPermissionsCaching();
                })
                .AddPasswordAuthentication(options => {
                    options.IsSupportChangePassword = true;
                })
                .AddAuthenticationProvider<CustomAuthenticationProvider>();
        });
        var authentication = services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
        authentication
            .AddCookie(options => {
                options.LoginPath = "/LoginPage";
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = Configuration["Authentication:Jwt:Issuer"],
                    //ValidAudience = Configuration["Authentication:Jwt:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Jwt:IssuerSigningKey"]))
                    
                };}
            );
        //Configure OAuth2 Identity Providers based on your requirements. For more information, see
        //https://docs.devexpress.com/eXpressAppFramework/402197/task-based-help/security/how-to-use-active-directory-and-oauth2-authentication-providers-in-blazor-applications
        //https://developers.google.com/identity/protocols/oauth2
        //https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-auth-code-flow
        //https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow
        //authentication.AddMicrosoftIdentityWebApp(Configuration, configSectionName: "Authentication:AzureAd", cookieScheme: null);
        //authentication.AddMicrosoftIdentityWebApi(Configuration, configSectionName: "Authentication:AzureAd", jwtBearerScheme: "AzureAd");

        services.AddAuthorization(options => {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                "AzureAd",
                JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireXafAuthentication()
                    .Build();
        });

        services
            .AddXafWebApi(Configuration, options => {
                // Use options.BusinessObject<YourBusinessObject>() to make the Business Object available in the Web API and generate the GET, POST, PUT, and DELETE HTTP methods for it.
            })
            .AddXpoServices();
        services
            .AddControllers()
            .AddOData((options, serviceProvider) => {
                options
                    .AddRouteComponents("api/odata", new EdmModelBuilder(serviceProvider).GetEdmModel())
                    .EnableQueryFeatures(100);
            });

        //services.AddSwaggerGen(c => {
        //    c.EnableAnnotations();
        //    c.SwaggerDoc("v1", new OpenApiInfo {
        //        Title = "Sunday API",
        //        Version = "v1",
        //        Description = @"Use AddXafWebApi(options) in the Sunday.Blazor.Server\Startup.cs file to make Business Objects available in the Web API."
        //    });
        //    c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme() {
        //        Type = SecuritySchemeType.Http,
        //        Name = "Bearer",
        //        Scheme = "bearer",
        //        BearerFormat = "JWT",
        //        In = ParameterLocation.Header,
                
        //    });
        //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //        {
        //            {
        //                new OpenApiSecurityScheme() {
        //                    Reference = new OpenApiReference() {
        //                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
        //                        Id = "JWT"
                               
        //                    }
        //                },
        //                new string[0]
        //            },
        //    });
        //    //var azureAdAuthorityUrl = $"{Configuration["Authentication:AzureAd:Instance"]}{Configuration["Authentication:AzureAd:TenantId"]}";
        //    //c.AddSecurityDefinition("AzureAd", new OpenApiSecurityScheme {
        //    //    Type = SecuritySchemeType.OAuth2,
        //    //    Flows = new OpenApiOAuthFlows() {
        //    //        AuthorizationCode = new OpenApiOAuthFlow() {
        //    //            AuthorizationUrl = new Uri($"{azureAdAuthorityUrl}/oauth2/v2.0/authorize"),
        //    //            TokenUrl = new Uri($"{azureAdAuthorityUrl}/oauth2/v2.0/token"),
        //    //            Scopes = new Dictionary<string, string> {
        //    //                // Configure scopes corresponding to https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-configure-app-expose-web-apis
        //    //                { @"[Enter the scope name in the Sunday.Blazor.Server\Startup.cs file]", @"[Enter the scope description in the Sunday.Blazor.Server\Startup.cs file]" }
        //    //            }
        //    //        }
        //    //    }
        //    //});
        //    //c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        //    //    {
        //    //        new OpenApiSecurityScheme {
        //    //            Reference = new OpenApiReference {
        //    //                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
        //    //                Id = "AzureAd"
        //    //            },
        //    //            In = ParameterLocation.Header
        //    //        },
        //    //        new string[0]
        //    //    }
        //    //});
        //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            //app.UseSwagger();
            //app.UseSwaggerUI(c => {
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sunday WebApi v1");
            //    c.OAuthClientId(Configuration["Authentication:AzureAd:ClientId"]);
            //    c.OAuthUsePkce();
            //});
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        //app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseXaf();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
    }
}
