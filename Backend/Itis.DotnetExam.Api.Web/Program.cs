using Itis.DotnetExam.Api.SignalR.Hubs;
using Itis.DotnetExam.Api.Web.Constants;
using Itis.DotnetExam.Api.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCore();
builder.ConfigureAuthorization();
builder.ConfigureJwtBearer();
builder.ConfigurePostgresqlConnection();
builder.ConfigureMongoDbConnection();
builder.ConfigureRabbitMq();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseExceptionHandling();
app.UseCors(SpecificOrigins.MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrateDbAsync();

app.MapControllers();
app.MapHub<GameHub>("/hub");

app.Run();