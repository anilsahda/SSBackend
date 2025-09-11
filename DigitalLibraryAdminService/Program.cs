using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using System.Text;
using Amazon;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var awsOptions = builder.Configuration.GetSection("AWS");
    var region = awsOptions["Region"];
    var accessKey = awsOptions["AccessKey"];
    var secretKey = awsOptions["SecretKey"];

    return new AmazonDynamoDBClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
});

builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();




builder.Services.AddAuthorization();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
