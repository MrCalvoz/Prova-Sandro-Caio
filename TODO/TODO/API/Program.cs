using API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapGet("/api/tarefas",
([FromServices]AppDataContext ctx) =>
Results.Ok(ctx.Tarefa.ToList())
);

app.MapPost("/api/tarefas",
    ([FromBody]Tarefas tarefas,[FromServices] AppDataContext ctx) =>
        var tarefa = ctx.Tarefas.Find(Tarefa.StatusId);
        if(Status == null){
            return Result.NotFound();
            tarefa.Status = Status;
            ctx.Tarefa.Add(tarefa);
            ctx.SaveChanges();
            return Results.Created($"/api/tarefas/tarefas.Id", tarefa);       
    });

app.Run();
