using Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IDatacollector>(options => new Database("Worktime.dbo.Work")); // I am stoppable - I simply can be stopped
builder.Services.AddSingleton<ISQLQuery, SQLQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//Testtest
var thesql = app.Services.GetRequiredService<ISQLQuery>(); //Yoooooo! I am no longer stoppable! I simply cannot be stopped!
thesql.CreatePostGres();

app.Run();
