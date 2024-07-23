using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project;
using Project.BLL;
using Project.DAL;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

//Add-Migration CategoryTable

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors();
//Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration
            ["Jwt:Key"]))
        };
    });

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
//
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//string ss = "Data Source=\u008e‰-˜…†\u0090\u0081˜‚\\SQLEXPRESS;Initial Catalog=N&T_Project;Integrated Security = True;Trust Server Certificate=True;";
builder.Services.AddDbContext<Context>(c => c.UseSqlServer("Data Source=srv2\\pupils;Initial Catalog=N&T_Projects;Integrated Security=True;Trust Server Certificate=True;"));
//builder.Services.AddDbContext<Context>(c => c.UseSqlServer("Data Source=LAPTOP-U499E8HQ;Initial Catalog=N&T_Project2;Integrated Security=True;Trust Server Certificate=True;"));
//builder.Services.AddDbContext<Context>(c => c.UseSqlServer("Data Source = \"\u0090‡\u008e‰-˜…†\u0090\u0081˜‚\\SQLEXPRESS\";Initial Catalog=N&T_Project; Integrated Security = True;Trust Server Certificate=True;"));
builder.Services.AddScoped<ICustomerDal, CustomerDal>();
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<IDonorDal, DonorDal>();
builder.Services.AddScoped<IGiftDal, GiftDal>();
builder.Services.AddScoped<ISaleDal, SaleDal>();
builder.Services.AddScoped<IWinnerDal, WinnerDal>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IWinnerService, WinnerService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "Images")),
    RequestPath = "/Img"
});
app.UseRouting();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//

app.UseAuthentication();


//

app.UseAuthorization();

//
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});
//



app.MapControllers();

app.Run();
