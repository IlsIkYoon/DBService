using DBService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
    options.MaxSendMessageSize = 10 * 1024 * 1024;    // 10 MB  
    options.EnableDetailedErrors = true;    //개발 과정에선 true, 운영 환경에선 false 권장
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<MyDBService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
