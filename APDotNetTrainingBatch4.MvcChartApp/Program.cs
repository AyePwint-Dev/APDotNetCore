using Serilog;

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/APDotNetTrainingBath4.MvcChartApp.log");

//log file Save in bin/../../ file path
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File(filePath, rollingInterval: RollingInterval.Hour)
	.CreateLogger();
//Save in local logs folder
//Log.Logger = new LoggerConfiguration()
//	.WriteTo.Console()
//	.WriteTo.File("logs/APDotNetTrainingBath4.MvcChartApp.log", rollingInterval: RollingInterval.Hour)
//	.CreateLogger();

try
{
	Log.Information("[AP]Starting web application");
	var builder = WebApplication.CreateBuilder(args);
	builder.Services.AddSerilog();

	// Add services to the container.
	builder.Services.AddControllersWithViews();
	

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Home/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthorization();

	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}


