using Cart.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseSession();
app.MapControllers();
app.Run();