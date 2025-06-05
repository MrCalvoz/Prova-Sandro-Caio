using API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapGet("/API/Tarefa",
([FromServices]AppDataContext ctx) =>
Result.Ok(ctx.Tarefa.ToList())
);


app.Run();
