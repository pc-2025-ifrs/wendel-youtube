// Adicione os namespaces dos seus repositórios
using Youtube.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IChannelRepository, InMemoryChannelRepository>();
builder.Services.AddSingleton<IVideoRepository, InMemoryVideoRepository>();
builder.Services.AddSingleton<ICommentRepository, InMemoryCommentRepository>();


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();