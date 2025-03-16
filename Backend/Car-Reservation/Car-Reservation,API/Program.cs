using Car_Reservation_API.Extension;
using Car_Reservation.APIs.Middlewares;
using Car_Reservation.APIs.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.MapOpenApi();
app.UseSwaggerUI(O => O.SwaggerEndpoint("/openapi/v1.json", "Car-Reservation Api"));
await app.UseUpdateDataBase();
await app.UseSeeding();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseCors(op =>
{
    op.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.StartRedisServer();
app.Run();