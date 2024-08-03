using CustomerManagement.Extensions;
using CustomerManagement.Middlewares;
using CustomerManagement.Services.AutoMapper;
var builder = WebApplication.CreateBuilder(args);

// Add app services to the container.
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddAppIdentity();
builder.Services.AddAppServices();
builder.Services.AddUnitOfWork();
builder.Services.AddAppControllers();
builder.Services.AddAppAutoMapper();

//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

//add middlewares
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
