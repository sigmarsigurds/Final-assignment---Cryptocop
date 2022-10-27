using System.Text.Json.Serialization;
using Cryptocop.Software.API.Services.Implementations;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Cryptocop.Software.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CryptocopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CryptocopConnectionString"),
       b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
    ));

builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtTokenAuthentication(builder.Configuration);


var jwtConfig = builder.Configuration.GetSection("JwtConfig");
builder.Services.AddTransient<ITokenService>((c) =>
    new TokenService(
        jwtConfig.GetValue<string>("secret"),
        jwtConfig.GetValue<string>("expirationInMinutes"),
        jwtConfig.GetValue<string>("issuer"),
        jwtConfig.GetValue<string>("audience")
        ));

builder.Services.AddScoped<IQueueService, QueueService>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<ICryptoCurrencyService, CryptoCurrencyService>();
builder.Services.AddHttpClient<ICryptoCurrencyService, CryptoCurrencyService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("messariApiBaseUrl"));
});
builder.Services.AddTransient<IExchangeService, ExchangeService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddHttpClient<IShoppingCartService, ShoppingCartService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("messariApiBaseUrl"));
});
//builder.Services.AddTransient<ITokenService, TokenService>();


builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();