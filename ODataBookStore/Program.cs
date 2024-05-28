using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ODataBookStore.Models;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ODataConventionModelBuilder builderO = new ODataConventionModelBuilder();
builderO.EntitySet<Book>("Books");
builderO.EntitySet<Press>("Presses");
builder.Services.AddDbContext<BookStoreContext>(otp => otp.UseInMemoryDatabase("BookLists"));
builder.Services.AddControllers();
builder.Services.AddControllers().AddOData(option => option.Select().Filter()
.Count().OrderBy().Expand().SetMaxTop(100)
.AddRouteComponents("odata", builderO.GetEdmModel()));
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

app.UseODataBatching();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();
    if (endpoint == null)
    {
        await next(); 
        return;
    }

    var metaData = endpoint.Metadata.OfType<IODataRoutingMetadata>().FirstOrDefault();
    if (metaData != null)
    {
        var templates = metaData.Template;
    }

    await next(); 
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
