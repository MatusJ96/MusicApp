using Microsoft.Extensions.Logging;
using MusicApp.Data;
using MusicApp.ViewModels;
using MusicApp.Views;

namespace MusicApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<SavedPage>();
        builder.Services.AddTransient<SavedPageViewModel>();

        //string dbPath = Path.Combine(FileSystem.AppDataDirectory, "item.db");

        builder.Services.AddSingleton<ItemRepository>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
