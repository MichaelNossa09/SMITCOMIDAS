using Microsoft.AspNetCore.Identity;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Services;
using SMITCOMIDAS.Web.Components;
using SMITCOMIDAS.Web.Data;
using SMITCOMIDAS.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using SMITCOMIDAS.Shared.Services.SnackBar;
using SMITCOMIDAS.Web.Services.SnackBar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the SMITCOMIDAS.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<IPlatformService, PlatformService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        NameClaimType = ClaimTypes.NameIdentifier,
        RoleClaimType = ClaimTypes.Role
    };
});
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5000")
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPersonasService, PersonasService>();
builder.Services.AddScoped<ICentrosCostoService, CentrosCostoService>();
builder.Services.AddScoped<ICompaniasService, CompaniasService>();
builder.Services.AddScoped<IProveedoresService, ProveedoresService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IElementoMenuService, ElementoMenuService>();
builder.Services.AddScoped<IDisponibilidadElementoService, DisponibilidadElementoService>();
builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddScoped<IDialogService, DialogService>();
builder.Services.AddScoped<ISnackbar, SnackBarService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IRolesService, RolesService>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(SMITCOMIDAS.Shared._Imports).Assembly);

app.Run();
