using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(o => 
{
    o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.ClientId = "Eh8WlavCWfrmbRChIYylfEAE0kwa";            // <- Fill Client ID
    o.ClientSecret = "yeU_0TvVO4Dz6GsqoMiN9jbzAsQa";        // <- Fill Client Secret
    o.Authority = "https://stage.api.asgardeo.io/t/thivi/oauth2/token";           // <- Authority link. Assuming https://api.asgardeo.io/t/{org_name}/oidc ?
    o.ResponseType = OpenIdConnectResponseType.Code;
    o.ResponseMode = OpenIdConnectResponseMode.Query;
    o.GetClaimsFromUserInfoEndpoint = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
