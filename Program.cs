using UserManagementApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add controller support
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// Custom logging middleware
app.UseMiddleware<LoggingMiddleware>();

// Custom authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();

// Map controller endpoints
app.MapControllers();

app.Run();