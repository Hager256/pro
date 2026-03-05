using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MotivaAI.Services;
using MotivAi.Models;
var builder = WebApplication.CreateBuilder(args);

// ✅ تسجيل DbContext مع Connection String
builder.Services.AddDbContext<MotivAiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// باقي الخدمات
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddSingleton<OnnxService>();
builder.Services.AddSingleton<ThresholdService>();

var app = builder.Build();

// Middleware

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Route افتراضي
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
