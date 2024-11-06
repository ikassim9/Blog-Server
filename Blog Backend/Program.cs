using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Blog_Backend.Services;
using DataAccess.DbAccess;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

//use production db only in production enviroment else we will use local db for dev
if (builder.Environment.IsProduction())
{
    var keyVaultEndPoint = new Uri(builder.Configuration["VaultKey"]);

    builder.Configuration.AddAzureKeyVault(keyVaultEndPoint, new DefaultAzureCredential());
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddSingleton<IPostData, PostData>();
builder.Services.AddSingleton<IDbAccess, DbAccess>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
// add firebase

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))

}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebasAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });



builder.Services.AddCors(options => options.AddPolicy("default", policy =>
{

    policy.WithOrigins("http://localhost:3000")


    .AllowAnyHeader().AllowAnyMethod();

}));

builder.Services.AddCors(options => options.AddPolicy("production", policy =>
{

    policy.WithOrigins("https://scribetoread.com")


     .AllowAnyHeader().AllowAnyMethod();

}));




builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("default");
app.UseCors("production");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
