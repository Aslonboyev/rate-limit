using AspNetCoreRateLimit;
using DemoRateLimitApp.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureRateLimiting();

builder.Services.AddCors(CorsOptions =>
{
    CorsOptions.AddPolicy("AllowAll", corsAccesses =>
    corsAccesses.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseAuthorization();
app.MapControllers();
app.Run();
