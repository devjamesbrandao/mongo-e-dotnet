using Mongo.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies(builder.Configuration);

builder.Services.AddSwaggerConfig();

builder.Services.AddCorsConfig();

var app = builder.Build();

app.UseSwaggerConfig(app.Environment);

app.MapControllers();

app.UseCorsConfig();

app.Run();
