using MyRepositories.API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IReposRepository, ReposRepository>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [Cors]
builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region [Cors]
app.UseCors("CorsPolicy");
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
