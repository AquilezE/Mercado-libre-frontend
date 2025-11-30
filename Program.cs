using Mercado_libre_frontend.Middlewares;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregamos los servicios
builder.Services.AddControllersWithViews();

// Soporte para consultar el API
var UrlWebAPI = builder.Configuration["UrlWebAPI"];
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<EnviaBearerDelegatingHandler>();
builder.Services.AddTransient<RefrescaTokenDelegatingHandler>();

HttpClientHandler CreateHandler() =>
    new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

// -------------------------
// AuthClientService
// -------------------------
builder.Services.AddHttpClient<AuthClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// CategoriasClientService
// -------------------------
builder.Services.AddHttpClient<CategoriasClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// UsuariosClientService
// -------------------------
builder.Services.AddHttpClient<UsuariosClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// RolesClientService
// -------------------------
builder.Services.AddHttpClient<RolesClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// ProductosClientService
// -------------------------
builder.Services.AddHttpClient<ProductosClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// PerfilClientService
// -------------------------
builder.Services.AddHttpClient<PerfilClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// ArchivosClientService
// -------------------------
builder.Services.AddHttpClient<ArchivosClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// BitacoraClientService
// -------------------------
builder.Services.AddHttpClient<BitacoraClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// CarritoClientService
// -------------------------
builder.Services.AddHttpClient<CarritoClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);

// -------------------------
// PedidoClientService
// -------------------------
builder.Services.AddHttpClient<PedidoClientService>(c =>
{
    c.BaseAddress = new Uri(UrlWebAPI!);
})
.AddHttpMessageHandler<EnviaBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(CreateHandler);


// -------------------------
// Authentication cookies
// -------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = ".MercadoLibreFrontend";
        options.LoginPath = "/Auth";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
