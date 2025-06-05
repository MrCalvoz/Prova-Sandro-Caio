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

app.MapGet("/api/tarefas/{id}", ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{
    Tarefa? tarefa = ctx.Tarefas.Find(id);
    if (tarefa == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(tarefa);
});

app.MapPut("/api/tarefas/{id}", ([FromRoute] string id,
    [FromBody] Tarefa tarefaAlterado,
    [FromServices] AppDataContext ctx) =>
{
    Tarefa?  = ctx.Tarefas.Find(id);
    if (tarefa == null)
    {
        return Results.NotFound();
    }
    Status? status = ctx.Status.Find(tarefa.StatusId);
    if (status is null)
    {
        return Results.NotFound();
    }
    tarefa.Status = status;
    tarefa.Titulo = tarefaAlterado.Titulo;
    tarefa.Status = tarefaAlterado.Status;
    tarefa.DataVencimento = tarefaAlterado.DataVencimento;
    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});

app.Run();
