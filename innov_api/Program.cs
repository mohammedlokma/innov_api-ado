using innov_api;
using innov_api.Data;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddAutoMapper(typeof(MapConfig));
builder.Services.AddControllers(options => { options.AllowEmptyInputInBodyModelBinding = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.Use(async (ctx, next) =>
//{
//    // using Microsoft.AspNetCore.Http;
//    var endpoint = ctx.GetEndpoint();

//    if (endpoint != null)
//    {
//        // An endpoint was matched.
//        // ...
//        var routePattern = (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern
//                                                                                        ?.RawText;

//        //var request = ctx.Request;
//        //var host = request.Host;
//        //if (host.Host.Equals("customdomainurl", StringComparison.OrdinalIgnoreCase))
//        //{
//        //    ctx.Response.Redirect(UriHelper.Encode(request.Scheme, "api/dd",
//        //        request.PathBase, request.Path, request.QueryStatement));
//        //    return Task.FromResult(0);
//        //}
//        //ctx.Response.Redirect("https://localhost:7132/api/Group/GetGroups");
//        return ctx.Request. Redirect();

//    }

//    await next();
//});

//app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
