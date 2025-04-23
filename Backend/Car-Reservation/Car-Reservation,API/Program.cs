using Car_Reservation_API.Extension;
using Car_Reservation.APIs.Middlewares;
using Car_Reservation.APIs.Extensions;
using Microsoft.AspNetCore.Builder;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car-Reservation Api");
});


await app.UseUpdateDataBase();
await app.UseSeeding();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseCors(op =>
{
    op.WithOrigins(["http://localhost:3000", 
        "http://127.0.0.1:5500", 
        "https://amazing-pavlova-d11c9a.netlify.app"])
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();