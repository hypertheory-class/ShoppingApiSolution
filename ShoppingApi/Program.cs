global using Microsoft.EntityFrameworkCore;
global using ShoppingApi.Data;
using ShoppingApi;
using ShoppingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<OnCallDeveloperHttpService>(config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("onCallDeveloperUri"));
})
    .AddPolicyHandler(HttpPolicies.GetRetyPolicy())
    .AddPolicyHandler(HttpPolicies.GetCircuitBreaker());

var globalSystemTime = new SystemTime();
// configure that thing...

//builder.Services.AddScoped<ISystemTime, SystemTime>();
builder.Services.AddSingleton<ISystemTime>(globalSystemTime);
//builder.Services.AddSingleton<ISystemTime, SystemTime>();
builder.Services.AddTransient<IProvideStatusInformation, RemoteStatusProvider>();

builder.Services.AddDbContext<ShoppingDataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("shopping"));
});


// TODO: Talk about this again when we create our ingress later. I don't like this code being here and it isn't going to be needed 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(); // When Jeff does BS stuff in class, it's an indication that it is MORE important than he is letting on.
app.UseAuthorization();

app.MapControllers();

app.Run();
