using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Blog_Backend.Services;
using DataAccess.DbAccess;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Amazon.S3;
using Amazon;
using Amazon.Runtime;



var builder = WebApplication.CreateBuilder(args);

//use production db only in production enviroment else we will use local db for dev
var keyVaultEndPoint = new Uri(builder.Configuration["VaultKey"]);

if (builder.Environment.IsProduction())
{
    // will use variables from keyvault
    builder.Configuration.AddAzureKeyVault(keyVaultEndPoint, new DefaultAzureCredential());

    
}

var AwsSecretKey = builder.Configuration["AwsConfiguration:SecretKey"];
var AwsAcessKey = builder.Configuration["AwsConfiguration:AcessKey"];

var credential = new BasicAWSCredentials(AwsAcessKey, AwsSecretKey);

 

builder.Services.AddSingleton<IAmazonS3>(sp =>
{

    return new AmazonS3Client(credential, RegionEndpoint.USEast1);
});

// add firebase

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))

}));

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
builder.Services.AddScoped<IS3Service, S3Service>();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
    loggingBuilder.AddAzureWebAppDiagnostics();
});



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
    app.UseCors("default");
}


app.UseCors("production");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
